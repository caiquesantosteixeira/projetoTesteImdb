import { OnInit, AfterContentChecked, Injector, EventEmitter } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { NotificacaoService } from './../notificacao/notificacao.service';

import { ToastrService } from 'ngx-toastr';

import { BaseResourceModel } from '../../models/base-resource.model';
import { BaseResourceService } from '../../services/base-resource.service';

import { ErroService } from './../../../shared/services/erro.service';

export abstract class BaseResourceFormPopupComponent<T extends BaseResourceModel> implements OnInit, AfterContentChecked {

  currentAction: string;
  resourceForm: FormGroup;
  pageTitle: string;
  serverErrorMessages: string[] = null;
  submittingForm = false;

  protected route: ActivatedRoute;
  protected router: Router;
  protected formBuilder: FormBuilder;
  protected erroService: ErroService;
  protected notificacaoService: NotificacaoService;
  public toastr: any;

  protected cadIsPopup: boolean;
  protected dadosObjeto: any;
  protected fechouJanela = new EventEmitter();

  constructor(
    protected injector: Injector,
    public resource: T, //  class instance
    protected resourceService: BaseResourceService<T>,
    protected jsonDataToResourceFn: (jsonData) => T,
    protected path: string
  ) {
    this.route = this.injector.get(ActivatedRoute);
    this.router = this.injector.get(Router);
    this.formBuilder = this.injector.get(FormBuilder);
    this.toastr = this.injector.get(ToastrService);
    this.erroService = this.injector.get(ErroService);
    this.notificacaoService = this.injector.get(NotificacaoService);
  }

  ngOnInit() {
    // this.setCurrentAction();
    this.buildResourceForm();
    this.loadResource();
  }

  ngAfterContentChecked() {
    // this.setPageTitle();
  }


  protected loadResource() {
    if (this.cadIsPopup) {
      this.preencherForm();
    } else {
      const path = this.route.snapshot.url[0].path;
      if ((path !== undefined) && Number(path) && parseInt(path) <= 0) {
        this.router.navigate(['/grupos']);
      }

      if ((path !== undefined) && path === 'novo') {
        this.pageTitle = this.creationPageTitle();
      } else {
        this.resourceService.getById(+path).subscribe(
          (res) => {
            this.preencherForm(res);
            this.pageTitle = this.editionPageTitle();
          },
          (error) => this.erroService.setError(error)
        );
      }

    }
  }

  protected preencherForm(obj: T = null) {
    this.submittingForm = false;
    if ((this.dadosObjeto != null && this.dadosObjeto !== undefined) || obj !== null) {
      this.resource = obj !== null ? obj : this.dadosObjeto;
      this.resourceForm.patchValue(this.resource);
    }
  }

  protected defineAcao() {
    if (this.resourceForm.get('id').value === 0) {
      this.inserirRegistro();
    } else {
      this.atualizarRegistro();
    }
  }

  protected inserirRegistro() {
    const resource: T = this.jsonDataToResourceFn(this.resourceForm.value || []);
    this.resourceService.create(resource).subscribe(
      (res) => {
        this.toastr.success('Solicitação processada com sucesso!');
        if (!this.cadIsPopup) {
          const baseComponentPath = this.route.snapshot.parent.url[0].path;
          this.router.navigate([baseComponentPath, res.id, 'edit']);
        }
        this.preencherForm(res);
      },
      (erro) => {
        this.submittingForm = false;
        this.erroService.setError(erro);
      }
    );
  }

  private atualizarRegistro() {
    const resource: T = this.jsonDataToResourceFn(this.resourceForm.value || []);
    this.resourceService.update(resource).subscribe(
      (res) => {
        this.toastr.success('Solicitação processada com sucesso!');
        this.preencherForm(res);
      },
      (erro) => {
        this.submittingForm = false;
        this.erroService.setError(erro);
      }
    );
  }

  protected creationPageTitle(): string {
    return 'Cadastro de recurso';
  }

  protected editionPageTitle(): string {
    const categoryName = this.resource.id || '';
    return 'Editando recurso: ' + categoryName;
  }

  public Voltar() {
    if (this.cadIsPopup) {
      this.Reset();
      this.fechouJanela.emit(true);
    } else {
      this.router.navigate([this.path]);
    }
  }

  public Reset() {
    this.resourceForm.reset();
    this.buildResourceForm();
  }

  public submitForm() {
    if (this.resourceForm.valid) {
      this.submittingForm = true;
      this.defineAcao();
    }
  }

  public Novo() {
    this.router.navigate([this.path, 'novo']);
  }

  // AINDA NAO ALTERADO
  protected abstract buildResourceForm(): void;

}

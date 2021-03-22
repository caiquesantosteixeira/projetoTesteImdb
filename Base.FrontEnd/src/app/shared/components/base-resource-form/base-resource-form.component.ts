import { OnInit, AfterContentChecked, Injector } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { switchMap } from 'rxjs/operators';

import { ToastrService } from 'ngx-toastr';

import { BaseResourceModel } from '../../models/base-resource.model';
import { BaseResourceService } from '../../services/base-resource.service';

import { ErroService } from './../../../shared/services/erro.service';

export abstract class BaseResourceFormComponent<T extends BaseResourceModel> implements OnInit, AfterContentChecked {

  currentAction: string;
  resourceForm: FormGroup;
  pageTitle: string;
  serverErrorMessages: string[] = null;
  submittingForm = false;

  protected route: ActivatedRoute;
  protected router: Router;
  protected formBuilder: FormBuilder;
  protected erroService: ErroService;

  public toastr: any;

  constructor(
    protected injector: Injector,
    public resource: T, //  class instance
    protected resourceService: BaseResourceService<T>,
    protected jsonDataToResourceFn: (jsonData) => T
  ) {
    this.route = this.injector.get(ActivatedRoute);
    this.router = this.injector.get(Router);
    this.formBuilder = this.injector.get(FormBuilder);
    this.toastr = this.injector.get(ToastrService);
    this.erroService = this.injector.get(ErroService);
  }

  ngOnInit() {
    this.setCurrentAction();
    this.buildResourceForm();
    this.loadResource();
  }

  ngAfterContentChecked() {
    this.setPageTitle();
  }

  submitForm() {
    this.submittingForm = true;
    if (this.currentAction === 'novo') {
      this.createResource();
    } else {
      this.updateResource();
    }
  }

  // PRIVATE METHODS
  protected setCurrentAction() {
    const url = this.route.snapshot.url[0];
    if (url !== undefined) {
      if (url.path === 'novo') {
        this.currentAction = 'novo';
      } else {
        this.currentAction = 'edit';
      }
    }
  }

  protected loadResource() {
    if (this.currentAction === 'edit') {
      this.route.paramMap.pipe(
        switchMap(params => this.resourceService.getById(+params.get('id')))
      )
        .subscribe(
          (resource) => {
            this.resource = resource;
            this.resourceForm.patchValue(resource); // binds load resource form
          },
          (error) => this.erroService.setError(error)
        );
    }
  }

  protected setPageTitle() {
    if (this.currentAction === 'novo') {
      this.pageTitle = this.creationPageTitle();
    } else {
      this.pageTitle = this.editionPageTitle();
    }
  }

  protected creationPageTitle(): string {
    return 'Novo';
  }

  protected editionPageTitle(): string {
    return 'Edição';
  }

  protected createResource() {
    const resource: T = this.jsonDataToResourceFn(this.resourceForm.value || []);
    this.resourceService.create(resource)
      .subscribe(
        resourceRet => this.actionsForSuccess(resourceRet),
        error => this.actionsForError(error)
      );
  }

  protected updateResource() {
    // console.log(this.resourceForm.value);
    const resource: T = this.jsonDataToResourceFn(this.resourceForm.value);
    this.resourceService.update(resource)
      .subscribe(
        resourceRet => this.actionsForSuccess(resourceRet),
        error => this.actionsForError(error)
      );
  }

  protected actionsForSuccess(resource: T) {
    this.toastr.success('Solicitação processada com sucesso!');
    const baseComponentPath = this.route.snapshot.parent.url[0].path;
    // skipLocationChange - faz com que nao grave o historico de navegacao
    // redirecionando e voltando para o metodo edit
    this.router.navigateByUrl(baseComponentPath, { skipLocationChange: true })
      .then(
        () => {
          this.router.navigate([baseComponentPath, resource.id, 'edit']);
        }
      );
  }

  protected actionsForError(error) {
    this.erroService.setError(error);
    // Reabilita o botão salvar
    this.submittingForm = false;
  }

  public reset() {
    this.resourceForm.reset(this.buildResourceForm());
  }

  protected abstract buildResourceForm(): void;

}

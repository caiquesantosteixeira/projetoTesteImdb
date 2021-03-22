import { Component, OnInit, Injector } from '@angular/core';
import { Validators } from '@angular/forms';
import { Observable } from 'rxjs';

import { BaseResourceFormComponent } from 'src/app/shared/components/base-resource-form/base-resource-form.component';
import { Usuario, UsuarioCommand } from '../shared/usuario.model';
import { UsuarioService } from './../shared/usuario.service';

import { ParametrosEmpresa } from '../../parametrosEmpresa/shared/parametros-empresa.model';
import { ParametrosEmpresaService } from '../../parametrosEmpresa/shared/parametros-empresa.service';

import { TrataCheckbox } from '../../../../shared/Util/trata.checkBox';
import { TrataNumeros } from '../../../../shared/Util/trata.numeros';
import { NotificacaoService } from '../../../../shared/components/notificacao/notificacao.service';
import { switchMap } from 'rxjs/operators';

import { Perfil } from '../../usuario-perfil/shared/perfil.model';
import { PerfilService } from '../../usuario-perfil/shared/perfil.service';

@Component({
  selector: 'app-usuario-form',
  templateUrl: './usuario-form.component.html',
  styleUrls: ['./usuario-form.component.scss']
})
export class UsuarioFormComponent extends BaseResourceFormComponent<UsuarioCommand> implements OnInit {
  
  perfis: Observable<Perfil[]>;

  constructor(
    protected injector: Injector,
    protected usuarioService: UsuarioService,
    protected parametrosEmpresaService: ParametrosEmpresaService,
    private notificacaoService: NotificacaoService,
    private perfilService: PerfilService
  ) {
    super(injector, new UsuarioCommand(), usuarioService, UsuarioCommand.fromJson)
  }

  ngOnInit() { 
    this.loadPerfil();

    super.ngOnInit();
  } 

  private loadPerfil() {
    this.perfis = this.perfilService.getAll();
  }

  // PROTECTED METHODS
  protected createResource() {
    const resource: UsuarioCommand = this.jsonDataToResourceFn(this.resourceForm.value || []);
    const ObjTratado = this.trataCamposFormUsuario(resource);

    this.resourceService.create(ObjTratado)
      .subscribe(
        resourceRet => this.actionsForSuccess(resourceRet),
        error => {
          this.submittingForm = false;
          this.erroService.setError(error)
        }
      );
  }

  protected loadResource() {
    if (this.currentAction === 'edit') {
      this.route.paramMap.pipe(
        switchMap(params => this.resourceService.getById(params.get('id')))
      )
        .subscribe(
          (resource) => {
            this.resource = resource;
            // console.log('recursos', resource);
            this.resourceForm.patchValue(resource); // binds load resource form
            if (resource.idPerfil === 0) {
              this.resourceForm.get('idPerfil').setValue('');
            }           
            this.resourceForm.get('ativo').setValue(TrataCheckbox.TrueFalse(resource.ativo));
          },
          (error) => this.erroService.setError(error)
        );
    }
  }

  protected updateResource() {   
    const resource: UsuarioCommand = this.jsonDataToResourceFn(this.resourceForm.value);
    const ObjTratado = this.trataCamposFormUsuario(resource);
        
    this.resourceService.update(ObjTratado)
      .subscribe(
        resourceRet => this.actionsForSuccess(resourceRet),
        error => {
          this.submittingForm = false;
          this.erroService.setError(error, ObjTratado)
        }
      );
  }

  private trataCamposFormUsuario(resource) {    
    return Object.assign(resource, {      
      ativo: TrataCheckbox.TrueFalse(<number>this.resourceForm.get('ativo').value)     
    });
    
  }

  protected buildResourceForm() {
    // codVendedorEcf
    this.resourceForm = this.formBuilder.group({
      id: ['0'],
      userName: ['', [Validators.required, Validators.minLength(3)]],
      senha: ['', [Validators.required, Validators.minLength(4)]],
      nome: ['', [Validators.required, Validators.minLength(2)]],      
      idPerfil: ['', [Validators.required]],
      ativo: ['1'],
      email: ['', [Validators.required]]   
    });
  }

  public ExcluirUsuario(usuario: UsuarioCommand) {
    this.notificacaoService.openConfirmDialog('Excluir Usuário: ' + usuario.userName + ' ?', true).afterClosed()
      .subscribe(
        (resp) => {
          if (resp) {
            this.usuarioService.delete(usuario.id).subscribe(
              (dados) => {
                this.toastr.success('Registro excluido com successo!!!');
                this.router.navigateByUrl('usuarios');
              }, (erro) => this.erroService.setError(erro, usuario)
            );
          }
        }
      );
  }

  public Novo() {
    this.submittingForm = false;
    this.router.navigateByUrl('usuarios/novo');
  }

  public Voltar() {
    this.router.navigateByUrl('usuarios');
  }

  protected creationPageTitle(): string {
    return 'Cadastro de Usuário';
  }

  protected editionPageTitle(): string {
    const usuario = this.resource.userName || '';
    return 'Editando Usuário: ' + usuario;
  }

}

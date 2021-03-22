import { Router, ActivatedRoute } from '@angular/router';
import { Component, OnInit, OnDestroy } from '@angular/core';
import { Validators, FormGroup, FormBuilder } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';

import { Perfil } from './shared/perfil.model';
import { PerfilService } from './shared/perfil.service';



import { NotificacaoService } from './../../../shared/components/notificacao/notificacao.service';
import { ErroService } from './../../../shared/services/erro.service';



@Component({
  selector: 'app-usuario-perfil',
  templateUrl: './usuario-perfil.component.html',
  styleUrls: ['./usuario-perfil.component.scss']
})
export class UsuarioPerfilComponent implements OnInit {

  resourceForm: FormGroup;

  perfil: Perfil = {};
  perfis: Perfil[];

  submittingForm = false;
  exComboMenu = false;
  exPerfilUsuario = false;

  constructor(
    private perfilService: PerfilService,
    private formBuilder: FormBuilder,
    private toastr: ToastrService,
    private erroservice: ErroService,
    private notificacaoService: NotificacaoService,
    private router: Router
  ) { }

  ngOnInit() {

     // Inicialização dos formulários
     this.buildResourceForm();
     this.loadPerfil();
  }

  public loadPerfil() {
    this.perfilService.getAll().subscribe(
      (res) => this.perfis = res,
      (erro) => this.erroservice.setError(erro)
    );
  }

  protected buildResourceForm() {
    this.resourceForm = this.formBuilder.group({
      id: [0],
      nome: ['', [Validators.required, Validators.minLength(2)]]
    });
  }

  public submitForm(form) {
    switch (form) {
      case 'PERFIL':
        this.resourceForm.valid ? this.defineAcao(form) : this.submittingForm = false;
        break;
      default:
        break;
    }
  }

  public defineAcao(form) {
    switch (form) {
      case 'PERFIL':
        this.resourceForm.get('id').value === 0 ? this.inserirPerfil() : this.atualizarPerfil();
        break;
      default:
        break;
    }
  }

  private inserirPerfil() {
    const obj = Perfil.fromJson(this.resourceForm.value);
    this.perfilService.create(obj).subscribe(
      (res) => {
        this.toastr.success('Solicitação processada com sucesso!');
        this.perfis.push(res);
        this.Reset('perfil');
      },
      (erro) => this.erroservice.setError(erro, obj)
    );
  }

  private atualizarPerfil() {
    const obj = Perfil.fromJson(this.resourceForm.value);
    this.perfilService.update(obj).subscribe(
      () => {
        this.toastr.success('Solicitação processada com sucesso!');
        this.Reset('PERFIL');
        this.loadPerfil();
      },
      (erro) => this.erroservice.setError(erro, obj)
    );
  }

  public DuplicarPerfil(perfil: Perfil) {
    this.notificacaoService.openConfirmDialog('Duplicar o Perfil: ' + perfil.nome + ' ?', true).afterClosed()
    .subscribe(
      (resp) => {
        if (resp) {
          this.perfilService.duplicar(perfil).subscribe(
            () => {
              this.toastr.success('Registro duplicado com successo!!!');
              this.loadPerfil();
            },
            (erro) => this.erroservice.setError(erro)
          );
        }
      }
    );
  }

  public ExcluirPerfil(perfil: Perfil) {
    this.notificacaoService.openConfirmDialog('Excluir o Perfil: ' + perfil.nome + ' ?', true).afterClosed()
    .subscribe(
      (resp) => {
        if (resp) {
          this.perfilService.delete(perfil.id).subscribe(
            () => {
              this.toastr.success('Registro excluido com successo!!!');
              this.loadPerfil();
            },
            (erro) => this.erroservice.setError(erro)
          );
        }
      }
    );
  }

  public Reset(form) {
    switch (form) {
      case 'PERFIL':
        this.resourceForm.reset();
        this.buildResourceForm();
        break;
      default:
        break;
    }
  }

  public Novo(form){
    switch (form) {
      case 'PERFIL':
        this.perfil = {};
        this.resourceForm.reset();
        this.buildResourceForm();
        break;
      default:
        break;
    }
  }

   // Botoes
  public BtnAtualizarPerfil(perfil: Perfil){
    this.perfil = perfil;
    this.resourceForm.patchValue(perfil);
  }

  public btnConfigurarPermissao(perfil: Perfil) {
    const id = perfil.id;
    this.router.navigate(['/usuarios-perfil', id , 'perfil'], {queryParams: {p: `${perfil.nome}`}});
  }



}

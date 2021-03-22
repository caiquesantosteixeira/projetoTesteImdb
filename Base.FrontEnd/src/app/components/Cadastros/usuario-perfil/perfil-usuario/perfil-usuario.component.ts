import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';

import { PerfilUsuarioService, TipoFiltroBusca } from './../shared/perfilUsuario.service';
import { PermissoesService } from './../../../Configuracoes/menu-opcoes/shared/permissoes.service';
import { Permissoes } from './../../../Configuracoes/menu-opcoes/shared/permissoes.model';
import { PerfilUsuario } from './../shared/perfilUsuario.model';
import { NotificacaoService } from './../../../../shared/components/notificacao/notificacao.service';
import { ErroService } from './../../../../shared/services/erro.service';

import { PerfilUsuarioBotoesService } from './../shared/perfil-usuario-botoes.service';
import { PerfilUsuarioBotoes } from './../shared/perfil-usuario-botoes.model';

import { SubMenu } from './../../../Configuracoes/menu-opcoes/shared/submenu.model';
import { Observable, from } from 'rxjs';



const Filtro = TipoFiltroBusca;

@Component({
  selector: 'app-perfil-usuario',
  templateUrl: './perfil-usuario.component.html',
  styleUrls: ['./perfil-usuario.component.scss']
})
export class PerfilUsuarioComponent implements OnInit {
  resourceForm: FormGroup;
  formPermissoes: FormGroup;

  // Paginacao e Busca
  public EndPoint = 'v1/perfisUsuario/paginacao';
  public pagina: any = 1;
  public registroExcluido = false;
  public qtdRegPagina = 15;
  public FiltroSelecionado: TipoFiltroBusca;
  public ValueFiltro = '';
  public id = 0;

  // propriedade da opcao de limpar busca
  public listarBusca = false;
  public execPesqVazia = false;
  private listaVazia = true;

  perfilUsuario: PerfilUsuario = {};
  perfisUsuario: PerfilUsuario[];
  perfisUsuBtn: PerfilUsuarioBotoes[];
  submenus: Observable<SubMenu[]>;

  public idPerfil = 0;
  public permissoes: Permissoes[];
  submittingForm = false;
  exPermissoes = false;
  public perfilLegenda = '';

  constructor(
    private perfilUsuarioService: PerfilUsuarioService,
    private formBuilder: FormBuilder,
    private router: Router,
    private route: ActivatedRoute,
    private notificacaoService: NotificacaoService,
    private erroservice: ErroService,
    private toastr: ToastrService,
    private permissoesService: PermissoesService,
    private perfilUsuarioBotoesService: PerfilUsuarioBotoesService
  ) { }

  ngOnInit() {
    this.route.queryParams.subscribe(
      (res) => this.perfilLegenda = res.p);

    this.route.url.subscribe(
      (rota) => {
        this.defineRotaAtual(rota);
      }
    );

    this.buildResourceForm();
    this.loadCbMenuOpcoes();
    this.buildFormPermissoes();
  }

  public defineRotaAtual(rota: any[]) {

    if ((rota !== undefined) && (rota !== null)) {
      const id = rota[0].path;
      const segm = rota[1].path.toString().toUpperCase();
      if (segm === 'PERFIL') {
        this.execPesqVazia = true;
        this.idPerfil = id;
        this.id = id;
      }

    }
  }

  public loadCbMenuOpcoes() {
    this.submenus = this.perfilUsuarioService.loadCbMenuOpcoes();
  }

  private buildFormPermissoes() {
    this.formPermissoes = this.formBuilder.group({
      id: [0],
      idPermissoes: ['', Validators.required],
      idPerfilUsuario: [],
      descricaoPerfil: ['']
    });
  }

  protected buildResourceForm() {
    this.resourceForm = this.formBuilder.group({
      id: [0],
      idModuloMenuOpc: ['', [Validators.required]],
      visivelMenu: [true]
    });
  }

  public DadosPaginacao(value: any) {
    if (value !== undefined && value !== null) {
      this.perfisUsuario = value.dados;
    } else {
      this.perfisUsuario = [];
    }
  }

  public PesqPerfilUsuario(event: string) {
    if (event.length > 0) {
      const pesq = event.trim();
      // tslint:disable-next-line: triple-equals
      if (pesq.length > 0) {
        this.listarBusca = true;
        this.ValueFiltro = pesq;
        if (Number(pesq)) {
          this.FiltroSelecionado = Filtro.CODIGO;
        } else {
          this.FiltroSelecionado = Filtro.NOME;
        }

        this.listaVazia = true;
      }
    }

    // Caso pressione Tab e a lista esteja zerada
    if (this.listarBusca && event.length === 0) {
      this.LimparPesquisa();
    }

  }

  // Responsavel pelo cancelamento da busca
  public LimparPesquisa() {
    this.listarBusca = false;
    this.FiltroSelecionado = Filtro.NAODEFINIDO;
    this.ValueFiltro = '';
  }

  public Voltar() {
    this.router.navigate(['/usuarios-perfil']);
  }

  public submitForm(form) {
    switch (form) {
      case 'PERFILUSUARIO':
        this.resourceForm.valid ? this.defineAcao(form) : this.submittingForm = false;
        break;
      case 'PERMISSAO':
        this.formPermissoes.valid ? this.defineAcao(form) : this.submittingForm = false;
        break;
      default:
        break;
    }
  }

  public defineAcao(form) {
    switch (form) {
      case 'PERFILUSUARIO':
        this.resourceForm.get('id').value === 0 ? this.inserirPerfilUsuario() : this.atualizarPerfil();
        break;
      case 'PERMISSAO':
        this.formPermissoes.get('id').value === 0 ? this.inserirPerfilUsuBtn() : this.atualizarPerfil();
        break;
      default:
        break;
    }
  }

  private inserirPerfilUsuario() {
    this.execPesqVazia = false;
    const obj = PerfilUsuario.fromJson(this.resourceForm.value);
    const obj2 = Object.assign(obj, {
      idPerfil: this.idPerfil
    });
    this.perfilUsuarioService.create(obj2).subscribe(
      (res) => {
        this.toastr.success('Solicitação processada com sucesso!');
        this.Reset('PERFILUSUARIO');
        this.execPesqVazia = true;
      },
      (erro) => this.erroservice.setError(erro, obj)
    );
  }

  private inserirPerfilUsuBtn() {
    const obj = PerfilUsuarioBotoes.fromJson(this.formPermissoes.value);
    this.perfilUsuarioBotoesService.create(obj).subscribe(
      (res) => {
        this.toastr.success('Solicitação processada com sucesso!');
        // console.log('Perfil', this.perfilUsuario);
        this.loadOpcoesBotoes(obj.idPerfilUsuario);
      },
      (erro) => this.erroservice.setError(erro, obj)
    );
  }

  private atualizarPerfil() {
    const obj = PerfilUsuario.fromJson(this.resourceForm.value);
    this.perfilUsuarioService.update(obj).subscribe(
      () => {
        this.toastr.success('Solicitação processada com sucesso!');
        this.Reset('PERFILUSUARIO');
        // this.perfisUsuario();
      },
      (erro) => this.erroservice.setError(erro, obj)
    );
  }

  public Reset(form) {
    switch (form) {
      case 'PERFIL':
        this.resourceForm.reset();
        this.buildResourceForm();
        break;
      case 'PERMISSAO':
        this.formPermissoes.reset();
        this.buildFormPermissoes();
        break;
      default:
        break;
    }
  }

  public btnExcluirPerfilUsu(obj: PerfilUsuario) {
    this.execPesqVazia = false;
    this.notificacaoService.openConfirmDialog('Excluir o registro: ' + obj.id + ' ?', true).afterClosed()
      .subscribe(
        (resp) => {
          if (resp) {
            this.perfilUsuarioService.delete(obj.id).subscribe(
              () => {
                this.toastr.success('Registro excluido com successo!!!');
                this.LimparPesquisa();
                this.exPermissoes = false;
                this.execPesqVazia = true;
              },
              (erro) => this.erroservice.setError(erro)
            );
          }
        }
      );
  }

  btnExPerfUsuBtn(obj: PerfilUsuarioBotoes) {
    this.notificacaoService.openConfirmDialog('Excluir o registro: ' + obj.id + ' ?', true).afterClosed()
      .subscribe(
        (resp) => {
          if (resp) {
            this.perfilUsuarioBotoesService.delete(obj.id).subscribe(
              () => {
                this.toastr.success('Registro excluido com successo!!!');
                this.loadOpcoesBotoes(obj.idPerfilUsuario);
                this.execPesqVazia = true;
              },
              (erro) => this.erroservice.setError(erro)
            );
          }
        }
      );
  }

  public loadPermissoes(IdMenuOpcoes: number) {
    this.permissoesService.GetPerMenuOpcoes(IdMenuOpcoes)
      .subscribe(
        (res) => {
          // console.log('res', res)
          this.permissoes = res
        },
        (erro) => this.erroservice.setError(erro)
      );
  }

  private loadOpcoesBotoes(idPerfilUsuario: number) {
    this.perfilUsuarioBotoesService
      .getbtnperfil(idPerfilUsuario).subscribe(
        (res) => this.perfisUsuBtn = res,
        (erro) => this.erroservice.setError(erro)
      );
  }

  // Botoes
  public btnConfigurarPermissao(obj: PerfilUsuario) {
    this.exPermissoes = true;
    this.formPermissoes.get('descricaoPerfil').setValue(obj.titulo);
    this.formPermissoes.get('idPerfilUsuario').setValue(obj.id);

    this.perfilUsuario = obj;
    this.loadPermissoes(obj.idMenuOpcoes);
    this.loadOpcoesBotoes(obj.id);
  }



}

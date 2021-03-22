import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

import { Usuario } from './../shared/usuario.model';
import { UsuarioService, TipoFiltroBusca } from './../shared/usuario.service';
import { ErroService } from './../../../../shared/services/erro.service';
import { NotificacaoService } from '../../../../shared/components/notificacao/notificacao.service';
import { ToastrService } from 'ngx-toastr';
import { TrataData } from '../../../../shared/Util/DataHora';
import { Perfil } from '../../usuario-perfil/shared/perfil.model';
import { PerfilService } from '../../usuario-perfil/shared/perfil.service';

declare var $: any;
const Filtro = TipoFiltroBusca;

@Component({
  selector: 'app-usuario-list',
  templateUrl: './usuario-list.component.html',
  styleUrls: ['./usuario-list.component.scss']
})

export class UsuarioListComponent implements OnInit {
  usuarios: Usuario[];
  usuario: Usuario;

  /// Paginacao e Busca
  public EndPoint = 'v1/usuarios/paginacao';
  public pagina: any = 1;
  public registroExcluido = false;
  public qtdRegPagina = 15;
  public FiltroSelecionado: TipoFiltroBusca;
  public ValueFiltro = '';

  // propriedade da opcao de limpar busca
  public listarBusca = false;
  private listaVazia = true;
  public perfis: Perfil[];


  constructor(
    private router: Router,
    protected usuarioService: UsuarioService,
    private erroService: ErroService,
    private notificacaoService: NotificacaoService,
    private toast: ToastrService,
    private perfilService: PerfilService
  ) { }

  ngOnInit() {
    this.loadPerfil();
  }

  // Responsavel pelo cancelamento da busca
  public LimparPesquisa() {
    this.listarBusca = false;
    this.FiltroSelecionado = Filtro.NAODEFINIDO;
    this.ValueFiltro = '';
  }

  public obterPerfil(id: number) {
    let _PERFIL = 'Não Definido';
    if ((id !== undefined) && id > 0) {
      if(this.perfis){
        const perfil = this.perfis.filter((resp) => {
          return resp.id === id;
        });
        _PERFIL = perfil[0].nome;
      }      
    }
    return _PERFIL;
  }

  private loadPerfil() {
    this.perfilService.getAll().subscribe(
      (res) => {        
        this.perfis = res
      },
      (erro) => this.erroService.setError(erro)
    );
  }

  public DadosPaginacao(value: any) {
    if (value !== undefined && value !== null) {
      this.usuarios = value.dados;
    } else {
      this.usuarios = [];
    }
  }

  // Método que controla a busca
  public consultaUsuario(event: string) {
    if (event.length > 0) {
      const pesq = event.trim();
      // tslint:disable-next-line: triple-equals
      if (pesq != this.ValueFiltro && pesq.length > 0) {
        this.listarBusca = true;
        this.ValueFiltro = pesq;
        if (Number(pesq) && pesq.length < 11) {
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

  public ExcluirUsuario(usuario: Usuario) {
    this.registroExcluido = false;
    this.notificacaoService.openConfirmDialog('Excluir Usuários: ' + usuario.login + ' ?', true).afterClosed()
      .subscribe(
        (resp) => {
          if (resp) {
            this.usuarioService.delete(usuario.id).subscribe(
              () => {
                this.registroExcluido = true;
                this.toast.success('Registro excluido com successo!!!');
              }, (erro) => {
                this.erroService.setError(erro, usuario);
              });
          }
        }
      );
  } 

  // Avança para um novo cadastro
  public Novo() {
    this.router.navigateByUrl('usuarios/novo');
  }




}

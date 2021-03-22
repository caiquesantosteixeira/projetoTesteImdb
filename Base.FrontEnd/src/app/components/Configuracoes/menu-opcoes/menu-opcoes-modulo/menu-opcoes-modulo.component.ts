import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';

import { MenuOpcoesModulo } from './../shared/menu-opcoes-modulo.model';
import { Modulo } from './../shared/modulo.model';
import { SubMenu } from './../shared/submenu.model';
import { MenuOpcoesModuloService, TipoFiltroBusca } from './../shared/menu-opcoes-modulo.service';

import { NotificacaoService } from './../../../../shared/components/notificacao/notificacao.service';
import { ErroService } from './../../../../shared/services/erro.service';

import { ToastrService } from 'ngx-toastr';

const Filtro = TipoFiltroBusca;

@Component({
  selector: 'app-menu-opcoes-modulo',
  templateUrl: './menu-opcoes-modulo.component.html',
  styleUrls: ['./menu-opcoes-modulo.component.scss']
})
export class MenuOpcoesModuloComponent implements OnInit {

  // Paginacao e Busca
  public EndPoint =  'modulomenuopcoes/paginacao';
  public pagina: any = 1;
  public registroExcluido = false;
  public qtdRegPagina = 15;
  public FiltroSelecionado: TipoFiltroBusca;
  public ValueFiltro = '';

  // propriedade da opcao de limpar busca
  public listarBusca = false;
  public execPesqVazia = false;
  private listaVazia = true;

  resourceForm: FormGroup;
  submittingForm = false;

  menuopcao: MenuOpcoesModulo = {};
  menuOpcoes: MenuOpcoesModulo[];
  modulos: Modulo[];
  SubMenus: SubMenu[];

  constructor(
    private formBuilder: FormBuilder,
    private menuOpcoesModuloService: MenuOpcoesModuloService,
    private notificacaoService: NotificacaoService,
    private erroService: ErroService,
    private toastr: ToastrService
    ) { }

  ngOnInit() {
    this.buildResourceForm();
    this.loadCbModulo();
    this.loadcbMenuOpcoes();
  }

  private loadCbModulo() {
    this.menuOpcoesModuloService.cbModulo().subscribe(
      (res) => this.modulos = res,
      (erro) => this.erroService.setError(erro)
    );
  }

  private loadcbMenuOpcoes() {
    this.menuOpcoesModuloService.cbMenuOpcoes().subscribe(
      (res) => this.SubMenus = res,
      (erro) => this.erroService.setError(erro)
    );
  }

  public DadosPaginacao(value: any) {
    if (value !== undefined && value !== null) {
      this.menuOpcoes = value.dados;
    } else {
      this.menuOpcoes = [];
    }
  }

  // Responsavel pelo cancelamento da busca
  public LimparPesquisa() {
    this.listarBusca = false;
    this.FiltroSelecionado = Filtro.NAODEFINIDO;
    this.ValueFiltro = '';
  }

  public PesqModuloSubMenu(event: string) {
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
    if(this.listarBusca && event.length ===  0) {
      this.LimparPesquisa();
    }

  }

  public Reset(form) {
    switch (form) {
      case 'MENU_OPCOES_MODULO':
        this.resourceForm.reset();
        this.buildResourceForm();
        break;
      default:
        break;
    }
  }

  public Novo(form){
    switch (form) {
      case 'MENU_OPCOES_MODULO':
        this.menuopcao = {};
        this.Reset('MENU_OPCOES_MODULO');
        break;
      default:
        break;
    }
  }

  protected buildResourceForm() {
    this.resourceForm = this.formBuilder.group({
      id: [0],
      idModulo: ['', [Validators.required]],
      idMenuOpcoes: ['', [Validators.required]],
    });
  }

  public submitForm(form) {
    switch (form) {
      case 'MENU_OPCOES_MODULO':
        this.resourceForm.valid ? this.defineAcao(form) : this.submittingForm = false;
        break;
      default:
        break;
    }
  }

  public defineAcao(form) {
    switch (form) {
      case 'MENU_OPCOES_MODULO':
        this.resourceForm.get('id').value === 0 ? this.inserirOpcModulo() : this.atualizarOpcModulo();
        break;
      default:
        break;
    }
  }

  private inserirOpcModulo() {
    this.execPesqVazia = false;
    const obj = MenuOpcoesModulo.fromJson(this.resourceForm.value);
    this.menuOpcoesModuloService.create(obj).subscribe(
      () => {
        this.toastr.success('Solicitação processada com sucesso!');
        // this.Reset('MENU_OPCOES_MODULO');

        // Recarrega a lista
        this.LimparPesquisa();
        this.execPesqVazia = true;
      },
      (erro) => this.erroService.setError(erro, obj)
    );
  }

  private atualizarOpcModulo() {
    const obj = MenuOpcoesModulo.fromJson(this.resourceForm.value);
    this.menuOpcoesModuloService.update(obj).subscribe(
      () => {
        this.toastr.success('Solicitação processada com sucesso!');
        this.Reset('MENU_OPCOES_MODULO');
      },
      (erro) => this.erroService.setError(erro, obj)
    );
  }


  // Botoes
  public btnExcluirMenuOpc(mom: MenuOpcoesModulo) {
    this.execPesqVazia = false;
    this.notificacaoService.openConfirmDialog('Excluir o registro: ' + mom.id + ' ?', true).afterClosed()
    .subscribe(
      (resp) => {
        if (resp) {
          this.menuOpcoesModuloService.delete(mom.id).subscribe(
            () => {
              this.toastr.success('Registro excluido com successo!!!');
              this.LimparPesquisa();
              this.execPesqVazia = true;
            },
            (erro) => this.erroService.setError(erro)
          );
        }
      }
    );
  }

}

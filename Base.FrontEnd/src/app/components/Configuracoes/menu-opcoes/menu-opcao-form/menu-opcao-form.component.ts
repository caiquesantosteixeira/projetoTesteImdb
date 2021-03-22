import { Component, OnInit, OnChanges, SimpleChanges } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

import { MenuService } from './../shared/menu.service';
import { Menu } from './../shared/menu.model';
import { ErroService } from './../../../../shared/services/erro.service';
import { NotificacaoService } from './../../../../shared/components/notificacao/notificacao.service';

import { SubMenuService, TipoFiltroBusca } from './../shared/submenu.service';
import { SubMenu } from './../shared/submenu.model';

import { MenuOpcoesBotoesService } from '../shared/menu-opcoes-botoes.service';
import { MenuOpcoesBotoes } from '../shared/menu-opcoes-botoes.model';

import { PermissoesService } from '../shared/permissoes.service';
import { Permissoes } from '../shared/permissoes.model';
import { Router, ActivatedRoute } from '@angular/router';

const Filtro = TipoFiltroBusca;

@Component({
  selector: 'app-menu-opcao-form',
  templateUrl: './menu-opcao-form.component.html',
  styleUrls: ['./menu-opcao-form.component.scss']
})
export class MenuOpcaoFormComponent implements OnInit {
  // Paginacao e Busca
  public EndPoint = 'v1/menuopcoes/paginacao';
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
  formOpcoes: FormGroup;
  formPermissoes: FormGroup;
  menu: Menu = {};
  menus: Menu[];
  submenu: SubMenu = {};
  submenus: SubMenu[];
  resource: Menu;
  permissoes: Permissoes[];
  permissao: Permissoes;
  menuOpcoesBotoes: MenuOpcoesBotoes[];
  menuOpcoesBotao: MenuOpcoesBotoes;
  submittingForm = false;
  exComboMenu = false;
  exPermissoes = false;

  constructor(
    private menuService: MenuService,
    private formBuilder: FormBuilder,
    private toastr: ToastrService,
    private erroservice: ErroService,
    private notificacaoService: NotificacaoService,
    private subMenuService: SubMenuService,
    private menuOpcoesBotoesService: MenuOpcoesBotoesService,
    private permissoesService: PermissoesService,
    private router: Router,
    private route: ActivatedRoute
  ) {

  }

  ngOnInit() {

    // Carregamento atraves de resolver
    this.route.data
      .subscribe(
        (res: { menu: Menu[] }) => this.menus = res.menu
      );

    // this.loadMenu();
    // Inicialização dos formulários
    this.buildResourceForm();
    this.buildFormOpcoesMenu();
    this.buildFormPermissoes();
  }


  public loadMenu() {
    this.menuService.getAll().subscribe(
      (res) => this.menus = res,
      (erro) => this.erroservice.setError(erro)
    );
  }

  public loadPermissoes() {
    this.permissoesService.getAll().subscribe(
      (res) => this.permissoes = res,
      (erro) => this.erroservice.setError(erro)
    );
  }

  public loadOpcoesBotoes(id: number) {
    this.menuOpcoesBotoesService.getOpcoesDefinidaById(id)
      .subscribe(
        (res) => this.menuOpcoesBotoes = res,
        (erro) => this.erroservice.setError(erro)

      );
  }

  public obterMenu(id) {
    let _MENU = 'Não Definido';
    if (id !== null) {
      const menu = this.menus.filter((resp) => {
        return resp.id === id;
      });
      _MENU = menu[0].menu1;
    }
    return _MENU;
  }

  public DadosPaginacao(value: any) {
    if (value !== undefined && value !== null) {
      this.submenus = value.dados;
    } else {
      this.submenus = [];
    }
  }

  // Responsavel pelo cancelamento da busca
  public LimparPesquisa() {
    this.listarBusca = false;
    this.FiltroSelecionado = Filtro.NAODEFINIDO;
    this.ValueFiltro = '';
  }

  public PesqSubMenu(event: string) {
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

  public Reset(form) {
    switch (form) {
      case 'MENU':
        this.resourceForm.reset();
        this.buildResourceForm();
        break;
      case 'SUBMENU':
        this.formOpcoes.reset();
        this.buildFormOpcoesMenu();
        break;
      default:
        break;
    }
  }

  public Novo(form) {
    switch (form) {
      case 'MENU':
        this.menu = {};
        this.resourceForm.reset();
        this.buildResourceForm();
        break;
      case 'SUBMENU':
        this.submenu = {};
        this.exComboMenu = false;
        this.formOpcoes.reset();
        this.buildFormOpcoesMenu();
        break;
      default:
        break;
    }
  }

  public ChangeInputMenu(value) {
    this.exComboMenu = false;
    this.formOpcoes.get('idMenu').setValue('');
    if (Number(value) && value > 0) {
      this.exComboMenu = true;
    }
  }

  protected buildResourceForm() {
    this.resourceForm = this.formBuilder.group({
      id: [0],
      menu1: ['', [Validators.required, Validators.minLength(2)]],
      icone: ['', [Validators.required]],
      ordem: [0, [Validators.pattern('[0-9]')]]
    });
  }

  private buildFormOpcoesMenu() {
    this.formOpcoes = this.formBuilder.group({
      id: [0],
      titulo: ['', [Validators.required, Validators.minLength(2)]],
      ativo: [true, [Validators.required]],
      visivelMenu: [true, [Validators.required]],
      slugPermissao: ['', [Validators.required, Validators.minLength(2)]],
      pathUrl: ['', [Validators.required]],
      subMenu: [0],
      idMenu: ['']
    });
  }

  private buildFormPermissoes() {
    this.formPermissoes = this.formBuilder.group({
      id: [0],
      idPermissoes: ['', Validators.required],
      idMenuOpcoes: [],
      descricaoSubMenu: []
    });
  }

  private setDadosPermissoes(submenu: SubMenu) {
    this.formPermissoes.get('idMenuOpcoes').setValue(submenu.id);
    this.formPermissoes.get('descricaoSubMenu').setValue(submenu.titulo);
  }

  public submitForm(form) {
    switch (form) {
      case 'MENU':
        this.resourceForm.valid ? this.defineAcao(form) : this.submittingForm = false;
        break;
      case 'SUBMENU':
        this.formOpcoes.valid ? this.defineAcao(form) : this.submittingForm = false;
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
      case 'MENU':
        this.resourceForm.get('id').value === 0 ? this.inserirMenu() : this.atualizarMenu();
        break;
      case 'SUBMENU':
        this.formOpcoes.get('id').value === 0 ? this.inserirSubMenu() : this.atualizarSubMenu();
        break;
      case 'PERMISSAO':
        this.formPermissoes.get('id').value === 0 ? this.inserirPermissao() : this.submittingForm = false;
        break;
      default:
        break;
    }
  }

  private inserirMenu() {
    const obj = Menu.fromJson(this.resourceForm.value);
    this.menuService.create(obj).subscribe(
      (res) => {
        this.toastr.success('Solicitação processada com sucesso!');
        this.menus.push(res);
        this.Reset('MENU');
      },
      (erro) => this.erroservice.setError(erro, obj)
    );
  }

  private inserirSubMenu() {
    this.execPesqVazia = false;
    const obj = SubMenu.fromJson(this.formOpcoes.value);
    this.subMenuService.create(obj).subscribe(
      (res) => {
        this.toastr.success('Solicitação processada com sucesso!');
        this.Reset('SUBMENU');
        // Recarrega a lista
        this.LimparPesquisa();
        this.execPesqVazia = true;
      },
      (erro) => this.erroservice.setError(erro, obj)
    );
  }

  private inserirPermissao() {
    const obj = MenuOpcoesBotoes.fromJson(this.formPermissoes.value);
    // console.log(obj);
    // return false;
    this.menuOpcoesBotoesService.create(obj).subscribe(
      (res) => {
        this.toastr.success('Solicitação processada com sucesso!');
        this.loadOpcoesBotoes(obj.idMenuOpcoes);
      },
      (erro) => this.erroservice.setError(erro, obj)
    );
  }

  private atualizarMenu() {
    const obj = Menu.fromJson(this.resourceForm.value);
    this.menuService.update(obj).subscribe(
      () => {
        this.toastr.success('Solicitação processada com sucesso!');
        this.Reset('MENU');
        this.loadMenu();
      },
      (erro) => this.erroservice.setError(erro, obj)
    );
  }

  private atualizarSubMenu() {
    this.execPesqVazia = false;
    const obj = SubMenu.fromJson(this.formOpcoes.value);
    this.subMenuService.update(obj).subscribe(
      (res) => {
        this.toastr.success('Solicitação processada com sucesso!');
        this.Reset('SUBMENU');
        // Recarrega a lista
        this.LimparPesquisa();
        this.execPesqVazia = true;
      },
      (erro) => this.erroservice.setError(erro, obj)
    );
  }

  public ExcluirMenu(menu: Menu) {
    this.notificacaoService.openConfirmDialog('Excluir o menu: ' + menu.menu1 + ' ?', true).afterClosed()
      .subscribe(
        (resp) => {
          if (resp) {
            this.menuService.delete(menu.id).subscribe(
              () => {
                this.toastr.success('Registro excluido com successo!!!');
                this.loadMenu();
              },
              (erro) => this.erroservice.setError(erro)
            );
          }
        }
      );
  }

  public btnExcluirSubmenu(subMenu: SubMenu) {
    this.execPesqVazia = false;
    this.notificacaoService.openConfirmDialog('Excluir o submenu: ' + subMenu.titulo + ' ?', true).afterClosed()
      .subscribe(
        (resp) => {
          if (resp) {
            this.subMenuService.delete(subMenu.id).subscribe(
              () => {
                this.toastr.success('Registro excluido com successo!!!');
                this.LimparPesquisa();
                this.execPesqVazia = true;
              },
              (erro) => this.erroservice.setError(erro)
            );
          }
        }
      );
  }

  public btnExMenuOpcBotoes(menuOpcoesBotao: MenuOpcoesBotoes) {
    this.notificacaoService.openConfirmDialog('Excluir a permissão: ' + menuOpcoesBotao.id + ' ?', true).afterClosed()
      .subscribe(
        (resp) => {
          if (resp) {
            this.menuOpcoesBotoesService.delete(menuOpcoesBotao.id).subscribe(
              () => {
                this.toastr.success('Registro excluido com successo!!!');
                this.loadOpcoesBotoes(menuOpcoesBotao.idMenuOpcoes);
              },
              (erro) => this.erroservice.setError(erro)
            );
          }
        }
      );
  }


  // Botoes
  public BtnAtualizarMenu(menu: Menu) {
    this.menu = menu;
    this.resourceForm.patchValue(menu);
  }

  public btnEditarSubmenu(subMenu: SubMenu) {
    this.exComboMenu = false;
    this.submenu = subMenu;
    this.formOpcoes.patchValue(subMenu);
    // console.log(subMenu);
    if (subMenu.subMenu === 1) {
      this.exComboMenu = true;
    }
  }

  public btnConfigurarPermissao(submenu: SubMenu) {
    this.exPermissoes = true;
    this.setDadosPermissoes(submenu);
    this.loadPermissoes();
    this.loadOpcoesBotoes(submenu.id);
  }

  public BtnOpcoesModulo() {
    this.router.navigate(['/menuopcoes', 'menu-opcoes-modulo']);
  }



}

import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

import { MenuService } from './../../../../shared/services/menu.service';
import { Menu } from './../../../../shared/models/menu.model';
import { ErroService } from './../../../../shared/services/erro.service';
import { LoginService } from './../../../../components/Autenticacao/login/shared/login.service';
import { ChavesStorage } from '../../../../shared/models/chaves-storage';

declare var $: any;
//Metadata
export interface RouteInfo {
  path: string;
  title: string;
  type: string;
  icontype: string;
  // icon: string;
  children?: ChildrenItems[];
}

export interface ChildrenItems {
  path: string;
  title: string;
  ab: string;
  type?: string;
}

//Exemplo do Array
/*export const ROUTES: RouteInfo[] = [{
  path: '/',
  title: 'Cadastros',
  type: 'sub',
  icontype: 'pe-7s-plus',
  children: [
    { path: 'clientes', title: 'Clientes', ab: 'pe-7s-angle-right' },
    { path: 'fornecedores', title: 'Fornecedores', ab: 'pe-7s-angle-right' },
    { path: 'produtos', title: 'Produtos', ab: 'pe-7s-angle-right' },
    { path: 'grupos', title: 'Grupos', ab: 'pe-7s-angle-right' },
    { path: 'linha', title: 'Linhas', ab: 'pe-7s-angle-right' },
    { path: 'subgrupo', title: 'Subgrupos', ab: 'pe-7s-angle-right' },
    { path: 'sessoes', title: 'Sessões', ab: 'pe-7s-angle-right' },
    { path: 'empresa', title: 'Empresa', ab: 'pe-7s-angle-right' },
    { path: 'tributacao', title: 'Tributação', ab: 'pe-7s-angle-right' },
    { path: 'naturezas', title: 'Naturezas', ab: 'pe-7s-angle-right' },
    { path: 'usuarios', title: 'Usuários', ab: 'pe-7s-angle-right' },
    { path: 'usuarios-perfil', title: 'Usuários-Perfil', ab: 'pe-7s-angle-right' }
  ]
}, {
  path: '/',
  title: 'Vendas',
  type: 'sub',
  icontype: 'pe-7s-cart',
  children: [
    { path: 'pedidos', title: 'Pedido de Venda', ab: 'pe-7s-angle-right' },
    { path: 'buttons', title: 'Emissão de NFe', ab: 'pe-7s-angle-right' },
    { path: 'pdv', title: 'PDV', ab: 'pe-7s-angle-right' }
  ]
},
{
  path: '/',
  title: 'Configurações',
  type: 'sub',
  icontype: 'pe-7s-config',
  children: [
    { path: 'menuopcoes', title: 'Menu de Opções', ab: 'pe-7s-angle-right' }
  ]
}
];*/


@Component({
  selector: 'app-sidebar',
  templateUrl: './sidebar.component.html',
  styleUrls: ['./sidebar.component.css']
})
export class SidebarComponent implements OnInit {

  public menuItems: any[];
  public menuItemsSer: Menu[];

  isNotMobileMenu() {
    if ($(window).width() > 991) {
      return false;
    }
    return true;
  }

  constructor(
    private router: Router,
    private menuService: MenuService,
    private erroService: ErroService,
    private loginService: LoginService
  ) { }

  ngOnInit() {
    var isWindows = navigator.platform.indexOf('Win') > -1 ? true : false;

    const MENU = JSON.parse(localStorage.getItem(ChavesStorage.Menu));
    if (MENU === null) {
      this.loginService.logout();
    } else {
      this.menuItems = MENU.filter(menuItem => menuItem);
    }   

    isWindows = navigator.platform.indexOf('Win') > -1 ? true : false;

    if (isWindows) {
      // if we are on windows OS we activate the perfectScrollbar function
      $('.sidebar .sidebar-wrapper, .main-panel').perfectScrollbar();
      $('html').addClass('perfect-scrollbar-on');
    } else {
      $('html').addClass('perfect-scrollbar-off');
    }
  }
  

  Sair() {
    this.loginService.logout();
  }

  ngAfterViewInit() {
    var $sidebarParent = $('.sidebar .nav > li.active .collapse li.active > a').parent().parent().parent();

    var collapseId = $sidebarParent.siblings('a').attr("href");

    $(collapseId).collapse("show");
  }

}

import { Router } from '@angular/router';
import { Component, OnInit, Renderer2, ViewChild, ElementRef } from '@angular/core';
import { Location } from '@angular/common';
import { LoginService } from './../../../../components/Autenticacao/login/shared/login.service';
import { ChavesStorage } from '../../../../shared/models/chaves-storage';

var misc: any = {
  navbar_menu_visible: 0,
  active_collapse: true,
  disabled_collapse_init: 0,
}
declare var $: any;

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {

  private listTitles: any[];
  location: Location;
  private nativeElement: Node;
  private toggleButton;
  private sidebarVisible: boolean;
  public dadosUsuario: any = {};

  @ViewChild("navbar-cmp") button;

  constructor(
    location: Location,
    private renderer: Renderer2,
    private element: ElementRef,
    private router: Router,
    private loginService: LoginService
  ) {
    this.location = location;
    this.nativeElement = element.nativeElement;
    this.sidebarVisible = false;
  }

  ngOnInit() {
    // Carrega objetos do Usuário
    this.dadosUsuario = JSON.parse(localStorage.getItem(ChavesStorage.Usuario));

    // this.listTitles = ROUTES.filter(listTitle => listTitle);

    var navbar: HTMLElement = this.element.nativeElement;
    this.toggleButton = navbar.getElementsByClassName('navbar-toggle')[0];
    if ($('body').hasClass('sidebar-mini')) {
      misc.sidebar_mini_active = true;
    }
    $('#minimizeSidebar').click(function () {
      var $btn = $(this);

      if (misc.sidebar_mini_active == true) {
        $('body').removeClass('sidebar-mini');
        misc.sidebar_mini_active = false;
      } else {
        setTimeout(function () {
          $('body').addClass('sidebar-mini');
          misc.sidebar_mini_active = true;
        }, 300);
      }

      // we simulate the window Resize so the charts will get updated in realtime.
      var simulateWindowResize = setInterval(function () {
        window.dispatchEvent(new Event('resize'));
      }, 180);

      // we stop the simulation of Window Resize after the animations are completed
      setTimeout(function () {
        clearInterval(simulateWindowResize);
      }, 1000);
    });
  }

  Sair() {
    this.loginService.logout();
  }

  isMobileMenu() {

    if ($(window).width() < 991) {
      return false;
    }
    return true;
  }

  sidebarOpen() {
    var toggleButton = this.toggleButton;
    var body = document.getElementsByTagName('body')[0];
    setTimeout(function () {
      toggleButton.classList.add('toggled');
    }, 500);
    body.classList.add('nav-open');
    this.sidebarVisible = true;
  }
  sidebarClose() {
    var body = document.getElementsByTagName('body')[0];
    this.toggleButton.classList.remove('toggled');
    this.sidebarVisible = false;
    body.classList.remove('nav-open');
  }
  sidebarToggle() {
    // var toggleButton = this.toggleButton;
    // var body = document.getElementsByTagName('body')[0];
    if (this.sidebarVisible == false) {
      this.sidebarOpen();
    } else {
      this.sidebarClose();
    }
  }

  getTitle(): string {

    let titulo = 'SGE - Sistema de Gestão Empresarial - 3.0';

    if ($(window).width() < 991) {
      titulo = 'SGE -  JAMSOFT';
    }
    return titulo;
  }

  getPath() {
    // console.log(this.location);
    return this.location.prepareExternalUrl(this.location.path());
  }


}

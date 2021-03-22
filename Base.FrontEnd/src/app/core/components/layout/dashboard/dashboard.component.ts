import { Component, OnInit, ViewChild } from '@angular/core';
import { Router, NavigationEnd, ActivatedRoute } from '@angular/router';
import { Subscription } from 'rxjs';
import { filter } from 'rxjs/operators';
import { Location } from '@angular/common';

import { NavbarComponent } from '../navbar/navbar.component';

import { Menu } from './../../../../shared/models/menu.model';
import { ChavesStorage } from '../../../../shared/models/chaves-storage';

declare var $: any;

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {

  location: Location;
  private _router: Subscription;
  // url: string;

  @ViewChild('sidebar') sidebar;
  @ViewChild(NavbarComponent) navbar: NavbarComponent;
  constructor(private router: Router, location: Location, private route: ActivatedRoute) {
    this.location = location;
  }

  ngOnInit() {
    this.route.data.subscribe(
      (res: { itensMenu: Menu[] }) => {
        if (res.itensMenu !== undefined) {
          localStorage.setItem(ChavesStorage.Menu, JSON.stringify(res.itensMenu));
        }
      });

    this._router = this.router.events.pipe(filter(event => event instanceof NavigationEnd)).subscribe(event => {
      //   this.url = event.url;
      this.navbar.sidebarClose();
    });

    var isWindows = navigator.platform.indexOf('Win') > -1 ? true : false;
    if (isWindows) {
      // if we are on windows OS we activate the perfectScrollbar function
      var $main_panel = $('.main-panel');
      $main_panel.perfectScrollbar();
      $main_panel.scrollTop();
    }

  }
  public isMap() {
    if (window.location.pathname.indexOf("/maps/fullscreen") !== -1) {
      return true;
    } else {
      return false;
    }
  }
}

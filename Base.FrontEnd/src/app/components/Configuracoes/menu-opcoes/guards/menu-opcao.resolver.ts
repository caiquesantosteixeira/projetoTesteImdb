import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, RouterStateSnapshot, Resolve } from '@angular/router';
import { Observable, of } from 'rxjs';

import { Menu } from '../shared/menu.model';
import { MenuService } from '../shared/menu.service';
@Injectable({
  providedIn: 'root'
})
export class MenuOpcaoResolver implements Resolve<Menu> {

  constructor(protected menuService: MenuService) {}

  resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<any> {
    return this.menuService.getAll();
  }
}

import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, RouterStateSnapshot, Resolve } from '@angular/router';
import { Observable, of } from 'rxjs';

import { MenuService } from '../../shared/services/menu.service';
import { Menu } from '../../shared/models/menu.model';
import { ChavesStorage } from '../../shared/models/chaves-storage';
import { UserToken } from '../../components/Autenticacao/login/shared/login.model';

@Injectable({
  providedIn: 'root'
})
export class MenuResolver implements Resolve<Menu> {

  constructor(protected menuService: MenuService) {}

  resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<any> {

    const MENU = JSON.parse(localStorage.getItem(ChavesStorage.Menu));
    if ((MENU === undefined) || MENU === null) {
      let id = 0;
      const dadosUsuario = localStorage.getItem(ChavesStorage.Usuario);
      if(dadosUsuario){
        const dados = <UserToken> JSON.parse(dadosUsuario);
        id = dados.idPerfil;
      }  
      return this.menuService.getMenus(id);
    }
  }
}

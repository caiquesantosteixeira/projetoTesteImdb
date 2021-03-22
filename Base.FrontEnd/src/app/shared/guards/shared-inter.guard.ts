import { ActivatedRouteSnapshot, RouterStateSnapshot, Router, CanActivate } from '@angular/router';
import { Observable } from 'rxjs';
import { Injectable } from '@angular/core';
import { JwtInfo } from './../Util/Jwt-info';
import { ToastrService } from 'ngx-toastr';

@Injectable()

export class SharedInterGuard implements CanActivate {


  constructor(
    private router: Router,
    private toast: ToastrService
  ) {

  }

  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ): Observable<boolean> | Promise<boolean> | boolean {
    const url = state.url.toUpperCase();
    const jwt = new JwtInfo();
    if (route.data.role !== undefined) {
      const opc: string = route.data.role.opcao !== undefined ? route.data.role.opcao : '';
      const perm: string = route.data.role.permissao !== undefined ? route.data.role.permissao : '';
      if (opc !== '') {
        let liberar = false;
        const arr = jwt.Roles.filter((elemet) => {
          // console.log('opc', opc, 'elem', elemet.opcao, ' -> ', perm, ' perm -> ', elemet.permissao);
          if (elemet.opcao === opc.toUpperCase() && elemet.permissao === perm.toUpperCase()) {
            liberar = true;
            return true;
          }
        });

        if (!liberar) {
          this.toast.error('Acesso negado!');
          this.router.navigate(['/']);
          return false;
        }
      } else {
        return true;
      }
    }
    return true;
  }

}

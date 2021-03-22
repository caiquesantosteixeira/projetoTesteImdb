
import { CanActivateChild, ActivatedRouteSnapshot, RouterStateSnapshot, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { Injectable } from '@angular/core';
import { JwtInfo } from './../../shared/Util/Jwt-info';
import { ToastrService } from 'ngx-toastr';

@Injectable()

export class AuthChildGuard implements CanActivateChild {
  constructor(
    private router: Router,
    private toast: ToastrService
  ) {

  }

  canActivateChild(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ): Observable<boolean> | Promise<boolean> | boolean {
    const url = state.url.split('/');
    //console.log('pai', route.data.role);
    const jwt = new JwtInfo();
    if (route.data.role !== undefined) {
      const opc: string = route.data.role.opcao !== undefined ? route.data.role.opcao : '';
      const perm: string = route.data.role.permissao !== undefined ? route.data.role.permissao : '';
      // console.log('opcao', jwt);
      if (opc !== '') {
        let liberar = false;
        const arr = jwt.Roles.filter((elemet) => {
          // console.log('opc', opc, 'elem', elemet.opcao);
          if (elemet.opcao === opc.toUpperCase() && (elemet.permissao === perm.toUpperCase() || perm === '')) {
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

    // Permite
    return true;
  }

}

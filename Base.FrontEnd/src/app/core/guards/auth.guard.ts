import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree, CanActivate, Router } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import { ChavesStorage } from './../../shared/models/chaves-storage';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {
  private jwtHelper = new JwtHelperService();

  constructor(private router: Router) { }
  canActivate(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): boolean {

    if (localStorage.getItem(ChavesStorage.Usuario)) {
      // const token = this.jwtHelper.decodeToken(JSON.parse(localStorage.getItem(ChavesStorage.Token)));
      return true;
    } else {
      this.router.navigate(['/autenticacao']);
      return false;
    }
  }

}

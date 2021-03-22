import { Router } from '@angular/router';
import { Injectable } from '@angular/core';
import { HttpInterceptor, HttpEvent, HttpRequest, HttpHandler, HttpResponse } from '@angular/common/http';
import { Observable, of, throwError } from 'rxjs';
import { tap } from 'rxjs/internal/operators/tap';

import { ErroService } from '../../shared/services/erro.service';
import { LoginService } from '../../components/Autenticacao/login/shared/login.service';
import { ChavesStorage } from '../../shared/models/chaves-storage';


@Injectable({ providedIn: 'root' })

export class AuthInterceptor implements HttpInterceptor {
  // private jwtHelper = new JwtHelperService();
  isRefresToken = false;

  constructor(
    private router: Router,
    private erroService: ErroService,
    private loginService: LoginService) { }

  public Deslogar() {
    this.loginService.logout();
    this.router.navigate(['/autenticacao']);
  }

  protected handleError(err: any): Observable<any> {
    console.log('codigo 2', err.status, 'erro', err.error.message);
    return throwError(err);
  }

  intercept(
    req: HttpRequest<any>,
    next: HttpHandler): Observable<HttpEvent<any>> {
    
    const token = localStorage.getItem(ChavesStorage.Token);  
    if (token) {
      // console.log(token);
      const URL = JSON.parse(token);
      const cloneReq = req.clone({
        headers: req.headers.set('Authorization', `Bearer ${URL}`)
      });

      return next.handle(cloneReq)
        .pipe(
          tap(
            (event: HttpEvent<any>) => {
              if (event instanceof HttpResponse) {
                // console.log(event);
              }
            },
            err => {
              // console.log('codigo', err, 'erro', req.headers);
              if (err.status !== 500 && err.status !== 0 && err.status !== 400 && err.status !== 404) {
                this.erroService.setError(err);
                this.Deslogar();
              }
            }
          )
        );
    } else {
      return next.handle(req.clone())
        .pipe(
          tap((res) => { },
            err => {
              // console.log('codigo 2', err.status, 'erro', err);
              if (err.status !== 500 && err.status !== 400 && err.status !== 404) {
                this.erroService.setError(err);
                this.Deslogar();
              }
            })
        );
    }
  }

}

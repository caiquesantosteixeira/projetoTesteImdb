import { Injectable } from '@angular/core';

import { Observable, throwError } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { UrlapiService } from './urlapi.service';
import { catchError, retry, map } from 'rxjs/operators';

import {Menu} from './../models/menu.model';
import { Retorno } from '../models/retornos';

@Injectable({
  providedIn: 'root'
})
export class MenuService {

  constructor(private http: HttpClient, private urlApi: UrlapiService) {

  }

  public getMenus(idPerfil: number): Observable<Menu[]> {
    return this.http.get<Menu[]>(`${this.urlApi.URL_WEB_SERVICE}v1/perfisUsuario/permissoesperfil/${idPerfil}`)
    .pipe(
      map((e: any) => {
        var resp = <Retorno>e;
        if(resp.sucesso){
          const objeto = e.data.menuItens
          // console.log(objeto);
          return objeto;
        }
        return null;
      }),
      retry(2),
      catchError(this.handleError)
    );
  }  

  protected handleError(error: any): Observable<any> {
    console.log('Erro na requisição => ' + error);
    return throwError(error);
  }

}

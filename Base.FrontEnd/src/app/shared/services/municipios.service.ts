import { Injectable } from '@angular/core';

import { Observable, throwError } from 'rxjs';
import { HttpClient } from '@angular/common/http';

import { Municipios } from './../models/municipios';
import { UrlapiService } from './urlapi.service';
import {  map, catchError, tap, take, retry } from 'rxjs/operators';


@Injectable({
  providedIn: 'root'
})
export class MuncipiosService {

  constructor(private http: HttpClient, private urlApi: UrlapiService) {

  }

  public getMunicipios(uf: string): Observable<Municipios[]> {
    return this.http.get<Municipios[]>(`${this.urlApi.URL_WEB_SERVICE}municipios/get/${uf}`)
    .pipe(
      retry(2),
      catchError(this.handleError)
    );

  }

  protected handleError(error: any): Observable<any> {
    console.log('Erro na requisição => ' + error);
    return throwError(error);
  }

}

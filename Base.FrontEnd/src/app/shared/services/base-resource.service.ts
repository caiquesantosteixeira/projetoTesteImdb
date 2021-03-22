import { BaseResourceModel } from '../models/base-resource.model';

import { HttpClient, HttpParams } from '@angular/common/http';
import { Injector } from '@angular/core';

import { Observable, throwError } from 'rxjs';
import { map, catchError } from 'rxjs/operators';

export abstract class BaseResourceService<T extends BaseResourceModel> {

  protected http: HttpClient;
  constructor(
    protected apiPath: string,
    protected injector: Injector,
    protected jsonDataToResourceFn: (jsonData: any) => T
  ) {
    // recebe uma instancia e injeta na classe
    // com apenas um injector pode ser usado para instancias varias service
    this.http = injector.get(HttpClient);   
  }

  //public getRegistrosPage(EndPoint: string, paginaAtual: number, qtdRegistros: number, Filtro?: string, FiltroValue?: string) { };

  getAll(): Observable<T[]> {
    return this.http.get(this.apiPath).pipe(
      map(this.jsonDataToResources.bind(this)),
      catchError(this.handleError)
    );
  }

  getById(id: any): Observable<T> {
    const url = `${this.apiPath}/${id}`;
    return this.http.get(url).pipe(
      map(this.jsonDataToResource.bind(this)),
      catchError(this.handleError)
    );
  }
  

  create(resource: T): Observable<T> {
    return this.http.post(this.apiPath, resource).pipe(
      map(this.jsonDataToResource.bind(this)),
      catchError(this.handleError)
    );
  }

  update(resource: T): Observable<T> {
    const url = `${this.apiPath}/${resource.id}`;
    return this.http.put(url, resource).pipe(
      map(() => resource),
      catchError(this.handleError)
    );
  }

  updateReturnAny(resource: T): Observable<any> {
    const url = `${this.apiPath}/${resource.id}`;
    return this.http.put<any>(url, resource);
  }

  delete(id: number): Observable<any> {
    const url = `${this.apiPath}/${id}`;
    return this.http.delete(url).pipe(
      catchError(this.handleError)
    );
  }

  public getRegGenericObject(EndPoint: string, ParametroName: string, ParametroValue: string): Observable<T> {
    return this.http.get(this.apiPath + EndPoint,
      {
        params: new HttpParams().set(ParametroName, ParametroValue)
      }).pipe(
        map(this.jsonDataToResource.bind(this)),
        catchError(this.handleError)
      );
  }

  public getRegGenericList(EndPoint: string, ParametroName: string, ParametroValue: string): Observable<T[]> {
    return this.http.get(this.apiPath + EndPoint,
      {
        params: new HttpParams().set(ParametroName, ParametroValue)
      }).pipe(
        map(this.jsonDataToResources.bind(this)),
        catchError(this.handleError)
      );
  }

  public getRegistrosPage(EndPoint: string, paginaAtual: number, qtdRegistros: number, Filtro?: string, FiltroValue?: string, id = null): Observable<any[]> {
    FiltroValue = FiltroValue === undefined ? '' : FiltroValue;
    Filtro = Filtro === undefined ? 'NAODEFINIDO' : Filtro;
    return this.http.get<any[]>
      (`${this.apiPath + EndPoint}`,
        {
          params: new HttpParams()
            .set('QtdPorPagina', qtdRegistros.toString())
            .set('PagAtual', paginaAtual.toString())
            .set('Filtro', Filtro)
            .set('ValueFiltro', FiltroValue)
        });
  }

  //protected ConsultaTributacao(tributacao: T){};

  // PROTECTED METHODS
  protected jsonDataToResources(jsonData: any): T[] {  
    const resources: T[] = [];
    jsonData.data.forEach(
      element => resources.push(this.jsonDataToResourceFn(element)));   
    return resources;
  }

  protected jsonDataToResource(jsonData: any): T {
    //console.log('teste', jsonData);
    return this.jsonDataToResourceFn(jsonData.data);
  }

  protected handleError(error: any): Observable<any> {
    console.log('Erro na requisição => ' + error);
    return throwError(error);
  }


}

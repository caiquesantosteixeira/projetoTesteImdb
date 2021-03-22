import { Injectable, Injector } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { MenuOpcoesBotoes } from './menu-opcoes-botoes.model';
import { BaseResourceService } from '../../../../shared/services/base-resource.service';
import { UrlapiService } from '../../../../shared/services/urlapi.service';
import { Observable } from 'rxjs';
import { map, catchError } from 'rxjs/operators';


@Injectable({
  providedIn: 'root'
})
export class MenuOpcoesBotoesService extends BaseResourceService<MenuOpcoesBotoes> {
  constructor(protected injector: Injector, private urlApi: UrlapiService, protected http: HttpClient ) {
    super(`${urlApi.URL_WEB_SERVICE}v1/menuopcoesbotoes`, injector, MenuOpcoesBotoes.fromJson);
  }

  getOpcoesDefinidaById(id: number): Observable<MenuOpcoesBotoes[]> {
    const url = `${this.urlApi.URL_WEB_SERVICE}v1/menuopcoesbotoes/${id}`;
    return this.http.get<MenuOpcoesBotoes[]>(url).pipe(
      map(this.jsonDataToResources.bind(this)),
      catchError(this.handleError)
    );
  }

}

import { Observable } from 'rxjs';
import { Injectable, Injector } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { MenuOpcoesModulo } from './menu-opcoes-modulo.model';
import { BaseResourceService } from '../../../../shared/services/base-resource.service';
import { UrlapiService } from '../../../../shared/services/urlapi.service';

export enum TipoFiltroBusca {
  NOME = 'NOME',
  CODIGO = 'CODIGO',
  NAODEFINIDO = ''
}

@Injectable({
  providedIn: 'root'
})
export class MenuOpcoesModuloService extends BaseResourceService<MenuOpcoesModulo> {
  constructor(protected injector: Injector, private urlApi: UrlapiService, protected http: HttpClient ) {
    super(`${urlApi.URL_WEB_SERVICE}modulomenuopcoes`, injector, MenuOpcoesModulo.fromJson);
  }

  public cbModulo(): Observable<any[]> {
    return this.http.get<any[]>(`${this.urlApi.URL_WEB_SERVICE}modulo`);
  }

  public cbMenuOpcoes(): Observable<any[]> {
    return this.http.get<any[]>(`${this.urlApi.URL_WEB_SERVICE}menuopcoes`);
  }

}

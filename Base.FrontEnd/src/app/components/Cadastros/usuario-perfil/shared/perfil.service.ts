import { Injectable, Injector } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { Perfil } from './perfil.model';
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
export class PerfilService extends BaseResourceService<Perfil> {

  constructor(protected injector: Injector, private urlApi: UrlapiService, protected http: HttpClient ) {
    super(`${urlApi.URL_WEB_SERVICE}v1/perfil`, injector, Perfil.fromJson);
  }

  public duplicar(obj: Perfil){
    return this.http.post<Perfil>(`${this.urlApi.URL_WEB_SERVICE}v1/perfil/duplicar`,obj);
  }

}

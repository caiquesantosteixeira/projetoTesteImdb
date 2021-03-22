import { Injectable, Injector } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { Menu } from './menu.model';
import { BaseResourceService } from '../../../../shared/services/base-resource.service';
import { UrlapiService } from '../../../../shared/services/urlapi.service';

export enum TipoFiltroBusca {
  NOME = 'NOME',
  CPFCNPJ = 'CPFCNPJ',
  CODIGO = 'CODIGO',
  NAODEFINIDO = ''
}

@Injectable({
  providedIn: 'root'
})
export class MenuService extends BaseResourceService<Menu> {

  constructor(protected injector: Injector, private urlApi: UrlapiService, protected http: HttpClient ) {
    super(`${urlApi.URL_WEB_SERVICE}v1/menu`, injector, Menu.fromJson);
  }

}

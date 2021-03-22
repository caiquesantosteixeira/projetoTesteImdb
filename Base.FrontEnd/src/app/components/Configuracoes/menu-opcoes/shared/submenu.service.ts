import { Injectable, Injector } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { SubMenu } from './submenu.model';
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
export class SubMenuService extends BaseResourceService<SubMenu> {

  constructor(protected injector: Injector, private urlApi: UrlapiService, protected http: HttpClient ) {
    super(`${urlApi.URL_WEB_SERVICE}v1/menuOpcoes`, injector, SubMenu.fromJson);
  }

}

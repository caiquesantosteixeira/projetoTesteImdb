import { Observable } from 'rxjs';
import { Injectable, Injector } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { PerfilUsuario } from './perfilUsuario.model';
import { BaseResourceService } from '../../../../shared/services/base-resource.service';
import { UrlapiService } from '../../../../shared/services/urlapi.service';
import { map } from 'rxjs/operators';
import { Retorno } from '../../../../shared/models/retornos';


export enum TipoFiltroBusca {
  NOME = 'NOME',
  CODIGO = 'CODIGO',
  NAODEFINIDO = ''
}

@Injectable({
  providedIn: 'root'
})
export class PerfilUsuarioService extends BaseResourceService<PerfilUsuario> {

  constructor(protected injector: Injector, private urlApi: UrlapiService, protected http: HttpClient ) {
    super(`${urlApi.URL_WEB_SERVICE}v1/perfisusuario`, injector, PerfilUsuario.fromJson);
  }

  public loadCbMenuOpcoes(): Observable<any[]> {
    return this.http.get<any[]>(`${this.urlApi.URL_WEB_SERVICE}v1/menuopcoes/OpcoesEmpresa`)
    .pipe(
      map((response: any) => {
        var resp = <Retorno>response;
        if(resp.sucesso){
          const objeto = <any[]> resp.data;            
          return objeto;
        }
        return null;          
      })
    );
  }

}

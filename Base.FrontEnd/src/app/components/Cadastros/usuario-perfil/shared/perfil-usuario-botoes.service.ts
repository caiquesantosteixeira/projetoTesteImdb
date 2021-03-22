import { Observable } from 'rxjs';
import { Injectable, Injector } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { PerfilUsuarioBotoes } from './perfil-usuario-botoes.model';
import { BaseResourceService } from '../../../../shared/services/base-resource.service';
import { UrlapiService } from '../../../../shared/services/urlapi.service';
import { Retorno } from '../../../../shared/models/retornos';
import { map } from 'rxjs/operators';


@Injectable({
  providedIn: 'root'
})
export class PerfilUsuarioBotoesService extends BaseResourceService<PerfilUsuarioBotoes> {

  constructor(protected injector: Injector, private urlApi: UrlapiService, protected http: HttpClient ) {
    super(`${urlApi.URL_WEB_SERVICE}v1/perfilusuariobotoes`, injector, PerfilUsuarioBotoes.fromJson);
  }

  public getbtnperfil(idPerfilUsu: any): Observable<any[]>{
    return this.http.get<any[]>(`${this.urlApi.URL_WEB_SERVICE}v1/perfilusuariobotoes/getbtnperfil/${idPerfilUsu}`)
    .pipe(
      map((response: any) => {
        var resp = <Retorno>response;
        if(resp.sucesso){
          return  <any[]> resp.data;          
        }
        return null;          
      })
    );
  }

}

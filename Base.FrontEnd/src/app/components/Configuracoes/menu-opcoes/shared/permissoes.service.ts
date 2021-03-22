import { Observable } from 'rxjs';
import { Injectable, Injector } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { Permissoes } from './permissoes.model';
import { BaseResourceService } from '../../../../shared/services/base-resource.service';
import { UrlapiService } from '../../../../shared/services/urlapi.service';
import { map } from 'rxjs/operators';
import { Retorno } from '../../../../shared/models/retornos';


@Injectable({
  providedIn: 'root'
})
export class PermissoesService extends BaseResourceService<Permissoes> {
  constructor(protected injector: Injector, private urlApi: UrlapiService, protected http: HttpClient ) {
    super(`${urlApi.URL_WEB_SERVICE}v1/permissoes`, injector, Permissoes.fromJson);
  }

  public GetPerMenuOpcoes(IdMenuOpcoes: number): Observable<any[]> {
    return this.http.get<any[]>(`${this.urlApi.URL_WEB_SERVICE}v1/permissoes/getpermenuopcoes/${IdMenuOpcoes}`)
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

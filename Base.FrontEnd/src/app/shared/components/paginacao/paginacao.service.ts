import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';

import { UrlapiService } from '../../services/urlapi.service';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { Retorno } from '../../models/retornos';


@Injectable({
  providedIn: 'root'
})
export class PaginacaoService {

  private url: any = '';

  constructor(private urlApi: UrlapiService, protected http: HttpClient) {
    this.url = this.urlApi.URL_WEB_SERVICE;
  }

  public getRegistrosPage(EndPoint: string, paginaAtual: number, qtdRegistros: number,
    Filtro?: string, FiltroValue?: string, id = 0): Observable<any[]> {
    FiltroValue = FiltroValue === undefined ? '' : FiltroValue;
    Filtro = Filtro === undefined ? 'NAODEFINIDO' : Filtro;
    return this.http.get<any[]>
      (`${this.url + EndPoint}`,
        {
          params: new HttpParams()
            .set('QtdPorPagina', qtdRegistros.toString())
            .set('PagAtual', paginaAtual.toString())
            .set('Filtro', Filtro)
            .set('ValueFiltro', FiltroValue)
            .set('id', id.toString())
        })
        .pipe(
          map((response: any) => {
            var resp = <Retorno> response;
            if(resp.sucesso){
              //const objeto = UsuarioLogado.fromJson(resp.data);            
              return <any[]> resp.data;
            }
            return null;          
          })
        );
  }

}

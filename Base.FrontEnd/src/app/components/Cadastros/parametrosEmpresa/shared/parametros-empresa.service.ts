import { map } from 'rxjs/operators';
import { Injectable, Injector, ɵConsole } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { ParametrosEmpresa } from './parametros-empresa.model';
import { BaseResourceService } from '../../../../shared/services/base-resource.service';
import { UrlapiService } from '../../../../shared/services/urlapi.service';
import { JwtInfo } from '../../../../shared/Util/Jwt-info';


export enum TipoFiltroBusca {
  EMPRESA = 'EMPRESA',
  CPFCNPJ = 'CPFCNPJ',
  CODEMPRESA = 'CODEMPRESA',
  NAODEFINIDO = ''
}

@Injectable({
  providedIn: 'root'
})
export class ParametrosEmpresaService extends BaseResourceService<ParametrosEmpresa> {

  private jwtInfo = new JwtInfo();

  constructor(protected injector: Injector, private urlApi: UrlapiService, protected http: HttpClient) {
    super(`${urlApi.URL_WEB_SERVICE}cadempresa`, injector, ParametrosEmpresa.fromJson);
  }

  public getAllCb(): ParametrosEmpresa[] {
    const EMPRESAS = JSON.parse(localStorage.getItem('SGFempresas'));
    const arr: ParametrosEmpresa[] = [];
    if (EMPRESAS != null) {
      EMPRESAS.map((a: any) => {
        const obj: any = {};
        obj.codEmpresa = a.id;
        obj.empresa = a.slogEmpresa;
        arr.push(obj);
      });
    }
    return arr;
  }

  public dadosEmpUser() {
    const codEmp = this.jwtInfo.CodEmpresa;
    const EMPRESAS = JSON.parse(localStorage.getItem('SGFempresas'));
    let arr: any = {};
    if (EMPRESAS != null) {
      EMPRESAS.filter((a: any) => {
        if (a.id === codEmp) {
          arr = a;
        }
      });
    }
    return arr;
  }

  public getCbEmpUser(): ParametrosEmpresa[] {
    this.jwtInfo = new JwtInfo();
    const codEmp = this.jwtInfo.CodEmpresa;
    const EMPRESAS = JSON.parse(localStorage.getItem('SGFempresas'));
    // console.log(EMPRESAS);
    const arr: ParametrosEmpresa[] = [];
    if (EMPRESAS != null) {
      EMPRESAS.map((a: any) => {
        if (a.id === codEmp) {
          const obj: any = {};
          obj.codEmpresa = a.id;
          obj.empresa = a.slogEmpresa;
          obj.estado = a.estado;
          obj.regime = a.regime;
          obj.vendeNegativo = a.vendeNegativo;
          arr.push(obj);
        }
      });
    }
    return arr;
  }

}


import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { UrlapiService } from './urlapi.service';
import { ToastrService } from 'ngx-toastr';
import { timeout } from 'rxjs/operators';
import { isArray } from 'jquery';

@Injectable({
  providedIn: 'root'
})
export class ErroService {
  private empresa: any = {};

  constructor(
    private http: HttpClient,
    private urlApi: UrlapiService,
    private toastr: ToastrService
  ) {

  }


  public setError(error: any, obj = null) {
    //console.log('erro', error);

    switch (error.status) {
      case 400:
        //this.toastr.error(error.error.data.erros);
        this.toastr.error(this.trataErros(error.error))
        break;
      case 401:
        this.toastr.error('Não autorizado!');
        break;
      case 403:
        this.toastr.error('Permissão negada!');
        break;
      case 500:
        // console.log('Erro service -> ', error.error, 'obj -> ', obj);
        this.MonitorError(error.error, obj);
        this.toastr.error('Erro de processamento do servidor.');
        break;
      default:
        // console.log('Erro service -> ', error.error, 'obj -> ', obj);
        this.toastr.error('Erro não catalogado.');
        break;
    }
  }

  private trataErros(notificacao: any){    
    var retorno="";
    if(notificacao.mensagem != undefined && notificacao.sucesso == undefined){
      retorno = notificacao.mensagem; 
    }else if(notificacao.data){
      var err = notificacao.data.erros;      
      if(isArray(err)){
        err.forEach(el => {
          if(el.message != undefined){
            retorno += el.message+"\n";
          }else if(el.description != undefined){
            retorno += el.description+" \n\n";
          }        
        });
      }else{
        retorno = err
      }
    }else if(notificacao.errors != undefined){
      retorno = "Erro não catalogado.";
    }
    
    return retorno;
  }

  private MonitorError(erro: any, obj2: string) {
    this.http.get('assets/WebService.json', { responseType: 'json' })
      .subscribe(res => {

        const local = JSON.parse(localStorage.getItem('SGFempresas'));
        if (local !== null) {
          this.empresa = local[0];
        }

        let cnpj = this.empresa.cnpjCpf;
        if (cnpj == null) {
          cnpj = '000000000000';
        }

        const obj: any = {
          cnpjCpf: cnpj,
          sistema: 2,
          logErro: JSON.stringify(erro) + ' - \n objDados -> ' + JSON.stringify(obj2)
        };

        const url = res[0].UrlWebServicelogErro;
        this.http.post(`${url}api/monitorerro`, obj)
          .pipe(
            timeout(3000)
          )
          .subscribe((resp2: any) => { },
            (error) => console.log(error)
          );
      }, (error) => console.log(error)
      );
  }

}

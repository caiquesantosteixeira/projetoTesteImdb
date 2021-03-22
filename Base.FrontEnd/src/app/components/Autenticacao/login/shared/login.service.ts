import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

import { UrlapiService } from '../../../../shared/services/urlapi.service';
import { Login, UsuarioLogado } from './login.model';
import { map, take } from 'rxjs/operators';
import { Observable } from 'rxjs';
import { ChavesStorage } from '../../../../shared/models/chaves-storage';
import { Retorno } from './../../../../shared/models/retornos';

import { Router } from '@angular/router';

const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json', 'responseType': 'text' })
}

export enum TipoFiltroBusca {
  NOME = 'NOME',
  CPFCNPJ = 'CPFCNPJ',
  CODIGO = 'CODIGO',
  NAODEFINIDO = ''
}

const fromJson = Login.fromJson;

@Injectable({
  providedIn: 'root'
})
export class LoginService {
 
  constructor(
    private urlApi: UrlapiService,
    protected http: HttpClient,    
    private router: Router
  ) {

  }

  login(login: any): Observable<UsuarioLogado> {
    return this.http.post<UsuarioLogado>(`${this.urlApi.URL_WEB_SERVICE}v1/usuarios/logar`, login, httpOptions)
      .pipe(
        take(1),
        map((response: any) => {
          var resp = <Retorno>response;
          if(resp.sucesso){
            const objeto = UsuarioLogado.fromJson(resp.data);            
            return objeto;
          }
          return null;          
        })
      );
  }

  logout() {
    localStorage.removeItem(ChavesStorage.Token);
    localStorage.removeItem(ChavesStorage.Usuario); 
    localStorage.removeItem(ChavesStorage.Menu); 
    this.router.navigate(['/autenticacao']);
  }
  
  // Manipulacao do localstorage
  public setLocalStorage(dados: UsuarioLogado){
    this.excluirLocalStorage();
    localStorage.setItem(ChavesStorage.Token, JSON.stringify(dados.accesToken))
    localStorage.setItem(ChavesStorage.Usuario, JSON.stringify(dados.userToken))
  }

  public excluirLocalStorage(){
    localStorage.removeItem(ChavesStorage.Token);
    localStorage.removeItem(ChavesStorage.Usuario);
  }

  public getLocalStorage(): UsuarioLogado{
    let user:UsuarioLogado = null;
    const token = localStorage.getItem(ChavesStorage.Token);
    if(token){
      user = new UsuarioLogado();
      user.accesToken = JSON.parse(token);
      user.userToken = JSON.parse(localStorage.getItem(ChavesStorage.Usuario));
    }  
    return user;
  }


}

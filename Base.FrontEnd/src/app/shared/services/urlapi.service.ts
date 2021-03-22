import { catchError } from 'rxjs/operators';
import { Observable, of } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { ChavesStorage } from '../models/chaves-storage';

@Injectable({
  providedIn: 'root'
})
export class UrlapiService {
  public URL_WEB_SERVICE;
  public URLS: any = {};

  constructor(
    private http: HttpClient,
    private router: Router
  ) {

    this.loadUrls();
   }

  private setUrl(): Observable<any> {
    return this.http.get('assets/WebService.json', { responseType: 'json' })
      .pipe(
        catchError(err => of([err]))
      );
  }

  public getUrl() {
    this.setUrl().subscribe(
      (data) => {
        this.URLS = { local: data[0].UrlWebServiceLocal, externa: data[0].UrlWebServiceExterna };
        this.defineUrl();
      },
      (erro) => console.log('Erro ao definir Url', erro.error)
    );
  }

  private loadUrls() {
    const URL = JSON.parse(localStorage.getItem(ChavesStorage.UrlApi));
    this.URL_WEB_SERVICE = URL === null ? '' : URL;
  }

  private validaUrlApi(urlAtual: string) {
    const urlSalva = JSON.parse(localStorage.getItem(ChavesStorage.UrlApi));
    if ((urlSalva !== undefined) && urlSalva !== null) {
      if (urlSalva !== urlAtual) {
        // desloga
        //localStorage.removeItem('SGFdadosUsuario');
        //localStorage.removeItem('SGFmenu');
        //localStorage.removeItem('SGFempresas');
        //localStorage.removeItem('SGFsessao');
        localStorage.removeItem(ChavesStorage.UrlApi);

        this.router.navigate(['/autenticacao']);
      }
    }
  }

  private defineUrl() {
    const urlAtual = window.location.href;
    const UrlAtualObj = urlAtual.split('/');
    const FragmentUrl = UrlAtualObj[2].toUpperCase();

    // locale url
    const urlInterna = this.URLS.local;
    const urlInternaObj = urlInterna.split('/');

    // possiveis url
    const urlLocal = 'LOCALHOST';
    const urlLocalServer = 'JAMSERVER';

    if (FragmentUrl.search(urlInternaObj[2]) > -1
      || FragmentUrl.search(urlLocal) > -1
      || FragmentUrl.search(urlLocalServer) > -1) {
      this.validaUrlApi(this.URLS.local);
      localStorage.setItem(ChavesStorage.UrlApi, JSON.stringify(this.URLS.local));
    } else {
      this.validaUrlApi(this.URLS.externa);
      localStorage.setItem(ChavesStorage.UrlApi, JSON.stringify(this.URLS.externa));
    }

    // Carrega a Url
    this.loadUrls();
  }

}

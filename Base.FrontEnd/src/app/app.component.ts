
import { Component, OnInit, OnDestroy, AfterViewChecked } from '@angular/core';
import { UrlapiService } from './shared/services/urlapi.service';

import { Router } from '@angular/router';

import { NgxUiLoaderService } from 'ngx-ui-loader';



@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit, OnDestroy {


  title = 'Jamsoft Lite Incialmente';
  EnvURLS: any = {};
  constructor(
    private url: UrlapiService,
    private ngxService: NgxUiLoaderService,
    private router: Router
  ) {
    this.url.getUrl();
  }

  ngOnInit(): void {
  }

  ngOnDestroy(): void {
  }

}

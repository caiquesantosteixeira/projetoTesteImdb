import { NgModule } from '@angular/core';

// Definicao da localidade da aplicacao
import { LOCALE_ID } from '@angular/core';
import { registerLocaleData } from '@angular/common';
import localePt from '@angular/common/locales/pt';
registerLocaleData(localePt, 'pt');

import { SharedModule } from './shared/shared.module';
import { CoreModule } from './core/core.module';
import { ComponentesModule } from './shared/modules/componentes.module';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { DashboardComponent } from './core/components/layout/dashboard/dashboard.component';
import { AutenticacaoComponent } from './components/Autenticacao/autenticacao.component';

import { HTTP_INTERCEPTORS } from '@angular/common/http';

// Guardioes
import { AuthInterceptor } from './core/guards/auth.interceptor';
import { AuthChildGuard } from './core/guards/auth-child.guard';
import { SharedInterGuard } from './shared/guards/shared-inter.guard';

import {
  NgxUiLoaderModule, NgxUiLoaderHttpModule,
  NgxUiLoaderConfig, POSITION, SPINNER, PB_DIRECTION
} from 'ngx-ui-loader';


const ngxUiLoaderConfig: NgxUiLoaderConfig = {
  bgsColor: 'red',
  bgsPosition: POSITION.bottomCenter,
  bgsSize: 40,
  bgsType: SPINNER.rectangleBounce, // background spinner type
  fgsType: SPINNER.chasingDots, // foreground spinner type
  pbDirection: PB_DIRECTION.leftToRight, // progress bar direction
  pbThickness: 5, // progress bar thickness
};

@NgModule({
  declarations: [
    AppComponent,
    AutenticacaoComponent,
    DashboardComponent
  ],
  imports: [
    CoreModule,
    SharedModule,
    AppRoutingModule,
    NgxUiLoaderModule.forRoot(ngxUiLoaderConfig),
    NgxUiLoaderHttpModule,
    ComponentesModule
  ],
  exports: [
    ComponentesModule
  ],
  providers: [
    { provide: LOCALE_ID, useValue: 'pt-BR' },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthInterceptor,
      multi: true
    },
    AuthChildGuard,
    SharedInterGuard
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }

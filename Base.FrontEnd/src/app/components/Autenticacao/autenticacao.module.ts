
import { NgModule } from '@angular/core';

import { LoginComponent } from './login/login.component';
import { SharedModule } from './../../shared/shared.module';
import { AutenticacaoRoutingModule } from './autenticacao.routing';
import { NgxMaskModule, IConfig } from 'ngx-mask';
import { RegistrarComponent } from './registrar/registrar.component';
export const options: Partial<IConfig> = null;

@NgModule({
  declarations: [LoginComponent, RegistrarComponent],
  imports: [
    SharedModule,
    AutenticacaoRoutingModule,
    NgxMaskModule.forChild(options)
  ]
})
export class AutenticacaoModule { }

import { NgModule } from '@angular/core';

import { SharedModule } from './../../../shared/shared.module';
import { MenuOpcaoFormComponent } from './menu-opcao-form/menu-opcao-form.component';
import { MenuOpcoesModuloComponent } from './menu-opcoes-modulo/menu-opcoes-modulo.component';
import { MenuOpcoesRoutingModule } from './menu-opcoes.routing.module';


@NgModule({
  declarations: [
    MenuOpcaoFormComponent,
    MenuOpcoesModuloComponent
  ],
  imports: [
    SharedModule,
    MenuOpcoesRoutingModule
  ]
})
export class MenuOpcoesModule { }

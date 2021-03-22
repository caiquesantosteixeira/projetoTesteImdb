import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';


import { MenuOpcaoFormComponent } from './menu-opcao-form/menu-opcao-form.component';
import { MenuOpcoesModuloComponent } from './menu-opcoes-modulo/menu-opcoes-modulo.component';

import { MenuOpcaoResolver } from './guards/menu-opcao.resolver';

const routes: Routes = [
  { path: '', component: MenuOpcaoFormComponent, resolve: {menu : MenuOpcaoResolver} },
  { path: 'menu-opcoes-modulo', component: MenuOpcoesModuloComponent}
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class MenuOpcoesRoutingModule {

}

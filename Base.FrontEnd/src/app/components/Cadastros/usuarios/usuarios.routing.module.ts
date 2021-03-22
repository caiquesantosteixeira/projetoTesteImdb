import { CarregamentoRotasResolver } from './../../../shared/guards/carregamento-rotas.resolver';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { UsuarioListComponent } from './usuario-list/usuario-list.component';
import { UsuarioFormComponent } from './usuario-form/usuario-form.component';

import { SharedInterGuard } from './../../../shared/guards/shared-inter.guard';

const routes: Routes = [
  { path: '', component: UsuarioListComponent },
  {
    path: 'novo',
    canActivate: [SharedInterGuard],
    data: { role: { opcao: 'CADUSUARIOS', permissao: 'ADICIONAR' } },
    component: UsuarioFormComponent
  },
  { path: ':id/edit', component: UsuarioFormComponent }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class UsuariosRoutingModule {

}

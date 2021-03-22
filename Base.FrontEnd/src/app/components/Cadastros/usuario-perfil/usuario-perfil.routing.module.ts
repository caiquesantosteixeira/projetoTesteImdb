import { CarregamentoRotasResolver } from './../../../shared/guards/carregamento-rotas.resolver';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { UsuarioPerfilComponent } from './usuario-perfil.component';
import { PerfilUsuarioComponent } from './perfil-usuario/perfil-usuario.component';

const routes: Routes = [
  { path: '', component: UsuarioPerfilComponent },
  { path: 'novo', component: UsuarioPerfilComponent },
  { path: ':id/perfil', component: PerfilUsuarioComponent }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class UsuariosPerfilRoutingModule {

}

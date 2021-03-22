
import { NgModule } from '@angular/core';
import { SharedModule } from '../../../shared/shared.module';

import { UsuarioPerfilComponent } from './usuario-perfil.component';
import { UsuariosPerfilRoutingModule } from './usuario-perfil.routing.module';
import { PerfilUsuarioComponent } from './perfil-usuario/perfil-usuario.component';
@NgModule({
  declarations: [
    UsuarioPerfilComponent,
    PerfilUsuarioComponent
  ],
  imports: [
    SharedModule,
    UsuariosPerfilRoutingModule,
  ]
})
export class UsuariosPerfilModule { }

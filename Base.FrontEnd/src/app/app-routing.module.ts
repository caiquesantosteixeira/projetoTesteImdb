import { AuthGuard } from './core/guards/auth.guard';
import { NgModule } from '@angular/core';
import { Routes, RouterModule, ExtraOptions } from '@angular/router';

import { DashboardComponent } from './core/components/layout/dashboard/dashboard.component';
import { AutenticacaoComponent } from './components/Autenticacao/autenticacao.component';
import { MenuResolver } from './core/guards/menu.resolver';
import { AuthChildGuard } from './core/guards/auth-child.guard';

const routerOptions: ExtraOptions = {
  useHash: false,
  anchorScrolling: 'enabled',
  scrollPositionRestoration: 'enabled'
};

const routes: Routes = [{
  path: '',
  redirectTo: 'dashboard',
  pathMatch: 'full',
}, {
  path: '',
  component: DashboardComponent,
  canActivate: [AuthGuard],
  resolve: { itensMenu: MenuResolver },
  canActivateChild: [AuthChildGuard],
  children: [
    { path: '',
      loadChildren: () => import('./components/PainelPrincipal/informativos/informativos.module').then(m => m.InformativosModule)
    },
    {
      path: 'usuarios',
      data: { role: { opcao: 'cadUsuarios', permissao: '' } },
      loadChildren: () => import('./components/Cadastros/usuarios/usuarios.module').then(m => m.UsuariosModule)
    },
    {
      path: 'usuarios-perfil',
      data: { role: { opcao: 'cadUsuPerfil', permissao: '' } },
      loadChildren: () => import('./components/Cadastros/usuario-perfil/usuario-perfil.module').then(m => m.UsuariosPerfilModule)
    },
    { path: 'menuopcoes',
      loadChildren: () => import('./components/Configuracoes/menu-opcoes/menu-opcoes.module').then(m => m.MenuOpcoesModule)
     },
    { path: '**', redirectTo: 'pagina-nao-encontrada' }
  ]
}, {
  path: 'autenticacao',
  component: AutenticacaoComponent,
  children: [
    { path: '',
    loadChildren: () => import('./components/Autenticacao/autenticacao.module').then(m => m.AutenticacaoModule)
   }
  ]
}
];

@NgModule({
  imports: [RouterModule.forRoot(routes, routerOptions)],
  exports: [RouterModule]
})
export class AppRoutingModule { }

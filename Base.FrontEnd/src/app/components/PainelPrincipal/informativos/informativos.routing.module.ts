import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { InformativosComponent } from './informativos.component';


const routes: Routes = [
  {path: '', component: InformativosComponent}
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class InformativosRoutingModule { }

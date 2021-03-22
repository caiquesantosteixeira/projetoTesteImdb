import { NgModule } from '@angular/core';
import { InformativosComponent } from './informativos.component';
import { SharedModule } from '../../../shared/shared.module';
import { NgxMaskModule, IConfig } from 'ngx-mask';

import { InformativosRoutingModule } from './informativos.routing.module';
export const options: Partial<IConfig> = null;


@NgModule({
  declarations: [
    InformativosComponent
  ],
  imports: [
    SharedModule,
    InformativosRoutingModule,
    NgxMaskModule.forChild(options)
  ]
})
export class InformativosModule { }

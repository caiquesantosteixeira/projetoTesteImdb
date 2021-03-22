import { NgModule } from '@angular/core';

import { FixedpluginComponent } from './fixedplugin.component';
import { SharedModule } from '../../../../shared/shared.module';

@NgModule({
  declarations: [FixedpluginComponent],
  imports: [
    SharedModule
  ],
  exports: [FixedpluginComponent]
})
export class FixedpluginModule { }

import { NgModule } from '@angular/core';

import { CommonModule } from '@angular/common';
// import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { RouterModule } from '@angular/router';
import { ReactiveFormsModule } from '@angular/forms';
import { FormsModule } from '@angular/forms';
import { IMaskModule } from 'angular-imask';
import { ToastrModule} from 'ngx-toastr';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';

import { MatDialogModule} from '@angular/material/dialog';

import { NgxMaskModule } from 'ngx-mask';
import { BrMaskerModule } from 'br-mask';
import { MatIconModule } from '@angular/material/icon';
import { PaginacaoComponent } from './components/paginacao/paginacao.component';
import { NotificacaoComponent } from './components/notificacao/notificacao.component';
import { JsPrintDirective } from './Directives/js-print.directive';
import { LayoutModule } from '@angular/cdk/layout';

import { AppLengthPipe } from './pipes/length.pipe';
import { JsPermissoesDirective } from './Directives/js-permissoes.directive';

@NgModule({
  declarations: [
    NotificacaoComponent,
    PaginacaoComponent,
    JsPrintDirective,
    JsPermissoesDirective,
    AppLengthPipe
  ],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    RouterModule,
    FormsModule,
    ToastrModule.forRoot({
      timeOut: 2000
    }),
    IMaskModule,
    NgbModule,
    NgxMaskModule.forRoot({}),
    BrMaskerModule,
    MatDialogModule,
    MatIconModule,
    LayoutModule
  ],
  exports: [
    CommonModule,
    ReactiveFormsModule,
    RouterModule,
    FormsModule,
    ToastrModule,
    IMaskModule,
    NgbModule,
    MatDialogModule,
    BrMaskerModule,
    MatIconModule,
    PaginacaoComponent,
    JsPrintDirective,
    JsPermissoesDirective,
    LayoutModule,
    AppLengthPipe
  ],
  entryComponents: [
    NotificacaoComponent,
    PaginacaoComponent
  ],
  providers: [
  ]
})
export class SharedModule { }

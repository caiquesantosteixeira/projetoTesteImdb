import { NotificacaoComponent } from './notificacao.component';
import { MatDialog } from '@angular/material/dialog';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class NotificacaoService {

  constructor(private dialog: MatDialog) { }

  openConfirmDialog(msg: string, YesNo: boolean, NameBtnYes = 'Sim', NameBtnNo = 'Não') {
    return this.dialog.open(NotificacaoComponent, {
      width: '390px',
      panelClass: 'confirm-dialog-container',
      disableClose: true,
      data: {
        mensagem: msg,
        yesno: YesNo,
        nameBtnYes: NameBtnYes,
        nameBtnNo: NameBtnNo
      }
    });
  }
}

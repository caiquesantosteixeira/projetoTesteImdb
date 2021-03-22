import { Directive, HostListener, Input, OnInit, ElementRef } from '@angular/core';
import { NG_VALUE_ACCESSOR } from '@angular/forms';
import { JwtInfo } from './../Util/Jwt-info';

@Directive({
  selector: '[appJsPermissao]',
  providers: [{
    provide: NG_VALUE_ACCESSOR,
    useExisting: JsPermissoesDirective,
    multi: true
  }]
})
export class JsPermissoesDirective implements OnInit {

  @Input() permissao: string;
  @Input() opcao: string;
  @Input() appJsPermissao: any;


  constructor(private el: ElementRef) {
  }

  ngOnInit(): void {
    // console.log('permissao', this.permissao, ' - ', 'opcao', this.opcao);
    const jsPerm = this.appJsPermissao;


    if (jsPerm !== undefined) {
      let opc: string = jsPerm.opcao !== undefined ? jsPerm.opcao : '';
      opc = opc.toUpperCase();
      let perm: string = jsPerm.permissao !== undefined ? jsPerm.permissao : '';
      perm = perm.toUpperCase();

      const ocultar: boolean = jsPerm.ocultar !== undefined ? jsPerm.ocultar : false;

      const jwt = new JwtInfo();
      if (opc !== '') {
        if (ocultar) {
          this.el.nativeElement.disabled = 'disabled';
        } else {
          this.el.nativeElement.style.display = 'none';
        }
        const arr = jwt.Roles.filter((elemet) => {
          // console.log('opc', opc, 'elem', elemet.opcao, ' -> ', perm, ' perm -> ', elemet.permissao);
          if (elemet.opcao === opc.toUpperCase() && elemet.permissao === perm.toUpperCase()) {
            if (ocultar) {
              this.el.nativeElement.disabled = '';
            } else {
              this.el.nativeElement.style.display = 'inline';
            }
          }
        });
      }
    }

  }


}

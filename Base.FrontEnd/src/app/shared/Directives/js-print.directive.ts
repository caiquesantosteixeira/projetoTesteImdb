// https://github.com/selemxmn/ngx-print
// Crédito do  NGX PRINT
import { Directive, HostListener, Input } from '@angular/core';
import { NG_VALUE_ACCESSOR } from '@angular/forms';

@Directive({
  selector: 'button[appNgxPrint]',
  providers: [{
    provide: NG_VALUE_ACCESSOR,
    useExisting: JsPrintDirective,
    multi: true
  }]
})
export class JsPrintDirective {

  public _printStyle = [];

  @Input() printSectionId: string;

  @Input() printTitle: string;

  @Input() useExistingCss = false;

  @Input()
  set printStyle(values: { [key: string]: { [key: string]: string } }) {
    for (let key in values) {
      if (values.hasOwnProperty(key)) {
        this._printStyle.push((key + JSON.stringify(values[key])).replace(/['"]+/g, ''));
      }
    }
    this.returnStyleValues();
  }

  public returnStyleValues() {
    return `<style> ${this._printStyle.join(' ').replace(/,/g, ';')} </style>`;
  }

  private _styleSheetFile = '';

  @Input()
  set styleSheetFile(cssList: string) {
    let linkTagFn = cssFileName =>
      `<link rel="stylesheet" type="text/css" href="${cssFileName}">`;
    if (cssList.indexOf(',') !== -1) {
      const valueArr = cssList.split(',');
      for (let val of valueArr) {
        this._styleSheetFile = this._styleSheetFile + linkTagFn(val);
      }
    } else {
      this._styleSheetFile = linkTagFn(cssList);
    }
  }
  private returnStyleSheetLinkTags() {
    return this._styleSheetFile;
  }
  private getElementTag(tag: keyof HTMLElementTagNameMap): string {
    const html: string[] = [];
    const elements = document.getElementsByTagName(tag);
    for (let index = 0; index < elements.length; index++) {
      html.push(elements[index].outerHTML);
    }
    return html.join('\r\n');
  }

  @HostListener('click')
  public print(): void {
    let printContents, popupWin, styles = '', links = '';

    if (this.useExistingCss) {
      styles = this.getElementTag('style');
      links = this.getElementTag('link');
    }

    const url = window.location.href;
    const arrUrl = url.split('/');
    const portArr = arrUrl[2].split(':');
    // console.log('port',portArr);
    let urlParc = '/';
    if (portArr.length <= 1) {
      urlParc = urlParc +  arrUrl[3] + '/';
    }

     // console.log('url',urlParc);
    // const urldef = 'http://'+arrUrl[2]+'/'+arrUrl[3]+'/default.asp';

    printContents = document.getElementById(this.printSectionId).innerHTML;
    popupWin = window.open("", "_blank", "top=0,left=0,height=auto,width=99%");
    popupWin.document.open();
    popupWin.document.write(`
      <html>
        <head>
          <title>${this.printTitle ? this.printTitle : ''}</title>
          <base href="/">
          <link href="${urlParc}assets/css/bootstrap.min.css" rel="stylesheet" />
          ${this.returnStyleValues()}
          ${this.returnStyleSheetLinkTags()}
          ${styles}
          ${links}
        </head>
        <body onload="window.print(); setTimeout(()=>{ window.close(); }, 0)">
          <div class="container-fluid">
            <div class="row">
              <div class="col-md-12">
                ${printContents}
              </div>
            </div>
          </div>
        </body>
      </html>`);
    popupWin.document.close();
  }
}

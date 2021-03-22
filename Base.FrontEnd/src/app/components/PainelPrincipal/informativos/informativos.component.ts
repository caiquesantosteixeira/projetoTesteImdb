import { Component, OnInit, ElementRef } from '@angular/core';
import { UrlapiService } from './../../../shared/services/urlapi.service';
import { JwtInfo } from './../../../shared/Util/Jwt-info';

@Component({
  selector: 'app-informativos',
  templateUrl: './informativos.component.html',
  styleUrls: ['./informativos.component.scss']
})
export class InformativosComponent implements OnInit {
  empresa: any = {};
  private jwtInfo = new JwtInfo();

  tableData: any[];
  constructor(private url: UrlapiService) { }

  ngOnInit() {
    const codEmp = this.jwtInfo.CodEmpresa;
    const local = JSON.parse(localStorage.getItem('SGFempresas'));
    if (local !== null) {
      const dadosEmp = local.filter((emp) => {
        return emp.id === codEmp;
      });
      this.empresa = dadosEmp[0];
      this.empresa.dataExpiracao = local[0].dataExpiracao;
    }

    const tableData = [
      ['A Nota Técnica 2018.005 obriga a emissão de NFe com a tag do responsável técnico.', 'https://blog.oobj.com.br/responsavel-tecnico-nfe-nfce'],
      ['SEFAZ-SE instável para emissão de NFC-e', 'http://www.nfce.se.gov.br/portal/painelMonitor.jsp']
    ]

    this.tableData = [['Sem registro de informações...', '/']]; 
 }

}

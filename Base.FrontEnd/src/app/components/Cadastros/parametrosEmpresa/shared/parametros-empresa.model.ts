import { BaseResourceModel } from '../../../../shared/models/base-resource.model';

export class ParametrosEmpresa extends BaseResourceModel {
  constructor(
    public id?: number,
    public codEmpresa?: number,
    public empresa?: string,
    public numBancoPadrao?: number,
    public centroDeCustoVenda?: number,
    public codHistVenda?: number,
    public centroDeCustoCompra?: number,
    public arquivoLogomarca?: string,
    public numNotaFiscal?: number,
    public centroDeCustoPedVenda?: number,
    public ambienteNFe?: number,
    public versaoNFe?: string,
    public nomeContador?: string,
    public numeroCPFContador?: string,
    public numeroCRCContador?: string,
    public numeroCNPJContador?: string,
    public codigoCEPContador?: string,
    public enderecoContador?: string,
    public numeroTelefoneContador?: string,
    public numeroFaxContador?: string,
    public emailContador?: string,
    public bairroContador?: string,
    public iBGEContador?: string,
    public codRegime?: number,
    public percentualPIS?: number,
    public percentualCofins?: number,
    public nomeBancoBackup?: string,
    public dataHoraBackup?: string,
    public LocalBackup?: string,
    public IntervaloEntreBackup?: number,
    public vendeNegativo?: number,
    public expiraEm?: string,
    public CalcularDifalNF?: number,
    public PerguntaVendeNegativo?: number,
    public CalcularPesoAutomatico?: number,
    public serieCertificado?: string,
    public senhaCertificado?: string,
    public serie?: number,
    public certificado?: string,
    public modulo?: number,
    public slogEmpresa?: string,
    public registro?: string,
    public codigoClientePadrao?: string,
    public estado?: string,
    public regime?: number,
    public idToken?: string,
    public cscToken?: string
  ) {
    super();
  }

  static fromJson(jsonData: any): ParametrosEmpresa {
    return Object.assign(new ParametrosEmpresa(), jsonData);
  }

}

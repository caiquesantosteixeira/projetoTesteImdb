import { JwtHelperService } from '@auth0/angular-jwt';
import { Roles } from './../models/roles.model';
import { ChavesStorage } from './../models/chaves-storage';

export class JwtInfo {
  private jwtHelper = new JwtHelperService();

  public CodEmpresa: any;
  public CodEmpAut: any;
  public Usuario: any;
  public Roles: Roles[] = [];

  constructor() {
    this.RecarregaToken();
  }

  public RecarregaToken() {
    const token = this.jwtHelper.decodeToken(JSON.parse(localStorage.getItem(ChavesStorage.Token)));
    // console.log('Token',token);
    if (token !== null) {
      //this.CodEmpresa = +(token.codEmpresa !== undefined ? token.codEmpresa : 0);
      //this.CodEmpAut = +(token.autorizadoEmp !== undefined ? token.autorizadoEmp : 0);
      //this.Usuario = +(token.user !== undefined ? token.user : 0);

      if ((token.Roles !== undefined) && token.roles !== null) {
        token.Roles.forEach((element: string) => {
          const arr = element.split('_');
          const dados = new Roles();
          dados.opcao = arr[0];
          dados.permissao = arr[1];
          this.Roles.push(dados);
        });
      }
    }
  }
}

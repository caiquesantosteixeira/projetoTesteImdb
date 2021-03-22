export class Login {

  constructor(
    public usuario?: string,
    public senha?: string
  ) {

  }

  static fromJson(jsonData: any): Login {
    return Object.assign(new Login(), jsonData);
  }
}

/*export class LoginEmpresa {
  constructor(
    public usuario?: Usuario,
    public empresa?: EmpresaLogin[],
    public security?: Security
  ) { }

  static fromJson(jsonData: any): LoginEmpresa {
    return Object.assign(new LoginEmpresa(), jsonData);
  }
}*/

export class UsuarioLogado{  
  constructor(
    public accesToken?: string,
    public userToken?: UserToken    
  ) {    

  }

  static fromJson(jsonData: any): UsuarioLogado {
    return Object.assign(new UsuarioLogado(), jsonData);
  }
}

export class UserToken {
  public id: string;
  public email: string;
  public usuario: string;
  public idPerfil: number;
}

import { BaseResourceModel } from '../../../../shared/models/base-resource.model';

export class Usuario extends BaseResourceModel {
  constructor(
    public id?: number,
    public login?: string,
    public senha?: string,
    public nomeDoUsuario?: string, 
    public idPerfil?: number,
    public ativo?: number,
    public email?: string    
  ) {
    super();
  }

  static fromJson(jsonData: any): Usuario {
    return Object.assign(new Usuario(), jsonData);
  }
}

export class UsuarioCommand extends BaseResourceModel {
  constructor(
    public id?: number,
    public userName?: string,
    public senha?: string,
    public nome?: string, 
    public idPerfil?: number,
    public ativo?: number,
    public email?: string    
  ) {
    super();
  }

  static fromJson(jsonData: any): UsuarioCommand {
    return Object.assign(new UsuarioCommand(), jsonData);
  }
}

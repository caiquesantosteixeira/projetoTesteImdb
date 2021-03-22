import { BaseResourceModel } from './../../../../shared/models/base-resource.model';

export class PerfilUsuarioBotoes extends BaseResourceModel {

  constructor(
    public id?: number,
    public idPermissoes?: number,
    public idPerfilUsuario?: number,
    public nome?: string,
    public idPerfilUsuarioNavigation?: any,
    public idPermissoesNavigation?: any
  ) {
    super();

  }

  static fromJson(jsonData: any): PerfilUsuarioBotoes {
    return Object.assign(new PerfilUsuarioBotoes(), jsonData);
  }
}

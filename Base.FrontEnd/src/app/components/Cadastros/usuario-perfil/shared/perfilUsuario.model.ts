import { BaseResourceModel } from './../../../../shared/models/base-resource.model';

export class PerfilUsuario extends BaseResourceModel {

  constructor(
    public id?: number,
    public IdModuloMenuOpc?: number,
    public idMenuOpcoes?: number,
    public titulo?: string,
    public visivelMenu?: boolean
  ) {
    super();
  }

  static fromJson(jsonData: any): PerfilUsuario {
    return Object.assign(new PerfilUsuario(), jsonData);
  }

}

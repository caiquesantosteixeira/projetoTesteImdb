import { BaseResourceModel} from './../../../../shared/models/base-resource.model';

export class PerfilUsuarioBotoes extends BaseResourceModel{
  constructor(
    public idPermissoes?: number,
    public idPerfilUsuario?: number
  ) {
    super();
  }

  static fromJson(jsonData: any): PerfilUsuarioBotoes {
    return Object.assign(new PerfilUsuarioBotoes(), jsonData);
  }
}

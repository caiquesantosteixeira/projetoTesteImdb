import { BaseResourceModel } from '../../../../shared/models/base-resource.model';

export class MenuOpcoesBotoes extends BaseResourceModel {
  constructor(
    public id?: number,
    public idPermissoes?: number,
    public idMenuOpcoes?: number,
    public descPermissao?: string
  ) {
    super();
  }
  static fromJson(jsonData: any): MenuOpcoesBotoes {
    return Object.assign(new MenuOpcoesBotoes(), jsonData);
  }
}

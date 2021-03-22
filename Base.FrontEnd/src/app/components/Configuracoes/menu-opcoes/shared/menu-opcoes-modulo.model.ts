import { BaseResourceModel} from './../../../../shared/models/base-resource.model';

export class MenuOpcoesModulo extends BaseResourceModel {

  constructor(
  public id?: number,
  public idModulo?: number,
  public idMenuOpcoes?: number,
  public modulo?: string,
  public menuOpcoes?: string
  ) {
    super();

  }

  static fromJson(jsonData: any): MenuOpcoesModulo {
    return Object.assign(new MenuOpcoesModulo(), jsonData);
  }
}

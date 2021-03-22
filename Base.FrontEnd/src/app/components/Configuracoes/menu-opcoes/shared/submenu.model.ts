import { BaseResourceModel } from '../../../../shared/models/base-resource.model';

export class SubMenu extends BaseResourceModel {
  constructor(
    public idMenu?: number,
    public pathUrl?: string,
    public subMenu?: number,
    public titulo?: string,
    public idModuloMenuOpc?: number,
    public ativo?: boolean,
    public visivelMenu?: boolean,
    public slugPermissao?: string
  ) {
    super();

  }

  static fromJson(jsonData: any): SubMenu {
    return Object.assign(new SubMenu(), jsonData);
  }

}

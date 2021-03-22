import { BaseResourceModel} from '../../../../shared/models/base-resource.model';

export class Menu extends BaseResourceModel{

  constructor (
    public menu1?: string,
    public ordem?: string,
    public icone?: string
  ) {
    super();
  }

  static fromJson(jsonData: any): Menu {
    return Object.assign(new Menu(), jsonData);
  }
}

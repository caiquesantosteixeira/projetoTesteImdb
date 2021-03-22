import { BaseResourceModel } from '../../../../shared/models/base-resource.model';

export class Permissoes extends BaseResourceModel {
  constructor(
    public id?: number,
    public nome?: string,
    public typeCampo?: string,
    public perfilUsuarioBotoes?: any,
  ) {
    super();

  }

  static fromJson(jsonData: any): Permissoes {
    return Object.assign(new Permissoes(), jsonData);
  }

}

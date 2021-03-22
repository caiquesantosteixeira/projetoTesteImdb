import { BaseResourceModel } from '../../../../shared/models/base-resource.model';

export class Perfil extends BaseResourceModel {

  constructor(
  public id?: number,
  public nome?: string
  ) {
    super();

  }

  static fromJson(jsonData: any): Perfil {
    return Object.assign(new Perfil(), jsonData);
  }
}

import { MenuOpcPermissao } from './menu-opc-permissao.model';

export class MenuOpcoes {
  public id?: number;
  public path?: string;
  public title?: string;
  public ab?: string;
  public permissoes?: MenuOpcPermissao[];
}

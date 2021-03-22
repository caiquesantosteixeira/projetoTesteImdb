import {MenuOpcoes} from './menu-opcoes.model';

export class Menu{
  public title?: string;
  public path?: string;
  public type?: string;
  public icontype?: string;
  public children?: MenuOpcoes[];
}

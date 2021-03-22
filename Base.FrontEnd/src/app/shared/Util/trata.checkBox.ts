export class TrataCheckbox{

  static TrueFalse(valor: any) {
      let retorno: any;
      // console.log('valor', valor);
      switch (valor) {
        case true:
            retorno = 1;
            break;
        case false:
            retorno =  0;
            break;
        case '':
            retorno =  0;
            break;
        case undefined:
            retorno =  0;
            break;
        case 1:
            retorno = true;
            break;
        case 0:
            retorno =  false;
            break;
        case '1':
            retorno = true;
            break;
        case '0':
            retorno =  false;
            break;    
      }

      return retorno;
  }
}

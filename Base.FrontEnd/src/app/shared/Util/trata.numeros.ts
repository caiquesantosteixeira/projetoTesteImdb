export class TrataNumeros{

  static  Numeric(valor: string|number) {
    let numero = valor.toString().trim();
    if((numero.length > 0)) {
      numero = numero.replace(',', '.');
      if (!isNaN(+numero)) {
        return +numero;
      } else {
        return +'0.00';
      }
    } else {
      return +'0.00';
    }
  }

  static  Float(valor: string|number) {
    const numero = valor.toString().trim();
    if((numero.length > 0) && !isNaN(+numero)) {
      let num = numero.replace('.', ',');
      return num;
    } else {
      return '0,00';
    }
  }
}

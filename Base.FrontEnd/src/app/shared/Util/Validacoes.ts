import { AbstractControl} from '@angular/forms';




export class  Validacoes{
      static ValidaCpf(controle: AbstractControl) {
      const cpf = controle.value;
      let soma = 0;
      let resto: number;
      let valido: boolean;

      const regex = new RegExp('[0-9]{11}');

// tslint:disable-next-line: align
      if (
        cpf == '00000000000' ||
        cpf == '11111111111' ||
        cpf == '22222222222' ||
        cpf == '33333333333' ||
// tslint:disable-next-line: triple-equals
        cpf == '44444444444' ||
        cpf == '55555555555' ||
        cpf == '66666666666' ||
        cpf == '77777777777' ||
        cpf == '88888888888' ||
        cpf == '99999999999' ||
        !regex.test(cpf)
      ) {
        valido = false;
      } else {
        for (let i = 1; i <= 9; i++)
          soma = soma + parseInt(cpf.substring(i - 1, i)) * (11 - i);
        resto = (soma * 10) % 11;

        if (resto == 10 || resto == 11) { resto = 0; };
        if (resto != parseInt(cpf.substring(9, 10))){ valido = false; };

        soma = 0;
        for (let i = 1; i <= 10; i++){
          soma = soma + parseInt(cpf.substring(i - 1, i)) * (12 - i);
        }
        resto = (soma * 10) % 11;

        if (resto == 10 || resto == 11) resto = 0;
        if (resto != parseInt(cpf.substring(10, 11))) valido = false;
        valido = true;
      }

      if (valido) return null;

      return { cpfInvalido: true };
    }

    static cepValidator(control: AbstractControl) {
      let cep = control.value;
      if (cep && cep !== '' && cep !== '0') {
        cep = cep.replace('-', '');
        const validacep = /^[0-9]{8}$/;
        return validacep.test(cep) ? null : { cepInvalido : true};
      }
      return null;
    }

    static cest(control: AbstractControl) {
      const valor = control.value.trim();
      return valor.length > 0 && valor.length < 7 ? {qtddigitosInvalido : true} : null;
    }


    static espacoBranco(control: AbstractControl){
      console.log(control.value);
      const ctrl = control.value.trim();
    }

}

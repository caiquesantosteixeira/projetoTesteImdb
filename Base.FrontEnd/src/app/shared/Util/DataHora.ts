import * as moment from 'moment';

export const DataAtual = moment(new Date()).format('DD/MM/YYYY');
export const HoraAtual = moment(new Date()).format('HH:mm');

export class TrataData {

  static DataSql(data: string) {
    let dt = '';
    let ar: any = [];
    let dataAt = '';

    if ((data !== undefined) && data != null) {
      if (data.toString().length <= 0) {
        return null;
      }

      if (data.search('Invalid') > -1) {
        return null;
      }

      dt = data.toString().substring(0, 10).trim();
      if (dt.length < 10) {
        return null;
      }
      ar = dt.split('/');
      const dataConv = ar[1] + '-' + ar[0] + '-' + ar[2];

      dataAt = moment(new Date(dataConv)).format('YYYY-MM-DD');

    }
    return dataAt;
  }

  static exbDataHora(data: string) {
    let dt = '';
    if ((data !== undefined) && data != null) {
      dt = moment(new Date(data)).format('DD/MM/YYYY HH:mm');
    }
    return dt;

  }

  static DataToYear(data: any) {
    let dt = '';
    if ((data !== undefined) && data != null) {
      dt = moment(new Date(data)).format('YYYY');
    }
    return dt;
  }

  static DataToMonth(data: any) {
    let dt = '';
    if ((data !== undefined) && data != null) {
      dt = moment(new Date(data)).format('MM');
    }
    return dt;
  }

  static DataToDay(data: any) {
    let dt = '';
    if ((data !== undefined) && data != null) {
      dt = moment(new Date(data)).format('DD');
    }
    return dt;
  }

  static DataSqlToPT(data: any) {
    let dt = '';
    if ((data !== undefined) && data != null) {
      dt = moment(new Date(data)).format('DD/MM/YYYY');
    }
    return dt;
  }

}


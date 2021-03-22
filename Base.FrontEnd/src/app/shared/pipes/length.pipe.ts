import { Pipe, PipeTransform } from '@angular/core';

@Pipe(
  { name: 'appLength' }
)

export class AppLengthPipe implements PipeTransform {
  transform(value: string, length?: any): string {
    let text = '';
     // console.log('valor', value, 'exponet -> ', length);
    if ((value !== undefined) && value !== null) {
      const size = length !== undefined ? length : 40;
      text = value.substring(0, size);
    }
    return text;
  }
}

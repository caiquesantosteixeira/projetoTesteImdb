import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, RouterStateSnapshot, Resolve } from '@angular/router';
import { Observable, of } from 'rxjs';

import { BaseResourceModel } from '../models/base-resource.model';
import { BaseResourceService } from '../services/base-resource.service';
@Injectable({
  providedIn: 'root'
})
export class CarregamentoRotasResolver<T extends BaseResourceModel> implements Resolve<any> {

  constructor(protected resourceService: BaseResourceService<T>) {}

  resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<any> {
    // console.log('resolver -> ', route.params);
    if (route.params && route.params[0]) {
      return this.resourceService.getById(route.params[0]);
    } else {
      return of(null);
    }

  }
}

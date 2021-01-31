import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {environment} from '../../environments/environment';
import {GenericHttpService} from './GenericHttpService/GenericHttpService';
import {IProduct} from '../contracts/IProduct';
import {Observable} from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ProductService extends GenericHttpService<IProduct, number> {

  constructor(protected http: HttpClient) {
    super(http, `${environment.api.baseUrl}/product`);
  }

  getByPZN(pzn: number): Observable<IProduct> {
    return this.http.get<IProduct>(this.base + '/pzn/' + pzn);
  }


}

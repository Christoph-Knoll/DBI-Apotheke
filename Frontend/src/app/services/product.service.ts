import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {environment} from '../../environments/environment';
import {GenericHttpService} from './GenericHttpService/GenericHttpService';
import {IProduct} from '../contracts/IProduct';

@Injectable({
  providedIn: 'root'
})
export class ProductService extends GenericHttpService<IProduct, number> {

  constructor(protected http: HttpClient) {
    super(http, `${environment.api.baseUrl}/product`);
  }


}

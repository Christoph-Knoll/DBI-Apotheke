import { Injectable } from '@angular/core';
import {GenericHttpService} from './GenericHttpService/GenericHttpService';
import {HttpClient} from '@angular/common/http';
import {environment} from '../../environments/environment';
import {IProductInfo} from '../contracts/IProductInfo';

@Injectable({
  providedIn: 'root'
})
export class ProductInfoService extends GenericHttpService<IProductInfo, number> {

  constructor(protected http: HttpClient) {
    // #TODO Add path
    super(http, `${environment.api.baseUrl}/`);
  }
}

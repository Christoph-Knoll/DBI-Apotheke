import { Injectable } from '@angular/core';
import {GenericHttpService} from './GenericHttpService/GenericHttpService';
import {HttpClient} from '@angular/common/http';
import {environment} from '../../environments/environment';
import {IStorage} from '../contracts/IStorage';

@Injectable({
  providedIn: 'root'
})
export class StorageService extends GenericHttpService<IStorage, string> {

  constructor(protected http: HttpClient) {
    super(http, `${environment.api.baseUrl}/storage`);
  }
}

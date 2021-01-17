import { Injectable } from '@angular/core';
import {GenericHttpService} from './GenericHttpService/GenericHttpService';
import {HttpClient} from '@angular/common/http';
import {environment} from '../../environments/environment';
import {IRecipe} from '../contracts/IRecipe';

@Injectable({
  providedIn: 'root'
})
export class RecipeService extends GenericHttpService<IRecipe, number> {

  constructor(protected http: HttpClient) {
    super(http, `${environment.api.baseUrl}/recipe`);
  }
}

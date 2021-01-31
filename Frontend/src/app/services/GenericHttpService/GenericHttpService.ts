import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import {HttpCrudOperations} from './HttpCrudOperations';
import { ControlContainer } from '@angular/forms';


export abstract class GenericHttpService<T, ID> implements HttpCrudOperations<T, ID> {

    protected constructor(
        protected http: HttpClient,
        protected base: string
    ) {}

    get(id: ID): Observable<T> {
        return this.http.get<T>(this.base + '/' + id);
    }

    getAll(): Observable<T[]> {
        return this.http.get<T[]>(this.base + '/all');
    }

  save(t: T): Observable<T> {
      const headers = { 'content-type': 'application/json'}
      const body=JSON.stringify(t);
      return this.http.post<T>(this.base,body,{'headers': headers})
    }

    update(id: ID, t: T): Observable<T> {
        return this.http.put<T>(this.base + '/' + id, t, {});
    }

    delete(id: ID): Observable<T> {
        return this.http.delete<T>(this.base + '/' + id);
    }
}

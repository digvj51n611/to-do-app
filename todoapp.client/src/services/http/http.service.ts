import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { Observable,map } from 'rxjs';
import { TaskItem,Response} from '../../app/data/models';
@Injectable({
  providedIn: 'root'
})
export class HttpService {
  headers  : HttpHeaders;
  url : string;
  token : string = 'to be taken from authentication';
  constructor(private http : HttpClient) {
    this.headers = new HttpHeaders().set(
      'Authorization', `Bearer ${this.token}`
    );
    // the below line must be condition based on where the request is going 
    this.url = environment.baseUrl+apiPaths.Tasks;
  }
  private toResponse<T>(response : any ) : Response<T> {
    if(response.isSuccess) {
      return {
        isSuccess : true,
        errorList : [],
        message : response.message,
        data : response.result
      }
    }
    else {
      return {
        isSuccess : false,
        errorList : response.errors,
        message : response.message,
        data : response.result
      }
    }
  }
  getAll<T>():Observable<Response<T>> {
    return this.http.get<T>(this.url, {
      headers : this.headers
    })
    .pipe(map(httpResponse => this.toResponse(httpResponse)));
  }
  get<T>(id : number):Observable<Response<T>> {
    return this.http.get<T>(this.url+`/${id}`,{
      headers : this.headers
    })
    .pipe(map(httpResponse => this.toResponse<T>(httpResponse)));
  }
  create<T>(item : T):Observable<Response<T>> {
    return this.http.post<T>( this.url, item , {
      headers : this.headers
    })
    .pipe(map(httpResponse => this.toResponse<T>(httpResponse)))
  }
  update<T>(id : number,item : T):Observable<Response<T>> {
    return this.http.put<T>(this.url+`$/{id}`,item, {
      headers : this.headers
    })
    .pipe(map(httpResponse => this.toResponse<T>(httpResponse)));
  }
  delete<T>(id : number) : Observable<Response<T>> {
    return this.http.delete<T>(this.url+`$/{id}`,{
      headers : this.headers
    })
    .pipe(map(httpResponse => this.toResponse<T>(httpResponse)));
  }
}

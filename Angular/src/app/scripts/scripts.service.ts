import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Observable} from 'rxjs';


@Injectable({
  providedIn: 'root'
})
export class ScriptsService {
//readonly APIUrl="http://localhost:5000/api";
//readonly PhotoUrl = "http://localhost:5000/Photos/";
readonly APIUrl="http://localhost:53535/api";
readonly PhotoUrl = "http://localhost:53535/Photos/";

  constructor(private http:HttpClient) { }

  getAll():Observable<any[]>{
    return this.http.get<any>(this.APIUrl+'/scripts');
  }

  getSalaryMore10000():Observable<any[]>{
    return this.http.get<any>(this.APIUrl+'/scripts');
  }

  updateSalary(){
    return this.http.get(this.APIUrl+'/scripts/UpdateSalary');
  }

  deleteOldEmployees(){
    return this.http.delete(this.APIUrl+'/scripts');
  }
  test(){
    return this.http.get(this.APIUrl+'/scripts/test');
  }
}

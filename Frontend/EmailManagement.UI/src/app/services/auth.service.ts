import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { IUser } from '../models/IUser';
import { Observable } from 'rxjs';
import { ILogin } from '../models/ILogin';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(private http:HttpClient) { }

  public register(user:IUser):Observable<boolean>{
    return this.http.post<boolean>('https://localhost:7174/api/User/Create',user);
  }

  public login(loginData:ILogin):Observable<string>{
    return this.http.post('https://localhost:7174/api/User/Login',loginData,{responseType:'text'});
  }
}

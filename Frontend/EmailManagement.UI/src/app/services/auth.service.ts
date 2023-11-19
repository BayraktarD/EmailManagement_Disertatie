import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { IUser } from '../models/IUser';
import { Observable, catchError, pipe } from 'rxjs';
import { ILogin } from '../models/ILogin';
import * as forge from 'node-forge'
import { environment } from 'src/environments/environment';
import { HttpService } from './http.service';
import { RsaHelper } from './rsa-helper.service';

@Injectable({
  providedIn: 'root'
})
export class AuthService {



  constructor(private http:HttpClient,private httpService: HttpService, private rsaHelper:RsaHelper) { }

  public register(user:IUser):Observable<boolean>{
    var rsa = this.rsaHelper.getRsaPublicKey();
    var encryptedUserData= {} as IUser;

    encryptedUserData.userName = window.btoa(rsa.encrypt(user.userName));
    encryptedUserData.password = window.btoa(rsa.encrypt(user.password));
    encryptedUserData.emailAddress = window.btoa(rsa.encrypt(user.emailAddress));
    encryptedUserData.firstName = window.btoa(rsa.encrypt(user.firstName));
    encryptedUserData.lastName = window.btoa(rsa.encrypt(user.lastName));

    return this.http.post<boolean>(environment.apiUrl+'User/Create',encryptedUserData)
    .pipe(catchError(this.httpService.handleError));
  }



  public login(loginData:ILogin):Observable<string>{
    var rsa = this.rsaHelper.getRsaPublicKey();
    var encryptedLoginData= {} as ILogin;
    //rsa.encrypt returneaza un byte[], iar pentru a transmite mai usor prin retea mai bine il convertim in base64 prin window.btoa
    encryptedLoginData.username = window.btoa(rsa.encrypt(loginData.username));
    encryptedLoginData.password = window.btoa(rsa.encrypt(loginData.password));
    encryptedLoginData.emailAddress = window.btoa(rsa.encrypt(loginData.emailAddress));

    return this.http.post(environment.apiUrl+'User/Login',encryptedLoginData,{responseType:'text'})
    .pipe(catchError(this.httpService.handleError));
  }
}

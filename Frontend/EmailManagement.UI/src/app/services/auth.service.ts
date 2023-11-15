import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { IUser } from '../models/IUser';
import { Observable } from 'rxjs';
import { ILogin } from '../models/ILogin';
import * as forge from 'node-forge'
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AuthService {



  constructor(private http:HttpClient) { }

  public register(user:IUser):Observable<boolean>{
    var rsa = forge.pki.publicKeyFromPem(environment.publicEncryptionKey);
    var encryptedUserData= {} as IUser;

    encryptedUserData.userName = window.btoa(rsa.encrypt(user.userName));
    encryptedUserData.password = window.btoa(rsa.encrypt(user.password));
    encryptedUserData.emailAddress = window.btoa(rsa.encrypt(user.emailAddress));
    encryptedUserData.firstName = window.btoa(rsa.encrypt(user.firstName));
    encryptedUserData.lastName = window.btoa(rsa.encrypt(user.lastName));

    return this.http.post<boolean>(environment.apiUrl+'User/Create',encryptedUserData);
  }

  public login(loginData:ILogin):Observable<string>{
    var rsa = forge.pki.publicKeyFromPem(environment.publicEncryptionKey);
    var encryptedLoginData= {} as ILogin;
    //rsa.encrypt returneaza un byte[], iar pentru a transmite mai usor prin retea mai bine il convertim in base64 prin window.btoa
    encryptedLoginData.username = window.btoa(rsa.encrypt(loginData.username));
    encryptedLoginData.password = window.btoa(rsa.encrypt(loginData.password));
    encryptedLoginData.emailAddress = window.btoa(rsa.encrypt(loginData.emailAddress));

    return this.http.post(environment.apiUrl+'User/Login',encryptedLoginData,{responseType:'text'});
  }
}

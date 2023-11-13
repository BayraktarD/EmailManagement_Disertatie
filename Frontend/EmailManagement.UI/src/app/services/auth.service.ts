import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { IUser } from '../models/IUser';
import { Observable } from 'rxjs';
import { ILogin } from '../models/ILogin';
import * as forge from 'node-forge'

@Injectable({
  providedIn: 'root'
})
export class AuthService {

publicKey:string =`-----BEGIN PUBLIC KEY-----
MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEA0j7lpc9KG1JxPjDQDgsF
zlfmDrCPU8uZ0Ycy31YFEkaSh6SAv4Pltm/VSPf9u1Yg3ysMzyXJc+oM4+AoaegT
YZE8YKOuRdQmR3GF6c+2f1ROl/tMYSm0CIYQXWLB7CV5mHxj1gEMJxLf3cUD0VfG
v+DH1axR1bqbiJNh+3LMOjliefb7WNN14Hja2k+SMpXDxlTvypm4VMz1UlhQVNib
xbez0Ku5m8OV4162R3vLe72yW8WAfQXwqueRjBkIyv0OTpxLCWUsYb6JEot8jov8
5SxORYVnF3VGLuhtOJSn48ONqfYlzXFoY2NuzGjq4WBB1h1i+I2bpv+eCVg+mVDe
eQIDAQAB
-----END PUBLIC KEY-----`

  constructor(private http:HttpClient) { }

  public register(user:IUser):Observable<boolean>{
    var rsa = forge.pki.publicKeyFromPem(this.publicKey);
    var encryptedUserData= {} as IUser;

    encryptedUserData.userName = window.btoa(rsa.encrypt(user.userName));
    encryptedUserData.password = window.btoa(rsa.encrypt(user.password));
    encryptedUserData.emailAddress = window.btoa(rsa.encrypt(user.emailAddress));
    encryptedUserData.firstName = window.btoa(rsa.encrypt(user.firstName));
    encryptedUserData.lastName = window.btoa(rsa.encrypt(user.lastName));

    return this.http.post<boolean>('https://localhost:7174/api/User/Create',encryptedUserData);
  }

  public login(loginData:ILogin):Observable<string>{
    var rsa = forge.pki.publicKeyFromPem(this.publicKey);
    var encryptedLoginData= {} as ILogin;

    //rsa.encrypt returneaza un byte[], iar pentru a transmite mai usor prin retea mai bine il convertim in base64 prin window.btoa
    encryptedLoginData.username = window.btoa(rsa.encrypt(loginData.username));
    encryptedLoginData.password = window.btoa(rsa.encrypt(loginData.password));
    encryptedLoginData.emailAddress = window.btoa(rsa.encrypt(loginData.emailAddress));

    return this.http.post('https://localhost:7174/api/User/Login',encryptedLoginData,{responseType:'text'});
  }
}

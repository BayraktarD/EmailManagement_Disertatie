import { Injectable } from '@angular/core';
import * as CryptoJS from 'crypto-js';
import { environment } from 'src/environments/environment';


@Injectable({
  providedIn: 'root'
})
export class EncryptDecryptService {

  constructor() { }
  // public decrypt(input:string):string{

  //   var rsa = forge.pki.privateKeyFromPem(environment.privateDecryptionKey);
  //   return rsa.decrypt(input);
  // }

}

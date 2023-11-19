import { Injectable } from '@angular/core';
import * as forge from 'node-forge'
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class RsaHelper {

  constructor() { }

  public getRsaPublicKey() {
    return forge.pki.publicKeyFromPem(environment.publicEncryptionKey);
  }

  public decrypt(chiperText:string){
    let rsa = forge.pki.privateKeyFromPem(environment.privateRSAKey);
    let input = forge.util.decode64(chiperText)
    let result = rsa.decrypt(input);
    return result;
  }
}

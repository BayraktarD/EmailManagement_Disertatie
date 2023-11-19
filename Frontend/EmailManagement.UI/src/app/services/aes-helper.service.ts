import { Injectable } from '@angular/core';
import * as CryptoJS from 'crypto-js';

@Injectable({
  providedIn: 'root',
})
export class AESHelper {
  private iv: string = '@qwertyuiop12344';
  private key: string = '';
  constructor() {}

  aesKey(): string {
    this.key = this.randomKey(16);
    return this.key;
  }

  encrypt(valueToEncrypt: string): string {
    var key = CryptoJS.enc.Utf8.parse(this.key);
    var iv = CryptoJS.enc.Utf8.parse(this.iv);

    var encryptedValue = CryptoJS.AES.encrypt(
      CryptoJS.enc.Utf8.parse(valueToEncrypt),
      key,

      {
        keySize: 128 / 8,
        iv: iv,
        mode: CryptoJS.mode.ECB,
        padding: CryptoJS.pad.Iso10126,
      }
    );

    return encryptedValue.toString();
  }

  // decrypt(chiperText: string, key:string): string {
  //   var keyArray = CryptoJS.enc.Utf8.parse(key);
  //   var iv = CryptoJS.enc.Utf8.parse(this.iv);

  //   var plainText = CryptoJS.AES.decrypt(
  //     chiperText,
  //     keyArray,

  //     {
  //       keySize: 128 / 8,
  //       iv: iv,
  //       mode: CryptoJS.mode.ECB,
  //       padding: CryptoJS.pad.Iso10126,
  //     }
  //   );

  //   return plainText.toString();
  // }

  decrypt(chiperText: string, key: string): string {
    const keyBytes = CryptoJS.enc.Utf8.parse(key);
    const ivBytes = CryptoJS.enc.Utf8.parse(this.iv);

    const decrypted = CryptoJS.AES.decrypt(
      chiperText,
      keyBytes,
      {
        keySize: 128 / 8,
        iv: ivBytes,
        mode: CryptoJS.mode.ECB,
        padding: CryptoJS.pad.Iso10126,
      }
    );

    return CryptoJS.enc.Utf8.stringify(decrypted);
  }




  randomKey(length: number) {
    var result = '';
    var characters = '@abcdefghijklmnopqrstuvwxyz123456789';
    var charactersLength = characters.length;
    for (var i = 0; i < length; i++) {
      result += characters.charAt(Math.floor(Math.random() * charactersLength));
    }
    return result;
  }
}

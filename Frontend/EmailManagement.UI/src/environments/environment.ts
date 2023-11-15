// This file can be replaced during build by using the `fileReplacements` array.
// `ng build` replaces `environment.ts` with `environment.prod.ts`.
// The list of file replacements can be found in `angular.json`.

export const environment = {
  production: false,
  apiUrl:"https://localhost:7174/api/",

  publicEncryptionKey:`-----BEGIN PUBLIC KEY-----
MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEA0j7lpc9KG1JxPjDQDgsF
zlfmDrCPU8uZ0Ycy31YFEkaSh6SAv4Pltm/VSPf9u1Yg3ysMzyXJc+oM4+AoaegT
YZE8YKOuRdQmR3GF6c+2f1ROl/tMYSm0CIYQXWLB7CV5mHxj1gEMJxLf3cUD0VfG
v+DH1axR1bqbiJNh+3LMOjliefb7WNN14Hja2k+SMpXDxlTvypm4VMz1UlhQVNib
xbez0Ku5m8OV4162R3vLe72yW8WAfQXwqueRjBkIyv0OTpxLCWUsYb6JEot8jov8
5SxORYVnF3VGLuhtOJSn48ONqfYlzXFoY2NuzGjq4WBB1h1i+I2bpv+eCVg+mVDe
eQIDAQAB
-----END PUBLIC KEY-----`,
aesEncryptKey:`1203199320052021`,
aesEncryptIV:`1203199320052021`,
};

/*
 * For easier debugging in development mode, you can import the following file
 * to ignore zone related error stack frames such as `zone.run`, `zoneDelegate.invokeTask`.
 *
 * This import should be commented out in production mode because it will have a negative impact
 * on performance if an error is thrown.
 */
// import 'zone.js/plugins/zone-error';  // Included with Angular CLI.

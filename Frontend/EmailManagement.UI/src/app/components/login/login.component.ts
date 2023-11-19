import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ILogin } from 'src/app/models/ILogin';
import { IUser } from 'src/app/models/IUser';
import { AuthService } from 'src/app/services/auth.service';
import { EncryptDecryptService } from 'src/app/services/encrypt-decrypt.service';
import { TokenStorageService } from 'src/app/services/token-storage.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  loginData: ILogin = {
    username: "",
    password: "",
    emailAddress:""
  };

  isLoggedIn = false;
  isLoginFailed = false;
  errorMessage = '';
  roles: string[] = [];

  constructor(private router: Router,private authService:AuthService, private tokenStorage:TokenStorageService) { }

  ngOnInit(): void {
  }

  onSubmit(): void {

    this.authService.login(this.loginData).subscribe(
      response => {
        if(response.length>0){

          this.isLoginFailed = false;
          this.isLoggedIn = true;
          this.tokenStorage.saveToken(response);
          this.router.navigate(["components/sent-mails"]);
          this.reloadPage();

        }
        else{
          this.errorMessage = "Login failed!"
          this.isLoginFailed = true;
        }
      },
      err => {
        this.errorMessage = err.error;
        this.isLoginFailed = true;
      }
    );
  }

 reloadPage(): void {
    window.location.reload();
  }

}

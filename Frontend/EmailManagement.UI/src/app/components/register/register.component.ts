import { Component, OnInit } from '@angular/core';
import { IUser } from 'src/app/models/IUser';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  user: IUser = {
    firstName: "",
    lastName: "",
    emailAddress:"",
    userName: "",
    password: "",
    clientAppId:"",
    userId:""
  };
  isSuccessful = false;
  isSignUpFailed = false;
  errorMessage = ''


  constructor(private authService:AuthService) { }

  ngOnInit(): void {
  }

  onSubmit(): void {

    this.authService.register(this.user).subscribe(
      data => {
        console.log(data);
        this.isSuccessful = true;
        this.isSignUpFailed = false;
      },
      err => {
        this.errorMessage = err.error.message;
        this.isSignUpFailed = true;
      }
    );
  }

}

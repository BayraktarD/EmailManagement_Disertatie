import { Component, OnChanges, OnInit, SimpleChanges } from '@angular/core';
import { Router } from '@angular/router';
import { TokenStorageService } from './services/token-storage.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit, OnChanges {
  isLoggedIn = false;
  showAdminBoard = false;
  showModeratorBoard = false;
  username?: string;
  title = 'EmailManagement.UI';

  constructor(private router: Router,private tokenStorageService: TokenStorageService) { }

  ngOnChanges(changes: SimpleChanges): void{
    this.isLoggedIn = this.tokenStorageService.getToken()!=null;
  }

  ngOnInit(): void {
    this.isLoggedIn = this.tokenStorageService.getToken()!=null;

    if (this.isLoggedIn) {
      this.router.navigate(["components/sent-mails"]);
    }
    else{
      this.router.navigate(["components/login"]);
    }
  }

  logout(): void {
    this.isLoggedIn = false;
    window.location.reload();
    this.tokenStorageService.signOut();
  }
}

import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './components/login/login.component';
import { RegisterComponent } from './components/register/register.component';
import { SentMailsComponent } from './components/sent-mails/sent-mails.component';
import { AddEmailFormComponent } from './components/sent-mails/add-email-form/add-email-form.component';

const routes: Routes = [
  { path: 'components/login', component: LoginComponent },
  { path: 'components/register', component: RegisterComponent },
  { path: 'components/sent-mails', component: SentMailsComponent },
  { path: 'components/sent-mails/add-email-form', component: AddEmailFormComponent },

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }

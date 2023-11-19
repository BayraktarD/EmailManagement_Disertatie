import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { IEmail } from '../models/IEmail';
import { Observable, catchError } from 'rxjs';
import { environment } from 'src/environments/environment';
import { HttpService } from './http.service';

@Injectable({
  providedIn: 'root'
})
export class EmailsService {

  constructor(private http:HttpClient, private httpService: HttpService) { }

  public sendEmail(email:IEmail):Observable<boolean>{
    return this.http.post<boolean>(environment.apiUrl+'SentEmails/SendEmail',email)
    .pipe(catchError(this.httpService.handleError));
  }

  public getUserEmails(userId:string):Observable<IEmail[]>{
    return this.http.get<IEmail[]>(environment.apiUrl+'SentEmails/GetUserSentEmails?userId='+userId)
    .pipe(catchError(this.httpService.handleError));
  }

  public deleteEmail(emailId:string):Observable<boolean>{
    return this.http.delete<boolean>(environment.apiUrl+'SentEmails/DeleteEmail?emailId='+emailId)
    .pipe(catchError(this.httpService.handleError));
  }

}

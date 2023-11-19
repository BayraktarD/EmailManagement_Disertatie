import { Component, Input, OnInit, TemplateRef } from '@angular/core';
import { Router } from '@angular/router';
import { IEmail } from 'src/app/models/IEmail';
import { AESHelper } from 'src/app/services/aes-helper.service';
import { EmailsService } from 'src/app/services/emails.service';
import { RsaHelper } from 'src/app/services/rsa-helper.service';
import { TokenStorageService } from 'src/app/services/token-storage.service';

@Component({
  selector: 'app-sent-mails',
  templateUrl: './sent-mails.component.html',
  styleUrls: ['./sent-mails.component.css'],
})
export class SentMailsComponent implements OnInit {


   dataSource=[] as IEmail[];

  selectedRecord={} as IEmail;


  constructor(private router: Router, private emailService:EmailsService, private tokenStorage :TokenStorageService,
    private rsaHelper:RsaHelper, private aesHelper:AESHelper){}

  ngOnInit(): void {
    this.getUserEmails();
  }

  getUserEmails(){
    var encryptedEmails = [] as IEmail[];
    this.emailService.getUserEmails(this.tokenStorage.getUserKey()).subscribe(data=>{
      data.forEach(email=> {
        let decryptedAesKey = this.rsaHelper.decrypt(email.aesKey);
        let decryptedEmail = {
        } as IEmail;
        decryptedEmail.content=this.aesHelper.decrypt(email.content,decryptedAesKey);
        decryptedEmail.title=this.aesHelper.decrypt(email.title,decryptedAesKey);
        decryptedEmail.from=this.aesHelper.decrypt(email.from,decryptedAesKey);
        decryptedEmail.to=this.aesHelper.decrypt(email.to,decryptedAesKey);
        decryptedEmail.userId=this.aesHelper.decrypt(email.userId,decryptedAesKey);
        decryptedEmail.sentEmailId=this.aesHelper.decrypt(email.sentEmailId,decryptedAesKey);

        this.dataSource.push(decryptedEmail);
      });
    });

  }

  sendEmail() {
    this.router.navigate(["components/sent-mails/add-email-form"]);
  }



  deleteEmail() {
    this.emailService.deleteEmail(this.selectedRecord.sentEmailId)
  }

  selectRecord(record: any) {
    this.selectedRecord = record;
  }

}

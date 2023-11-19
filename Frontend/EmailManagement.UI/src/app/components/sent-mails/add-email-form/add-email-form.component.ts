import { ChangeDetectionStrategy, Component,OnInit, Output, EventEmitter } from '@angular/core';
import { Router } from '@angular/router';
import { IEmail } from 'src/app/models/IEmail';
import { AESHelper } from 'src/app/services/aes-helper.service';
import { EmailsService } from 'src/app/services/emails.service';
import { RsaHelper } from 'src/app/services/rsa-helper.service';
import { TokenStorageService } from 'src/app/services/token-storage.service';

@Component({
    selector: 'app-add-email-form',
    templateUrl: './add-email-form.component.html',
    styleUrls: ['./add-email-form.component.css'],
    changeDetection: ChangeDetectionStrategy.OnPush,
})
export class AddEmailFormComponent implements OnInit {
  ngOnInit(): void {
  }

  constructor(private router: Router, private emailService:EmailsService,
    private tokenStorage:TokenStorageService, private aesHelper:AESHelper, private rsaHelper:RsaHelper){}


  newRecord: IEmail = {
    sentEmailId: '',
    userId: this.tokenStorage.getUserKey()?.toString(),
    from: '',
    to: '',
    title: '',
    content: '',
    aesKey:'',
  };

  @Output() addRecord = new EventEmitter<IEmail>();
  @Output() close = new EventEmitter<void>();

  onSubmit() {

    const aesKeyValue = this.aesHelper.aesKey();
    const rsa = this.rsaHelper.getRsaPublicKey();
    const aesEncryptedKey = window.btoa(rsa.encrypt(aesKeyValue));

    let encryptedEmail = {
      aesKey:aesEncryptedKey,
      sentEmailId:'',
      title:this.aesHelper.encrypt(this.newRecord.title),
      content:this.aesHelper.encrypt(this.newRecord.content),
      from:this.aesHelper.encrypt(this.newRecord.from),
      to:this.aesHelper.encrypt(this.newRecord.to),
      userId:this.aesHelper.encrypt(this.newRecord.userId)
    } as IEmail;

    this.emailService.sendEmail(encryptedEmail).subscribe(
      data => {
        console.log(data);
        this.router.navigate(["components/sent-mails"]);
      },
      err => {
      }
    );
  }

}

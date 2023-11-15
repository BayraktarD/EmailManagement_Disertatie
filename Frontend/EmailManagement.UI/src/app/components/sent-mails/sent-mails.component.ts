import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-sent-mails',
  templateUrl: './sent-mails.component.html',
  styleUrls: ['./sent-mails.component.css'],
  template: `<ejs-grid [dataSource]='data' [allowPaging]="true" [allowSorting]="true"
              [allowFiltering]="true" [pageSettings]="pageSettings">
              <e-columns>
                  <e-column field='OrderID' headerText='Order ID' textAlign='Right' width=90></e-column>
                  <e-column field='CustomerID' headerText='Customer ID' width=120></e-column>
                  <e-column field='Freight' headerText='Freight' textAlign='Right' format='C2' width=90></e-column>
                  <e-column field='OrderDate' headerText='Order Date' textAlign='Right' format='yMd' width=120></e-column>
              </e-columns>
              </ejs-grid>`
})
export class SentMailsComponent implements OnInit {

  constructor() { }

  ngOnInit(): void {
  }

}

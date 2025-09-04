import { Component, OnInit } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';

@Component({
  selector: 'app-auth',
  templateUrl: './auth.component.html',
  styleUrls: ['./auth.component.scss']
})
export class AuthComponent implements OnInit {

  constructor(private translate: TranslateService) {
 
    const browserLang = localStorage.getItem('lang') ?? 'en';
    this.translate.use(browserLang);
  }


  ngOnInit(): void {
  }

}

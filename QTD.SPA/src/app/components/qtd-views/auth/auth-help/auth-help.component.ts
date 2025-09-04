import { Component, OnInit } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';

@Component({
  selector: 'app-auth-help',
  templateUrl: './auth-help.component.html',
  styleUrls: ['./auth-help.component.scss'],
})
export class AuthHelpComponent implements OnInit {
  constructor(private translate: TranslateService) {
    const browserLang = localStorage.getItem('lang') ?? 'en';
    this.translate.use(browserLang);
  }

  ngOnInit(): void {}
}

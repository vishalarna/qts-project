import { Component, OnInit } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';

@Component({
  selector: 'app-not-found404',
  templateUrl: './not-found404.component.html',
  styleUrls: ['./not-found404.component.scss'],
})
export class NotFound404Component implements OnInit {
  constructor(private translate: TranslateService) {
    const browserLang = localStorage.getItem('lang') ?? 'en';
    this.translate.use(browserLang);
  }

  ngOnInit(): void {}
}

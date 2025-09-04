import { Component, OnInit } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';

@Component({
  selector: 'app-emp',
  templateUrl: './emp.component.html',
  styleUrls: ['./emp.component.scss'],
})
export class EmpComponent implements OnInit {
  constructor(private translate: TranslateService) {
    
    const browserLang = localStorage.getItem('lang') ?? 'en';
    this.translate.use(browserLang);
  }
  ngOnInit(): void {}
}

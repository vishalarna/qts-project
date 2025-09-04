import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { Router } from '@angular/router';
import { TranslateService } from '@ngx-translate/core';
import { AuthService } from 'src/app/_Services/Auth/auth.service';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-index',
  templateUrl: './index.component.html',
  styleUrls: ['./index.component.scss'],
  encapsulation: ViewEncapsulation.None,
})
export class IndexComponent implements OnInit {
  QTD_Domain: string = environment.QTD;
  EMP_Domain: string = environment.EMP;
  Admin_Domain: string = environment.Admin;

  constructor(
    private translate: TranslateService,
    private authService: AuthService,
    private route:Router
  ) {
    
    const browserLang = localStorage.getItem('lang') ?? 'en';
    this.translate.use(browserLang);
  }

  ngOnInit(): void {}

  signOut() {
    this.authService.logout();
  }
  redirectToInstances(){
    this.route.navigate(['/home/instance-selection'])
  }
}

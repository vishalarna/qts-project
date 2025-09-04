import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { MatSidenav } from '@angular/material/sidenav';
import { ActivatedRoute, ActivatedRouteSnapshot } from '@angular/router';
import { TranslateService } from '@ngx-translate/core';
import { DataBroadcastService } from 'src/app/_Shared/services/DataBroadcast.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';

@Component({
  selector: 'app-jta',
  templateUrl: './jta.component.html',
  styleUrls: ['./jta.component.scss'],
})
export class JtaComponent implements OnInit, AfterViewInit {
  constructor(
    private translate: TranslateService,
    private dataBroadcastService: DataBroadcastService
  ) {
    const browserLang = localStorage.getItem('lang') ?? 'en';
    this.translate.use(browserLang);
    this.dataBroadcastService.ToggleMainMenu.next('close');

  }

  ngAfterViewInit(): void {
  }

  ngOnInit(): void {
  }
}

import { TemplatePortal } from '@angular/cdk/portal';
import { Component, OnInit, ViewContainerRef } from '@angular/core';
import { Router } from '@angular/router';
import { TranslateService } from '@ngx-translate/core';
import { DataBroadcastService } from 'src/app/_Shared/services/DataBroadcast.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';

@Component({
  selector: 'app-task-ojt-link',
  templateUrl: './task-ojt-link.component.html',
  styleUrls: ['./task-ojt-link.component.scss'],
})
export class TaskOJTLinkComponent implements OnInit {
  editOjtTime: boolean = false;
  AddOjtTime: boolean = true;
  activeTab: string = 'timeEntry';
  constructor(
    private translate: TranslateService,
    private alert: SweetAlertService,
    private dataBroadcastService: DataBroadcastService,
    public flyPanelSrvc: FlyInPanelService,
    private vcr: ViewContainerRef
  ) {
    const browserLang = localStorage.getItem('lang') ?? 'en';
    this.translate.use(browserLang);

    this.dataBroadcastService.ShowMenuSideBar.next(false);
  }
  ngOnInit(): void {}
  openFlyInPanel(templateRef: any) {
    let portal = new TemplatePortal(templateRef, this.vcr);
    this.flyPanelSrvc.open(portal);
  }
}

import {Component, Input, OnInit} from '@angular/core';
import {Store} from '@ngrx/store';
import {ApiClientSettingsService} from "../../../../_Services/QTD/ClientSettings/api.clientsettings.service";
import {ApiEmployeesService} from "../../../../_Services/QTD/Employees/api.employees.service";
import {SweetAlertService} from 'src/app/_Shared/services/sweetalert.service';
import {
  ClientSettings_NotificationUpdateOptions
} from "../../../../_DtoModels/ClientsSettingsNotification/ClientSettings_NotificationUpdateOptions";
import {Subject} from 'rxjs';
import {TranslateService} from '@ngx-translate/core';
import {
  ClientSettings_Notification
} from "../../../../_DtoModels/ClientsSettingsNotification/ClientSettings_Notification";
import { DataBroadcastService } from 'src/app/_Shared/services/DataBroadcast.service';
 
@Component({
  selector: 'app-email-notifications',
  templateUrl: './email-notifications.component.html',
  styleUrls: ['./email-notifications.component.scss']
})
export class EmailNotificationsComponent implements OnInit {
  public ClientNotifications: Array<ClientSettings_Notification> = [];
  public Employees: Array<any>;
  eventsSubject: Subject<ClientSettings_Notification> = new Subject<ClientSettings_Notification>();
  public SelectedClientNotification: any;
  public selectedClientNotificationData: ClientSettings_Notification;
  public defaultMode: string;
  public isSubmitClicked: boolean;
  public UpdateOptions: ClientSettings_NotificationUpdateOptions;
  public isPublicFeatureEnabled:boolean;
 
  constructor(private store: Store<{ toggle: string }>,
              private clientSettingsService: ApiClientSettingsService,
              private employeeService: ApiEmployeesService,
              private alertService: SweetAlertService,
              private translate: TranslateService,
              private dataBroadcastService:DataBroadcastService
  ) {
  }
 
  async ngOnInit(): Promise<any> {
    this.dataBroadcastService.publicClassEnabled.subscribe((x) => {
      this.isPublicFeatureEnabled = x;
    })
    this.isSubmitClicked = false;
    this.Employees = [];
    this.defaultMode = "read";
    await this.getNotifications();
    await this.getEmployees();
    this.initializePutOptionsModel();
  }
 
  initializePutOptionsModel(): void {
    this.UpdateOptions = new ClientSettings_NotificationUpdateOptions();
  }
 
  async getEmployees() {
    await this.employeeService
      .getAll()
      .then((data: any) => {
        this.Employees = data;
      });
  }
 
  async getNotifications() {
    const data = await this.clientSettingsService.getNotifications();
    this.ClientNotifications = data.locList;
    if(!this.isPublicFeatureEnabled){
      this.ClientNotifications = this.ClientNotifications.filter(x=>!x.name.includes('Public Class Schedule Request'))
    }
  }
 
  getOnSaveSettingsClickEvent(value: any) {
    const notification = this.SelectedClientNotification;
    this.isSubmitClicked = true;
    this.clientSettingsService.updateNotification(notification, this.UpdateOptions)
      .then((data) => {
        this.alertService.notificationSuccessToast(this.translate.instant("Saved Successfully"));
        this.updateClientSettings_Notification(notification);
      });
  }
 
  getSelectedNotificationScreen(item: any) {
    this.SelectedClientNotification = item;
    this.selectedClientNotificationData = this.ClientNotifications.filter(
      dd => dd.name === this.SelectedClientNotification
    )[0];
    this.defaultMode = "read";
    this.initializePutOptionsModel();
  }
 
  async updateClientSettings_Notification(name: string) {
    await this.clientSettingsService
      .getNotificationByName(name)
      .then((data: any) => {
       const index = this.ClientNotifications.findIndex(r => r.name === name);
       this.ClientNotifications.splice(index, 1);
       this.ClientNotifications.splice(index, 0, data);
      });
 
    this.defaultMode = "read";
  }
 
  getOnEditClickEvent(value: any) {
    this.defaultMode = value.mode;
  }
 
  getOnCancelClickEvent(value: any) {
    const refreshNotificationData = this.ClientNotifications.filter(dd => dd.name === this.SelectedClientNotification)[0];
    this.eventsSubject.next(refreshNotificationData);
    this.defaultMode = value.mode;
    this.initializePutOptionsModel();
  }
 
  getCertificationExpirationChangeValue(event: ClientSettings_NotificationUpdateOptions) {
    this.UpdateOptions = event;
  }
 
  public getEmpDefaultChangeValues(event: ClientSettings_NotificationUpdateOptions) {
    this.UpdateOptions = event;
  }
 
  public getClassScheduleChangeValues(event: ClientSettings_NotificationUpdateOptions) {
    this.UpdateOptions = event;
  }
 
  getPublicClassScheduleRequestChangeValue(event: ClientSettings_NotificationUpdateOptions){
    this.UpdateOptions = event;
  }
}
import {Component, OnInit, Input, Output, EventEmitter, ViewChild} from '@angular/core';
import { UntypedFormGroup } from '@angular/forms';
import { Certification } from '@models/Certification/Certification';
import {Observable, Subject, Subscription} from 'rxjs';
import {ClientSettings_Notification} from 'src/app/_DtoModels/ClientsSettingsNotification/ClientSettings_Notification';
import {
  ClientSettings_NotificationUpdateOptions,
  CustomSetting_Notification_Setting,
  StepCustomSetting
} from 'src/app/_DtoModels/ClientsSettingsNotification/ClientSettings_NotificationUpdateOptions';
import { CertificationService } from 'src/app/_Services/QTD/certification.service';

@Component({
  selector: 'app-certification-expiration',
  templateUrl: './certification-expiration.component.html',
  styleUrls: ['./certification-expiration.component.scss']
})
export class CertificationExpirationComponent implements OnInit {
  constructor(private certificationService: CertificationService) {
  }

  @Input() mode: string;

  @Input() notification: ClientSettings_Notification;

  @Input() modelBinderItems: Array<any>;
  @Input() events: Observable<ClientSettings_Notification>;
  @Output() notificationChangedEvent: EventEmitter<ClientSettings_NotificationUpdateOptions> = new EventEmitter();

  public clientSettingsNotificationUpdateOptions: ClientSettings_NotificationUpdateOptions;
  eventToggle: Subject<boolean> = new Subject<boolean>();
  eventFirstTemplate: Subject<string> = new Subject<string>();
  eventSecondTemplate: Subject<string> = new Subject<string>();
  eventThirdTemplate: Subject<string> = new Subject<string>();
  firstEmailSetting: Subject<any> = new Subject<any>();
  secondEmailSetting: Subject<any> = new Subject<any>();
  thirdEmailSetting: Subject<any> = new Subject<any>();
  public IsPanelOpen: Boolean = false;
  private eventsSubscription: Subscription;
  caretPos: any;
  caretPosEnd: any;
  certificateList: Certification[] = [];
  certificates: Certification[] =[];
  certificateForm: UntypedFormGroup;
  certificatearray: any[] = [];

  ngOnInit(): void {
    this.initializeNotificationCustomSetting();
    this.setInitialValues();
    this.getAllCertificates();
    this.setInitialCertificates();
  }

  setInitialCertificates(){
    var value = this.getCustomSettingsValue('CertificationType');
    this.certificatearray = value.split(",");
  }

  initializeNotificationCustomSetting(): void {
    this.clientSettingsNotificationUpdateOptions = new ClientSettings_NotificationUpdateOptions();
  }

  public setInitialValues() {
    this.eventsSubscription = this.events.subscribe((data) => {
      this.notification = data;
      this.eventToggle.next(data.enabled);
      this.eventFirstTemplate.next(data.steps[0].template);
      this.eventSecondTemplate.next(data.steps[1].template);
      this.eventThirdTemplate.next(data.steps[2].template);
      this.firstEmailSetting.next(data.steps[0].customSettings);
      this.secondEmailSetting.next(data.steps[1].customSettings);
      this.thirdEmailSetting.next(data.steps[2].customSettings);
    });
  }

  public handleCertificationTypeChange(value: string) {
    const customSetting = new CustomSetting_Notification_Setting('CertificationType', value);
    this.clientSettingsNotificationUpdateOptions.UpdateCustomSetting(customSetting);
    this.notificationChangedEvent.emit(this.clientSettingsNotificationUpdateOptions);
  }

  public handleEmailAddressEditorChangeEvent(setting: any, order: number) {

    const stepCustomSetting = new StepCustomSetting(order, setting.key, setting.value);
    this.clientSettingsNotificationUpdateOptions.UpdateStepCustomSetting(stepCustomSetting);
    this.notificationChangedEvent.emit(this.clientSettingsNotificationUpdateOptions);
  }

  public handleEmailTemplateModifiedEvent(templateEditDetails: any) {
    this.clientSettingsNotificationUpdateOptions.updateTemplate(templateEditDetails);
    this.notificationChangedEvent.emit(this.clientSettingsNotificationUpdateOptions);
  }

  public handleToggleValueChange(value: boolean) {
    if (this.notification.enabled == value) {
      this.clientSettingsNotificationUpdateOptions.ClearEnableDisable();
    } else if (value) {
      this.clientSettingsNotificationUpdateOptions.Enable();
    } else if (!value) {
      this.clientSettingsNotificationUpdateOptions.Disable();
    }
    this.notificationChangedEvent.emit(this.clientSettingsNotificationUpdateOptions);
  }

  handleCustomSettingChange(order: number, setting: string, value: string) {
    const step = this.notification.steps.filter(dd => dd.order == order);
    const customSetting = step[0].customSettings.filter(r => r.key === setting)[0];
    let updateCustomSetting = new StepCustomSetting(order, setting, value);
    if (customSetting && customSetting.value === value) {
      this.clientSettingsNotificationUpdateOptions.RemoveStepCustomSetting(updateCustomSetting);
    } else {
      this.clientSettingsNotificationUpdateOptions.UpdateStepCustomSetting(updateCustomSetting);
    }
    this.notificationChangedEvent.emit(this.clientSettingsNotificationUpdateOptions);
  }

  public openNav() {
    this.IsPanelOpen = !this.IsPanelOpen;
  }

  closeModel() {
    this.IsPanelOpen = false;
  }

  GetChildData(Data, id: number) {

  }

  select(event) {
    var range = window.getSelection()?.getRangeAt(0);
    this.caretPos = range?.startOffset;
    this.caretPosEnd = range?.endOffset;
  }

  getStepCustomSettingsValue(key: string, order: number) {
    const step = this.notification.steps.filter(dd => dd.order == order)[0];
    return step.customSettings.filter(r => r.key.toUpperCase() == key.toUpperCase())[0].value;
  }

  getCustomSettingsValue(key: string) {
    const cs = this.notification.customSettings.filter(dd => dd.key.toUpperCase() == key.toUpperCase())[0];
    return cs.value;
  }

  async getAllCertificates(){
    this.certificateList = await this.certificationService.getAll();
    this.certificates = this.certificateList.filter(item =>item.certifyingBody.isNERC == false);
  }

  onCertificateClick(id: string): void {
    const index = this.certificatearray.indexOf(id);
    if (index > -1) {
      this.certificatearray.splice(index, 1)
    }
    else {
      this.certificatearray.push(id);
    }
    const value = this.certificatearray.join(",");
    this.handleCertificationTypeChange(value);
  }

  getCertificateName(id: string): string {
    if (id === 'NERC') {
      return 'NERC';
    }
    const cert = this.certificates.find(c => c.certAcronym === id);
    return cert ? cert.name : "";
  }

  onNERCSelection(): void {
    const nercValue = 'NERC';
    const index = this.certificatearray.indexOf(nercValue);
    if (index > -1) {
      this.certificatearray.splice(index, 1);
    } else {
      this.certificatearray.push(nercValue);
    }
    const value = this.certificatearray.join(",");
    this.handleCertificationTypeChange(value);
  }

  removeCertificate(item: number | string): void {
    this.certificatearray = this.certificatearray.filter(cert => cert !== item);
    const value = this.certificatearray.join(",");
    this.handleCertificationTypeChange(value);
  }

  get getLinkedCerts(): (number | string)[] {
    let linked = this.certificates
      .filter(x => this.certificatearray.includes(x.certAcronym))
      .map(m => m.certAcronym);

    if (this.certificatearray.includes('NERC')) {
      linked.push('NERC');
    }
    return linked;
  }

}



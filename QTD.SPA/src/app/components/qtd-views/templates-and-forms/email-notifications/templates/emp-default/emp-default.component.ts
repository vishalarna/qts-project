import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import {
  ClientSettings_NotificationUpdateOptions,
  CustomSetting_Notification_TemplateEdit
} from 'src/app/_DtoModels/ClientsSettingsNotification/ClientSettings_NotificationUpdateOptions';
import { Observable, Subject, Subscription } from 'rxjs';
import {
  ClientSettings_Notification
} from "../../../../../../_DtoModels/ClientsSettingsNotification/ClientSettings_Notification";

@Component({
  selector: 'app-emp-default',
  templateUrl: './emp-default.component.html',
  styleUrls: ['./emp-default.component.scss']
})
export class EmpDefaultComponent implements OnInit {
  @Input() notification: ClientSettings_Notification;
  @Input() checked: boolean;
  @Input() mode: string;
  @Input() empDefaultData: string;
  @Input() timingText: string;
  @Input() defaultEditor: any;
  @Input() modelBinderItems: Array<any>;
  @Input() emailData: string;
  @Output()
  notificationChangedEvent: EventEmitter<ClientSettings_NotificationUpdateOptions> = new EventEmitter();

  public IsPanelOpen: Boolean = false;
  public modelBindData: string;

  public clientSettingsNotificationUpdateOptions: ClientSettings_NotificationUpdateOptions;

  caretPos: any;
  caretPosEnd: any;
  anyData: any;
  private eventsSubscription: Subscription;
  eventsSubject: Subject<boolean> = new Subject<boolean>();
  emailSubjectData: Subject<string> = new Subject<string>();
  modeSubject: Subject<string> = new Subject<string>();
  @Input() events: Observable<ClientSettings_Notification>;

  constructor() {
  }

  ngOnInit(): void {
    this.initializeNotificationCustomSetting();
    this.checked = this.notification.enabled;
    this.eventsSubscription = this.events.subscribe((data) => {
      this.eventsSubject.next(data.enabled);
      const template = data.steps[0].template;
      this.emailSubjectData.next(template);
    });
  }

  initializeNotificationCustomSetting(): void {
    this.clientSettingsNotificationUpdateOptions = new ClientSettings_NotificationUpdateOptions();
  }

  public openNav() {
    this.IsPanelOpen = !this.IsPanelOpen;
  }

  closeModel() {
    this.IsPanelOpen = false;
  }

  GetChildData(Data) {
    this.modelBindData = Data;
    this.empDefaultData = this.empDefaultData.slice(0, this.caretPos) + this.modelBindData + this.empDefaultData.slice(this.caretPosEnd);
  }

  select(event) {
    var range = window.getSelection()?.getRangeAt(0);
    this.caretPos = range?.startOffset;
    this.caretPosEnd = range?.endOffset;
  }

  handleEmailTemplateModifiedEvent(templateEditDetails:CustomSetting_Notification_TemplateEdit) {
    this.clientSettingsNotificationUpdateOptions.updateTemplate(templateEditDetails);
    this.notificationChangedEvent.emit(this.clientSettingsNotificationUpdateOptions);
  }

  handleToggleValueChange(value: boolean) {
    if (value) {
      this.clientSettingsNotificationUpdateOptions.Enable();
    } else if (!value) {
      this.clientSettingsNotificationUpdateOptions.Disable();
    }
    this.notificationChangedEvent.emit(this.clientSettingsNotificationUpdateOptions);
  }

  public(): void {
  }

}


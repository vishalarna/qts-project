import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { ClientSettings_Notification } from '@models/ClientsSettingsNotification/ClientSettings_Notification';
import { ClientSettings_NotificationUpdateOptions, CustomSetting_Notification_Setting, StepCustomSetting } from '@models/ClientsSettingsNotification/ClientSettings_NotificationUpdateOptions';
import { Observable, Subject, Subscription } from 'rxjs';

@Component({
  selector: 'app-public-class-schedule-request',
  templateUrl: './public-class-schedule-request.component.html',
  styleUrls: ['./public-class-schedule-request.component.scss'] 
})
export class PublicClassScheduleRequestComponent implements OnInit {
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
    public IsPanelOpen: Boolean = false;
    private eventsSubscription: Subscription;
    caretPos: any;
    caretPosEnd: any;
  constructor() { }

  ngOnInit(): void {
    this.initializeNotificationCustomSetting();
    this.setInitialValues();
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
    });
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

  public handleEmailTemplateModifiedEvent(templateEditDetails: any) {
    this.clientSettingsNotificationUpdateOptions.updateTemplate(templateEditDetails);
    this.notificationChangedEvent.emit(this.clientSettingsNotificationUpdateOptions);
  }

  closeModel() {
    this.IsPanelOpen = false;
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

   public handleEmailAddressEditorChangeEvent(setting: any, order: number) {
  
      const stepCustomSetting = new StepCustomSetting(order, setting.key, setting.value);
      this.clientSettingsNotificationUpdateOptions.UpdateStepCustomSetting(stepCustomSetting);
      this.notificationChangedEvent.emit(this.clientSettingsNotificationUpdateOptions);
    }
   
}

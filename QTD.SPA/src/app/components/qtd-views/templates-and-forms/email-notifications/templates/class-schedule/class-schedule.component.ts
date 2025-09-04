import { Component, Input, OnInit, Output, EventEmitter } from '@angular/core';
import { Observable, Subject, Subscription } from 'rxjs';
import { ClientSettings_Notification } from 'src/app/_DtoModels/ClientsSettingsNotification/ClientSettings_Notification';
import {
  ClientSettings_NotificationUpdateOptions,
  CustomSetting_Notification_Recipients,
  CustomSetting_Notification_TemplateEdit,
  StepCustomSetting,
} from 'src/app/_DtoModels/ClientsSettingsNotification/ClientSettings_NotificationUpdateOptions';
import { Employee } from '../../../../../../_DtoModels/Employee/Employee';
import { UntypedFormControl, UntypedFormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-class-schedule',
  templateUrl: './class-schedule.component.html',
  styleUrls: ['./class-schedule.component.scss'],
})
export class ClassScheduleComponent implements OnInit {
  @Input() notification: ClientSettings_Notification;
  @Input() mode: string;

  @Input() emailData: string;

  @Input() editor: any;
  @Input() events: Observable<ClientSettings_Notification>;

  @Input()
  public employees: Array<Employee>;
  @Input()
  public isScheduleForNext: boolean = true;

  @Output()
  notificationChangedEvent: EventEmitter<ClientSettings_NotificationUpdateOptions> =
    new EventEmitter();

  public IsPanelOpen: Boolean = false;

  public modelBindData: string;

  public clientSettingsNotificationUpdateOptions: ClientSettings_NotificationUpdateOptions;

  private caretPos: any;
  private caretPosEnd: any;
  private eventsSubscription: Subscription;
  eventsSubject: Subject<boolean> = new Subject<boolean>();
  eventsTemplate: Subject<string> = new Subject<string>();
  selectedFrequency: string;
  dayOfWeek: string;
  dayOfMonth: string;
  daysOfWeek: string[];
  daysInMonth: string[];
  classScheduleForm:UntypedFormGroup
  constructor() {}

  ngOnInit(): void {
    this.daysOfWeek = [
      'Sunday',
      'Monday',
      'Tuesday',
      'Wednesday',
      'Thursday',
      'Friday',
      'Saturday',
    ];
    this.daysInMonth = Array.from({ length: 31 }, (_, i) => (i + 1).toString());
    this.initializeClassForm();
    this.initializeNotificationCustomSetting();
    this.setInitialValues();
    if (this.notification.name == 'Admin - Employee Portal Completions'){
      this.getInitialFrequency();
      this.setTimeValues();
    }
  }
  
   ngAfterViewInit(){
    if (this.notification.name == 'Admin - Employee Portal Completions'){
       this.onDayChange(null);
       this.onMonthChange(null);
    }
  }

  initializeNotificationCustomSetting(): void {
    this.clientSettingsNotificationUpdateOptions =
      new ClientSettings_NotificationUpdateOptions();
  }

  setInitialValues() {
    this.eventsSubscription = this.events.subscribe((data) => {
      this.notification = data;
      this.eventsSubject.next(data.enabled);
      this.eventsTemplate.next(data.steps[0].template);
      this.resetInputs();
    });
  }

  initializeClassForm() {
    this.classScheduleForm = new UntypedFormGroup({
      dayTime: new UntypedFormControl('', Validators.required),
    })
  }

  handleNotificationToggleValue(value: any) {
    if (this.notification.enabled == value) {
      this.clientSettingsNotificationUpdateOptions.ClearEnableDisable();
    } else if (value) {
      this.clientSettingsNotificationUpdateOptions.Enable();
    } else if (!value) {
      this.clientSettingsNotificationUpdateOptions.Disable();
    }
    this.notificationChangedEvent.emit(
      this.clientSettingsNotificationUpdateOptions
    );
  }

  handleEmailTemplateModifiedEvent(
    templateEditDetails: CustomSetting_Notification_TemplateEdit
  ) {
    this.clientSettingsNotificationUpdateOptions.updateTemplate(
      templateEditDetails
    );
    this.notificationChangedEvent.emit(
      this.clientSettingsNotificationUpdateOptions
    );
  }

  handleRecipientChange(value: any) {
    for (var checked of value.checked) {
      var recipient = new CustomSetting_Notification_Recipients(checked, 1);

      if (
        this.notification.steps[0].recipients.filter(
          (r) => r.employeeId == checked
        )[0]
      ) {
        this.clientSettingsNotificationUpdateOptions.RemoveFromAddRecipient(
          recipient
        );
      } else {
        this.clientSettingsNotificationUpdateOptions.AddToAddRecipient(
          recipient
        );
      }
    }

    for (var unchecked of value.unchecked) {
      var recipient = new CustomSetting_Notification_Recipients(unchecked, 1);

      if (
        this.notification.steps[0].recipients.filter(
          (r) => r.employeeId == unchecked
        )[0]
      ) {
        this.clientSettingsNotificationUpdateOptions.AddToRemoveRecipient(
          recipient
        );
      } else {
        this.clientSettingsNotificationUpdateOptions.RemoveFromRemoveRecipient(
          recipient
        );
      }
    }

    this.notificationChangedEvent.emit(
      this.clientSettingsNotificationUpdateOptions
    );
  }

  public openNav() {
    this.IsPanelOpen = !this.IsPanelOpen;
  }

  closeModel() {
    this.IsPanelOpen = false;
  }

  GetChildData(Data) {
    this.modelBindData = Data;
    this.emailData =
      this.emailData.slice(0, this.caretPos) +
      this.modelBindData +
      this.emailData.slice(this.caretPos);
  }

  select(event) {
    var range = window.getSelection()?.getRangeAt(0);
    this.caretPos = range?.startOffset;
    this.caretPosEnd = range?.endOffset;
  }

  handleCustomSettingChange(setting: string, value: string) {
    const customSetting = this.notification.steps[0].customSettings.filter(
      (r) => r.key === setting
    )[0];
    let updateCustomSetting = new StepCustomSetting(1, setting, value);

    if (customSetting && customSetting.value === value) {
      this.clientSettingsNotificationUpdateOptions.RemoveStepCustomSetting(
        updateCustomSetting
      );
    } else {
      this.clientSettingsNotificationUpdateOptions.UpdateStepCustomSetting(
        updateCustomSetting
      );
    }

    this.notificationChangedEvent.emit(
      this.clientSettingsNotificationUpdateOptions
    );
  }

  getStepCustomSettingsValue(key: string) {
    return this.notification?.steps[0].customSettings.filter((r) => r.key === key)[0]?.value;
  }

  getInitialFrequency(){
    const emailFrequency = this.getStepCustomSettingsValue('Email Frequency');
    this.selectedFrequency = emailFrequency;
  }

   onFrequencyChange(frequency?: string) {
    const emailFrequency = frequency || this.getStepCustomSettingsValue('Email Frequency');
    this.selectedFrequency = emailFrequency;
    this.handleCustomSettingChange('Email Frequency',this.selectedFrequency);
    this.resetInputs();
  }

    onDayChange(day:string) {
    const selectedDayOfweekValue = day ||  this.getStepCustomSettingsValue('Day of Week');
      this.dayOfWeek = selectedDayOfweekValue;
      this.handleCustomSettingChange('Day of Week',this.dayOfWeek);
  }
   onMonthChange(time:string) {
    const selectedDayOfMonthValue = time || this.getStepCustomSettingsValue('Day # of Month');
     this.dayOfMonth = selectedDayOfMonthValue;
     this.handleCustomSettingChange('Day # of Month',this.dayOfMonth)
  }
  resetInputs() {
    this.dayOfMonth = this.getStepCustomSettingsValue('Day # of Month');
    this.dayOfWeek = this.getStepCustomSettingsValue('Day of Week');
    this.setTimeValues();
  }

setTimeValues() {
  let time = this.getStepCustomSettingsValue('Time of Day'); 
  this.classScheduleForm.patchValue({ dayTime: time });
}

onTimeSelected()
{
  var dayTime = this.classScheduleForm.get('dayTime')?.value;
  this.handleCustomSettingChange('Time of Day', dayTime);
}

}

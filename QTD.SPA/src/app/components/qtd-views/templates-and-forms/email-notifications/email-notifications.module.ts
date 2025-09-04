import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {HttpClientModule} from '@angular/common/http';
import {BrowserModule} from '@angular/platform-browser';
import {LocalizeModule} from 'src/app/_Shared/modules/localize.module';
import {TopNavBarModule} from "./top-nav-bar/top-nav-bar.module";
import {EmailAddressEditorModule} from "./email-address-editor/email-address-editor.module";
import {
  EmailNotificationNavigationBarModule
} from "./email-notification-navigation-bar/email-notification-navigation-bar.module";
import {EmployeePickerModule} from "./employee-picker/employee-picker.module";
import {NotificationsEnabledModule} from "./notifications-enabled/notifications-enabled.module";
import {EmailNotificationsComponent} from "./email-notifications.component";
import {ClassScheduleModule} from "./templates/class-schedule/class-schedule.module";
import {CertificationExpirationModule} from "./templates/certification-expiration/certification-expiration.module";
import {EmpDefaultModule} from "./templates/emp-default/emp-default.module";
import { PublicClassScheduleRequestModule } from './templates/public-class-schedule-request/public-class-schedule-request.module';

@NgModule({
  declarations: [
    EmailNotificationsComponent
  ],
  imports: [
    CommonModule,
    HttpClientModule,
    LocalizeModule,
    EmailNotificationNavigationBarModule,
    ClassScheduleModule,
    CertificationExpirationModule,
    EmpDefaultModule,
    TopNavBarModule,
    PublicClassScheduleRequestModule
  ],
})
export class EmailNotificationsModule {
}

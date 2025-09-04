import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {TemplatesAndFormsComponent} from './templates-and-forms.component';
import {HttpClientModule} from '@angular/common/http';
import {TemplatesAndFormsRoutingModule} from './templates-and-forms-routing.module';
import {LocalizeModule} from 'src/app/_Shared/modules/localize.module';
import {EmailNotificationsModule} from "./email-notifications/email-notifications.module";


@NgModule({
  declarations: [
    TemplatesAndFormsComponent
  ],
  imports: [
    CommonModule,
    HttpClientModule,
    LocalizeModule,
    EmailNotificationsModule,
    TemplatesAndFormsRoutingModule
  ],
})
export class TemplatesAndFormsModule {
}

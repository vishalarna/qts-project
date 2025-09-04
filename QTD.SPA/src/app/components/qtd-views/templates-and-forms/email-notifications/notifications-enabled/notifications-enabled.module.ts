import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {HttpClientModule} from '@angular/common/http';
import {BrowserModule} from '@angular/platform-browser';
import {LocalizeModule} from 'src/app/_Shared/modules/localize.module';
import {NotificationsEnabledComponent} from "./notifications-enabled.component";
import {FormsModule} from "@angular/forms";
import { BaseModule } from 'src/app/components/base/base.module';

@NgModule({
  declarations: [
    NotificationsEnabledComponent
  ],
  imports: [
    CommonModule,
    HttpClientModule,
    LocalizeModule,
    FormsModule,
    BaseModule
  ],
  exports: [
    NotificationsEnabledComponent
  ]
})
export class NotificationsEnabledModule {
}

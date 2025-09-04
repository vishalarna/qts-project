import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {HttpClientModule} from '@angular/common/http';
import {LocalizeModule} from 'src/app/_Shared/modules/localize.module';
import {EmailNotificationNavigationBarComponent} from "./email-notification-navigation-bar.component";
import {MatIconModule} from "@angular/material/icon";
import { BaseModule } from 'src/app/components/base/base.module';

@NgModule({
  declarations: [
    EmailNotificationNavigationBarComponent
  ],
  imports: [
    CommonModule,
    HttpClientModule,
    LocalizeModule,
    MatIconModule,
    BaseModule
  ],
  exports: [
    EmailNotificationNavigationBarComponent
  ]
})
export class EmailNotificationNavigationBarModule {
}

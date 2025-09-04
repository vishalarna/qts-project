import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {HttpClientModule} from '@angular/common/http';
import {LocalizeModule} from 'src/app/_Shared/modules/localize.module';
import {ModelBindingsModule} from "../../model-bindings/model-bindings.module";
import {EmailTemplateEditorModule} from "../../email-template-editor/email-template-editor.module";
import {EmpDefaultComponent} from "./emp-default.component";
import {MatIconModule} from "@angular/material/icon";
import {NotificationsEnabledModule} from "../../notifications-enabled/notifications-enabled.module";
import { BaseModule } from 'src/app/components/base/base.module';

@NgModule({
  declarations: [
    EmpDefaultComponent
  ],
  imports: [
    CommonModule,
    HttpClientModule,
    LocalizeModule,
    ModelBindingsModule,
    EmailTemplateEditorModule,
    MatIconModule,
    NotificationsEnabledModule,
    BaseModule
  ],
  exports: [
    EmpDefaultComponent
  ]
})
export class EmpDefaultModule {
}

import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {HttpClientModule} from '@angular/common/http';
import {LocalizeModule} from 'src/app/_Shared/modules/localize.module';
import {ModelBindingsModule} from "../../model-bindings/model-bindings.module";
import {ClassScheduleComponent} from "./class-schedule.component";
import {EmployeePickerModule} from "../../employee-picker/employee-picker.module";
import {EmailTemplateEditorModule} from "../../email-template-editor/email-template-editor.module";
import {NotificationsEnabledModule} from "../../notifications-enabled/notifications-enabled.module";
import {MatIconModule} from "@angular/material/icon";
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BaseModule } from 'src/app/components/base/base.module';
import { MatLegacyFormFieldModule as MatFormFieldModule } from '@angular/material/legacy-form-field';
import { MatLegacyInputModule as MatInputModule } from '@angular/material/legacy-input';


@NgModule({
  declarations: [
    ClassScheduleComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    BaseModule,
    HttpClientModule,
    LocalizeModule,
    ModelBindingsModule,
    EmployeePickerModule,
    EmailTemplateEditorModule,
    NotificationsEnabledModule,
    MatIconModule,
    MatFormFieldModule,
    ReactiveFormsModule,
    MatInputModule
  ],
  exports: [
    ClassScheduleComponent
  ]
})
export class ClassScheduleModule {
}

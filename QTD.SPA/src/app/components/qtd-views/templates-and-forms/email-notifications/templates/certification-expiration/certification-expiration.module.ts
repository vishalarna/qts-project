import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {HttpClientModule} from '@angular/common/http';
import {LocalizeModule} from 'src/app/_Shared/modules/localize.module';
import {CertificationExpirationComponent} from "./certification-expiration.component";
import {EmailAddressEditorModule} from "../../email-address-editor/email-address-editor.module";
import {EmailTemplateEditorModule} from "../../email-template-editor/email-template-editor.module";
import {ModelBindingsModule} from "../../model-bindings/model-bindings.module";
import {EmpDefaultComponent} from "../emp-default/emp-default.component";
import {NotificationsEnabledModule} from "../../notifications-enabled/notifications-enabled.module";
import {MatIconModule} from "@angular/material/icon";
import { BaseModule } from 'src/app/components/base/base.module';
import { MatLegacySelectModule as MatSelectModule } from '@angular/material/legacy-select';
import { MatLegacyChipsModule as MatChipsModule } from '@angular/material/legacy-chips';
import { MatLegacyFormFieldModule as MatFormFieldModule } from '@angular/material/legacy-form-field';
import { MatLegacyInputModule as MatInputModule } from '@angular/material/legacy-input';

@NgModule({
  declarations: [
    CertificationExpirationComponent
  ],
  imports: [
    BaseModule,
    CommonModule,
    HttpClientModule,
    LocalizeModule,
    EmailAddressEditorModule,
    EmailTemplateEditorModule,
    ModelBindingsModule,
    NotificationsEnabledModule,
    MatIconModule,
    MatSelectModule,
    MatChipsModule,
    MatFormFieldModule,
    MatInputModule,
    MatIconModule 
  ],
  exports: [
    CertificationExpirationComponent
  ]
})
export class CertificationExpirationModule {
}

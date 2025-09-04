import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {HttpClientModule} from '@angular/common/http';
import {BrowserModule} from '@angular/platform-browser';
import {LocalizeModule} from 'src/app/_Shared/modules/localize.module';
import {EmailAddressEditorComponent} from "./email-address-editor.component";
import {FormsModule} from "@angular/forms";
import {BaseModule} from "../../../../base/base.module";
import {MatLegacyChipsModule as MatChipsModule} from "@angular/material/legacy-chips";
import {MatLegacyFormFieldModule as MatFormFieldModule} from "@angular/material/legacy-form-field";

@NgModule({
  declarations: [
    EmailAddressEditorComponent
  ],
  imports: [
    CommonModule,
    HttpClientModule,
    LocalizeModule,
    FormsModule,
    BaseModule,
    MatChipsModule,
    MatFormFieldModule
  ],
  exports: [
    EmailAddressEditorComponent
  ]
})
export class EmailAddressEditorModule {
}

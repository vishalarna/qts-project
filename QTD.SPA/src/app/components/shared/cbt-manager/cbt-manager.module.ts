import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CbtManagerComponent } from './cbt-manager.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatLegacyFormFieldModule as MatFormFieldModule } from '@angular/material/legacy-form-field';
import { CKEditorModule } from '@ckeditor/ckeditor5-angular';
import { BaseModule } from 'src/app/components/base/base.module';
import { MatLegacyRadioModule as MatRadioModule } from '@angular/material/legacy-radio';
import { MatLegacySelectModule as MatSelectModule } from '@angular/material/legacy-select';
import {LocalizeModule} from 'src/app/_Shared/modules/localize.module';

import { HttpClientModule } from '@angular/common/http';

@NgModule({
  declarations: [
    CbtManagerComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    MatFormFieldModule,
    ReactiveFormsModule,
    CKEditorModule,
    BaseModule,
    ReactiveFormsModule,
    HttpClientModule,
    MatRadioModule,
    MatSelectModule,
    LocalizeModule
  ],
  exports: [
    CbtManagerComponent
  ]
})

export class CbtManagerModule { }

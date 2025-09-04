import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatLegacyFormFieldModule as MatFormFieldModule } from '@angular/material/legacy-form-field';
import { CKEditorModule } from '@ckeditor/ckeditor5-angular';
import { BaseModule } from 'src/app/components/base/base.module';
import { MatLegacyRadioModule as MatRadioModule } from '@angular/material/legacy-radio';
import { MatLegacySelectModule as MatSelectModule } from '@angular/material/legacy-select';
import {LocalizeModule} from 'src/app/_Shared/modules/localize.module';

import { HttpClientModule } from '@angular/common/http';
import { ImportCsvWizardComponent } from './import-csv-wizard.component';
import { MatStepperModule } from '@angular/material/stepper';
import { MatToolbarModule } from '@angular/material/toolbar';
import { LayoutModule } from '../../qtd-views/layout/layout.module';
import { MatLegacyTableModule as MatTableModule } from '@angular/material/legacy-table';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';

@NgModule({
  declarations: [
    ImportCsvWizardComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    LayoutModule,
    MatFormFieldModule,
    ReactiveFormsModule,
    CKEditorModule,
    BaseModule,
    ReactiveFormsModule,
    HttpClientModule,
    MatStepperModule,
    MatToolbarModule,
    LocalizeModule,
    MatTableModule,
    MatCheckboxModule
  ],
  exports: [
    ImportCsvWizardComponent
  ]
})

export class ImportCsvWizardModule { }

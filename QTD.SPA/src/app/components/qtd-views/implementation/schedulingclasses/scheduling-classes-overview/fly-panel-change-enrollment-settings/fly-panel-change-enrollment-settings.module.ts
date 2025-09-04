import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FlyPanelChangeEnrollmentSettingsComponent } from './fly-panel-change-enrollment-settings.component';
import { BaseModule } from 'src/app/components/base/base.module';
import { LayoutModule } from '@angular/cdk/layout';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { MatLegacyFormFieldModule as MatFormFieldModule } from '@angular/material/legacy-form-field';
import { MatLegacySelectModule as MatSelectModule } from '@angular/material/legacy-select';
import { MatStepperModule } from '@angular/material/stepper';
import { MatLegacyTableModule as MatTableModule } from '@angular/material/legacy-table';
import { CKEditorModule } from '@ckeditor/ckeditor5-angular';
import { MatLegacyInputModule as MatInputModule } from '@angular/material/legacy-input';



@NgModule({
  declarations: [
    FlyPanelChangeEnrollmentSettingsComponent
  ],
  imports: [
    CommonModule,
    BaseModule,
    FormsModule,
    ReactiveFormsModule,
    MatCheckboxModule,
    MatFormFieldModule,
    MatSelectModule,
    MatStepperModule,
    CKEditorModule,
    MatTableModule,
    LayoutModule,
    MatInputModule
  ],
  exports:[FlyPanelChangeEnrollmentSettingsComponent]
})
export class FlyPanelChangeEnrollmentSettingsModule { }

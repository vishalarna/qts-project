import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { IlaEmpCbtSettingsComponent } from './ila-emp-cbt-settings.component';
import { BaseModule } from 'src/app/components/base/base.module';
import { MatLegacySelectModule as MatSelectModule } from '@angular/material/legacy-select';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CKEditorModule } from '@ckeditor/ckeditor5-angular';



@NgModule({
  declarations: [
    IlaEmpCbtSettingsComponent
  ],
  imports: [
    CommonModule,
    BaseModule,
    MatSelectModule,
    MatCheckboxModule,
    FormsModule,
    CKEditorModule,
    ReactiveFormsModule,
  ],
  exports: [
    IlaEmpCbtSettingsComponent,
  ]
})
export class IlaEmpCbtSettingsModule { }

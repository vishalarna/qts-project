import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { IlaEmpSchedulingComponent } from './ila-emp-scheduling.component';
import { BaseModule } from 'src/app/components/base/base.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatLegacyCheckbox as MatCheckbox, MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { CKEditorModule } from '@ckeditor/ckeditor5-angular';



@NgModule({
  declarations: [
    IlaEmpSchedulingComponent
  ],
  imports: [
    CommonModule,
    BaseModule,
    FormsModule,
    ReactiveFormsModule,
    MatCheckboxModule,
    CKEditorModule
  ],
  exports: [
    IlaEmpSchedulingComponent,
  ]
})
export class IlaEmpSchedulingModule { }

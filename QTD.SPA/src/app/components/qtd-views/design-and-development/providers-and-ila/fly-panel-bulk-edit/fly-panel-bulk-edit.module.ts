import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FlyPanelBulkEditComponent } from './fly-panel-bulk-edit.component';
import { BaseModule } from 'src/app/components/base/base.module';
import { MatStepperModule } from '@angular/material/stepper';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { CKEditorModule } from '@ckeditor/ckeditor5-angular';
import { MatLegacyTableModule as MatTableModule } from '@angular/material/legacy-table';
import { MatLegacySelectModule as MatSelectModule } from '@angular/material/legacy-select';



@NgModule({
  declarations: [
    FlyPanelBulkEditComponent
  ],
  imports: [
    CommonModule,
    BaseModule,
    MatStepperModule,
    MatCheckboxModule,
    CKEditorModule,
    MatTableModule,
    MatSelectModule,
    FormsModule,
    ReactiveFormsModule
  ],
  exports: [FlyPanelBulkEditComponent],
})
export class FlyPanelBulkEditModule { }

import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AddEmpRecurrDialogComponent } from './add-emp-recurr-dialog.component';
import { BaseModule } from 'src/app/components/base/base.module';
import { MatLegacyDialogModule as MatDialogModule } from '@angular/material/legacy-dialog';
import { MatLegacySelectModule as MatSelectModule } from '@angular/material/legacy-select';
import { ReactiveFormsModule } from '@angular/forms';



@NgModule({
  declarations: [
    AddEmpRecurrDialogComponent
  ],
  imports: [
    CommonModule,
    BaseModule,
    MatDialogModule,
    MatSelectModule,
    ReactiveFormsModule,
  ],
  exports: [
    AddEmpRecurrDialogComponent
  ]
})
export class AddEmpRecurrDialogModule { }

import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { StudentEvalWithoutEmpComponent } from './student-eval-without-emp.component';
import { BaseModule } from 'src/app/components/base/base.module';
import { MatLegacyTableModule as MatTableModule } from '@angular/material/legacy-table';
import { MatLegacyPaginatorModule as MatPaginatorModule } from '@angular/material/legacy-paginator';
import { MatSortModule } from '@angular/material/sort';
import { FillEvalFormDialogModule } from '../fill-eval-form-dialog/fill-eval-form-dialog.module';



@NgModule({
  declarations: [
    StudentEvalWithoutEmpComponent
  ],
  imports: [
    CommonModule,
    BaseModule,
    MatTableModule,
    MatPaginatorModule,
    MatSortModule,
    FillEvalFormDialogModule,
  ],
  exports: [
    StudentEvalWithoutEmpComponent
  ]
})
export class StudentEvalWithoutEmpModule { }

import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { StudentEvalWithEmpComponent } from './student-eval-with-emp.component';
import { BaseModule } from 'src/app/components/base/base.module';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { MatIconModule } from '@angular/material/icon';
import { MatLegacyMenuModule as MatMenuModule } from '@angular/material/legacy-menu';
import { MatLegacySelectModule as MatSelectModule } from '@angular/material/legacy-select';
import { MatLegacyTableModule as MatTableModule } from '@angular/material/legacy-table';
import { MatLegacyTooltipModule as MatTooltipModule } from '@angular/material/legacy-tooltip';
import { NgChartsModule } from 'ng2-charts';



@NgModule({
  declarations: [
    StudentEvalWithEmpComponent
  ],
  imports: [
    CommonModule,
    BaseModule,
    MatTableModule,
    NgChartsModule,
    MatSelectModule,
    MatMenuModule,
    ReactiveFormsModule,
    MatIconModule,
    FormsModule,
    MatTooltipModule,
  ],
  exports:[
    StudentEvalWithEmpComponent,
  ]
})
export class StudentEvalWithEmpModule { }

import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FlyPanelEnrollEmployeeComponent } from './fly-panel-enroll-employee.component';
import { FormsModule } from '@angular/forms';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { BaseModule } from 'src/app/components/base/base.module';
import { MatLegacyTableModule as MatTableModule } from '@angular/material/legacy-table';
import { MatLegacyPaginatorModule as MatPaginatorModule } from '@angular/material/legacy-paginator';
import { PlannedDateDialogueModule } from '../planned-date-dialogue/planned-date-dialogue.module';
import { EnrollmentDateDialogueModule } from '../enrollment-date-dialogue/enrollment-date-dialogue.module';
import { MatSortModule } from '@angular/material/sort';



@NgModule({
  declarations: [
    FlyPanelEnrollEmployeeComponent
  ],
  imports: [
    CommonModule,
    BaseModule,
    FormsModule,
    MatCheckboxModule,
    MatTableModule,
    MatPaginatorModule,
    PlannedDateDialogueModule,
    EnrollmentDateDialogueModule,
    MatSortModule
  ],
  
  exports:[FlyPanelEnrollEmployeeComponent]
})
export class FlyPanelEnrollEmployeeModule { }

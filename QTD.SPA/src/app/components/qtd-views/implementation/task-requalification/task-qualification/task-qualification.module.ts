import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TaskQualificationComponent } from './task-qualification.component';
import { BaseModule } from 'src/app/components/base/base.module';
import { MatLegacyInputModule as MatInputModule } from '@angular/material/legacy-input';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatIconModule } from '@angular/material/icon';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { MatLegacyTableModule as MatTableModule } from '@angular/material/legacy-table';
import { FlypanelTaskRequalFilterModule } from '../flypanel-task-requal-filter/flypanel-task-requal-filter.module';
import { FlypanelTaskRequalFilterByEmpModule } from '../flypanel-task-requal-filter-by-emp/flypanel-task-requal-filter-by-emp.module';
import { MatLegacyMenuModule as MatMenuModule } from '@angular/material/legacy-menu';
import { TaskRequallByEmpModule } from '../task-requall-by-emp/task-requall-by-emp.module';
import { MatSortModule } from '@angular/material/sort';
import { MatLegacyPaginatorModule as MatPaginatorModule } from '@angular/material/legacy-paginator';



@NgModule({
  declarations: [
    TaskQualificationComponent
  ],
  imports: [
    CommonModule,
    BaseModule,
    MatInputModule,
    ReactiveFormsModule,
    FormsModule,
    MatIconModule,
    MatCheckboxModule,
    MatTableModule,
    FlypanelTaskRequalFilterModule,
    FlypanelTaskRequalFilterByEmpModule,
    MatMenuModule,
    TaskRequallByEmpModule,
    MatSortModule,
    MatPaginatorModule,
  ],
  exports : [
    TaskQualificationComponent,
  ]
})
export class TaskQualificationModule { }

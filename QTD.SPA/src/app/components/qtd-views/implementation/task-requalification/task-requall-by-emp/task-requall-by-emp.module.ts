import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TaskRequallByEmpComponent } from './task-requall-by-emp.component';
import { BaseModule } from 'src/app/components/base/base.module';
import { MatLegacyTableModule as MatTableModule } from '@angular/material/legacy-table';
import { MatLegacyMenuModule as MatMenuModule } from '@angular/material/legacy-menu';
import { FlypanelTaskQualifiedModule } from '../flypanel-task-qualified/flypanel-task-qualified.module';
import { FlypanelAddTaskQualificationModule } from '../flypanel-add-task-qualification/flypanel-add-task-qualification.module';
import { MatLegacyInputModule as MatInputModule } from '@angular/material/legacy-input';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatIconModule } from '@angular/material/icon';
import { MatSortModule } from '@angular/material/sort';
import { FlypanelFilterEmployeeTaskQualificationModule } from '../flypanel-filter-employee-task-qualification/flypanel-filter-employee-task-qualification.module';



@NgModule({
  declarations: [
    TaskRequallByEmpComponent
  ],
  imports: [
    CommonModule,
    BaseModule,
    MatTableModule,
    MatMenuModule,
    FlypanelTaskQualifiedModule,
    FlypanelAddTaskQualificationModule,
    MatInputModule,
    FormsModule,
    ReactiveFormsModule,
    MatIconModule,
    MatSortModule,
    FlypanelFilterEmployeeTaskQualificationModule
  ],
  exports : [
    TaskRequallByEmpComponent
  ]
})
export class TaskRequallByEmpModule { }

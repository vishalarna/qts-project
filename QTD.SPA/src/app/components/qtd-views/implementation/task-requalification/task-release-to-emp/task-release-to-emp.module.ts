import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TaskReleaseToEmpComponent } from './task-release-to-emp.component';
import { BaseModule } from 'src/app/components/base/base.module';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { MatIconModule } from '@angular/material/icon';
import { MatLegacyTableModule as MatTableModule } from '@angular/material/legacy-table';
import { MatLegacyInputModule as MatInputModule } from '@angular/material/legacy-input';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatLegacyMenuModule as MatMenuModule } from '@angular/material/legacy-menu';
import { TaskRequalUpdateDateDialogModule } from '../task-requal-update-date-dialog/task-requal-update-date-dialog.module';
import { TaskRequalReassignTaskRequalModule } from '../task-requal-reassign-task-requal/task-requal-reassign-task-requal.module';
import { RecallTaskQualificationDialogModule } from '../recall-task-qualification-dialog/recall-task-qualification-dialog.module';
import { FlypanelReleaseTaskQualificationModule } from '../flypanel-release-task-qualification/flypanel-release-task-qualification.module';
import { FlypanelReleaseTaskQualByEmpModule } from '../flypanel-release-task-qual-by-emp/flypanel-release-task-qual-by-emp.module';
import { MatLegacyPaginatorModule as MatPaginatorModule } from '@angular/material/legacy-paginator';
import { MatSortModule } from '@angular/material/sort';



@NgModule({
  declarations: [
    TaskReleaseToEmpComponent
  ],
  imports: [
    CommonModule,
    BaseModule,
    MatCheckboxModule,
    MatIconModule,
    MatTableModule,
    MatInputModule,
    ReactiveFormsModule,
    FormsModule,
    MatMenuModule,
    TaskRequalUpdateDateDialogModule,
    TaskRequalReassignTaskRequalModule,
    RecallTaskQualificationDialogModule,
    FlypanelReleaseTaskQualificationModule,
    FlypanelReleaseTaskQualByEmpModule,
    MatPaginatorModule,
    MatSortModule,
  ],
  exports : [
    TaskReleaseToEmpComponent,
  ]
})
export class TaskReleaseToEmpModule { }

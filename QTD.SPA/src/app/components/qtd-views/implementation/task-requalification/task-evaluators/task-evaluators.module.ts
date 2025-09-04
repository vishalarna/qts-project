import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TaskEvaluatorsComponent } from './task-evaluators.component';
import { BaseModule } from 'src/app/components/base/base.module';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { MatLegacyTableModule as MatTableModule } from '@angular/material/legacy-table';
import { MatIconModule } from '@angular/material/icon';
import { MatLegacyInputModule as MatInputModule } from '@angular/material/legacy-input';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { FlypanelAssignedTaskQualificationModule } from '../flypanel-assigned-task-qualification/flypanel-assigned-task-qualification.module';
import { MatLegacyPaginatorModule as MatPaginatorModule } from '@angular/material/legacy-paginator';
import { MatSortModule } from '@angular/material/sort';



@NgModule({
  declarations: [
    TaskEvaluatorsComponent
  ],
  imports: [
    CommonModule,
    BaseModule,
    MatCheckboxModule,
    MatTableModule,
    MatIconModule,
    MatInputModule,
    ReactiveFormsModule,
    FormsModule,
    FlypanelAssignedTaskQualificationModule,
    MatPaginatorModule,
    MatSortModule
  ],
  exports : [
    TaskEvaluatorsComponent,
  ]
})
export class TaskEvaluatorsModule { }

import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TaskProceduresComponent } from './task-procedures.component';
import { BaseModule } from 'src/app/components/base/base.module';
import { MatLegacyTableModule as MatTableModule } from '@angular/material/legacy-table';
import { MatLegacyPaginatorModule as MatPaginatorModule } from '@angular/material/legacy-paginator';
import { MatSortModule } from '@angular/material/sort';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { FlyPanelTaskProcedureLinkModule } from '../../fly-panel-task-procedure-link/fly-panel-task-procedure-link.module';
import { FlyPanelLinkedTasksModule } from '../../fly-panel-linked-tasks/fly-panel-linked-tasks.module';



@NgModule({
  declarations: [
    TaskProceduresComponent
  ],
  imports: [
    CommonModule,
    BaseModule,
    MatTableModule,
    MatPaginatorModule,
    MatSortModule,
    MatCheckboxModule,
    FlyPanelTaskProcedureLinkModule,
    FlyPanelLinkedTasksModule,
  ],
  exports : [
    TaskProceduresComponent
  ]
})
export class TaskProceduresModule { }

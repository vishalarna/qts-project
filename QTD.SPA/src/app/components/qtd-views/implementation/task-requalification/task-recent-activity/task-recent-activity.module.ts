import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TaskRecentActivityComponent } from './task-recent-activity.component';
import { BaseModule } from 'src/app/components/base/base.module';
import { MatLegacyTableModule as MatTableModule } from '@angular/material/legacy-table';
import { MatIconModule } from '@angular/material/icon';
import { MatLegacyTooltipModule as MatTooltipModule } from '@angular/material/legacy-tooltip';
import { MatLegacyMenuModule as MatMenuModule } from '@angular/material/legacy-menu';
import { FlypanelAddTaskQualificationModule } from '../flypanel-add-task-qualification/flypanel-add-task-qualification.module';
import { MatLegacyPaginatorModule as MatPaginatorModule } from '@angular/material/legacy-paginator';
import { MatSortModule } from '@angular/material/sort';



@NgModule({
  declarations: [
    TaskRecentActivityComponent
  ],
  imports: [
    CommonModule,
    BaseModule,
    MatTableModule,
    MatIconModule,
    MatTooltipModule,
    MatMenuModule,
    FlypanelAddTaskQualificationModule,
    MatPaginatorModule,
    MatSortModule,
  ],
  exports : [
    TaskRecentActivityComponent,
  ]
})
export class TaskRecentActivityModule { }

import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TaskListReviewTasksComponent } from './task-list-review-tasks.component';
import { MatLegacyTableModule as MatTableModule } from '@angular/material/legacy-table';
import { MatTreeModule } from '@angular/material/tree';
import { MatIconModule } from '@angular/material/icon';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { MatLegacyMenuModule as MatMenuModule } from '@angular/material/legacy-menu';
import { MatExpansionModule } from '@angular/material/expansion';
import { FlyPanelAddReviewersModule } from '../task-list-review-details/fly-panel-add-reviewers/fly-panel-add-reviewers.module';
import { MatLegacySelectModule as MatSelectModule } from '@angular/material/legacy-select';
import { LayoutModule } from '@angular/cdk/layout';
import { BaseModule } from 'src/app/components/base/base.module';
import { FlypanelTaskListReviewTasksModule } from './flypanel-task-list-review-tasks/flypanel-task-list-review-tasks.module';
import { TaskReviewTableModule } from './task-review-table/task-review-table.module';


@NgModule({
  declarations: [TaskListReviewTasksComponent],
  imports: [
    CommonModule,
    BaseModule,
    LayoutModule,
    MatSelectModule,
    MatExpansionModule,
    MatMenuModule,
    MatCheckboxModule,
    MatIconModule,
    MatTreeModule,
    MatTableModule,
    FlypanelTaskListReviewTasksModule,
    TaskReviewTableModule
  ],
  exports:[TaskListReviewTasksComponent]
})
export class TaskListReviewTasksModule { }
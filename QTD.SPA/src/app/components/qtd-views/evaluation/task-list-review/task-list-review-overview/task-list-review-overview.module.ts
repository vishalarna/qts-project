import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatLegacyTableModule as MatTableModule } from '@angular/material/legacy-table';
import { FormsModule } from '@angular/forms';
import { LayoutModule } from '../../../layout/layout.module';
import { TaskListReviewOverviewComponent } from './task-list-review-overview.component';
import { RouterModule, Routes } from '@angular/router';
import { BaseModule } from 'src/app/components/base/base.module';
import { MatLegacyMenuModule as MatMenuModule } from '@angular/material/legacy-menu';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { FlyPanelFilterTaskListReviewModule } from '../fly-panel-filter-task-list-review/fly-panel-filter-task-list-review.module';
import { MatSortModule } from '@angular/material/sort';
import { TaskReviewTableModule } from '../task-list-review-create-wizard/task-list-review-tasks/task-review-table/task-review-table.module';
import { TaskListReviewGenerateReportModule } from '../task-list-review-generate-report/task-list-review-generate-report.module';
import { FlyPanelCreateQtdUserModule } from '../task-list-review-create-wizard/task-list-review-details/fly-panel-create-qtd-user/fly-panel-create-qtd-user.module';

const routes: Routes = [
  {
    path: '',
    component: TaskListReviewOverviewComponent,
  }
];

@NgModule({
  declarations: [TaskListReviewOverviewComponent],
  imports: [
    CommonModule,
    MatTableModule,
    FormsModule,
    LayoutModule,
    RouterModule.forChild(routes),
    BaseModule,
    MatMenuModule,
    MatCheckboxModule,
    FlyPanelFilterTaskListReviewModule,
    MatSortModule,
    TaskReviewTableModule,
    TaskListReviewGenerateReportModule,
    FlyPanelCreateQtdUserModule
  ],
  exports:[TaskListReviewOverviewComponent]
})
export class TaskListReviewOverviewModule { }

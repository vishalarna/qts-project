import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TaskRequalificationOverviewComponent } from './task-requalification-overview.component';
import { RouterModule, Routes } from '@angular/router';
import { BaseModule } from 'src/app/components/base/base.module';
import { LayoutModule } from '../../../layout/layout.module';
import { MatLegacyTabsModule as MatTabsModule } from '@angular/material/legacy-tabs';
import { TaskEvaluatorsModule } from '../task-evaluators/task-evaluators.module';
import { TaskQualificationModule } from '../task-qualification/task-qualification.module';
import { TaskRecentActivityModule } from '../task-recent-activity/task-recent-activity.module';
import { TaskReleaseToEmpModule } from '../task-release-to-emp/task-release-to-emp.module';
import { FlypanelWithoutPositionModule } from '../flypanel-without-position/flypanel-without-position.module';
import { FlypanelWithoutTasksQualificationsModule } from '../flypanel-without-tasks-qualifications/flypanel-without-tasks-qualifications.module';
import { FlypanelPendingTaskQualificationModule } from '../flypanel-pending-task-qualification/flypanel-pending-task-qualification.module';
import { TaskRequallByEmpModule } from '../task-requall-by-emp/task-requall-by-emp.module';

const routes: Routes = [
  {
    path: '',
    component: TaskRequalificationOverviewComponent,
  }
 ]

@NgModule({
  declarations: [TaskRequalificationOverviewComponent],
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    BaseModule,
    LayoutModule,
    MatTabsModule,
    TaskEvaluatorsModule,
    TaskQualificationModule,
    TaskRecentActivityModule,
    TaskReleaseToEmpModule,
    FlypanelWithoutPositionModule,
    FlypanelWithoutTasksQualificationsModule,
    FlypanelPendingTaskQualificationModule,
    TaskRequallByEmpModule,
  ],
  exports: [TaskRequalificationOverviewComponent]
})
export class TaskRequalificationOverviewModule { }

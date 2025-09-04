import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TaskRequalificationComponent } from './task-requalification.component';
import { RouterModule, Routes } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';
import { LocalizeModule } from 'src/app/_Shared/modules/localize.module';
import { FormsModule } from '@angular/forms';
import { TaskRequalificationOverviewComponent } from './task-requalification-overview/task-requalification-overview.component';
import { AuthGuard } from 'src/app/_Guards/auth.guard';
import { RouteGuard } from 'src/app/_Guards/route.guard';
import { FlypanelWithoutTasksQualificationsComponent } from './flypanel-without-tasks-qualifications/flypanel-without-tasks-qualifications.component';
import { FlypanelPendingTaskQualificationComponent } from './flypanel-pending-task-qualification/flypanel-pending-task-qualification.component';
import { TaskRequalReassignTaskRequalComponent } from './task-requal-reassign-task-requal/task-requal-reassign-task-requal.component';

const routes: Routes = [
  {
    path: '',
    component: TaskRequalificationComponent,
    children:
      [
        {
          path: '',
          redirectTo: 'overview',
          pathMatch: 'full',
        },
        {
          path: 'overview',
          canActivate: [AuthGuard, RouteGuard],
          loadChildren: () =>
            import('./task-requalification-overview/task-requalification-overview.module').then(
              (m) => m.TaskRequalificationOverviewModule
            ),
        },
        {
          path: 'linkedEmp',
          canActivate: [AuthGuard, RouteGuard],
          loadChildren: () =>
            import('./task-requal-emp-linked/task-requal-emp-linked.module').then(
              (m) => m.TaskRequalEmpLinkedModule
            ),
        },
      ]
  },
];

@NgModule({
  declarations: [TaskRequalificationComponent],
  imports: [
    CommonModule,
    HttpClientModule,
    RouterModule.forChild(routes),
    LocalizeModule,
    FormsModule,
  ],
  exports: [TaskRequalificationComponent]
})
export class TaskRequalificationModule { }

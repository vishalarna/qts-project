import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TaskReQualificationComponent } from './task-re-qualification.component';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from 'src/app/_Guards/auth.guard';
import { RouteGuard } from 'src/app/_Guards/route.guard';

const routes: Routes = [
  {
    path: '',
    component: TaskReQualificationComponent,
    children: [
      {

        path: 'overview',
        canActivate: [AuthGuard, RouteGuard],
        loadChildren: () =>
          import('./task-re-qualification-overview/task-re-qualification-overview.module').then(
            (m) => m.TaskReQualificationOverviewModule
          ),
      },
      {

        path: 'view-task',
        canActivate: [AuthGuard, RouteGuard],
        loadChildren: () =>
          import('./fly-panel-task-re-qualification-task-feedback/fly-panel-task-re-qualification-task-feedback.module').then(
            (m) => m.FlyPanelTaskReQualificationTaskFeedbackModule
          )
      },
      {

        path: 'view-feedback',
        canActivate: [AuthGuard, RouteGuard],
        loadChildren: () =>
          import('./fly-panel-task-re-qualification-comp-feedback/fly-panel-task-re-qualification-comp-feedback.module').then(
            (m) => m.FlyPanelTaskReQualificationCompFeedbackModule
          ),
      },
      {

        path: 'task-suggestions',
        canActivate: [AuthGuard, RouteGuard],
        loadChildren: () =>
          import('./fly-panel-task-re-qualification-suggestions/fly-panel-task-re-qualification-suggestions.module').then(
            (m) => m.FlyPanelTaskReQualificationSuggestionsModule
          ),
      },
      {

        path: '',
        redirectTo:'overview',
        pathMatch:'full'
      },

    ],
  },
];

@NgModule({
  declarations: [
    TaskReQualificationComponent
  ],
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
  ]
})
export class TaskReQualificationModule { }

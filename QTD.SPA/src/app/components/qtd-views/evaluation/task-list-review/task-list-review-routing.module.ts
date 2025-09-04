import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from 'src/app/_Guards/auth.guard';
import { RouteGuard } from 'src/app/_Guards/route.guard';

const routes: Routes = [
  {
    path: '',
    children: [     
      {
        path: 'overview',
        canActivate: [AuthGuard, RouteGuard],
        loadChildren: () =>
          import('./task-list-review-overview/task-list-review-overview.module').then(
            (m) => m.TaskListReviewOverviewModule
          ),
      }, 
      {
        path: 'create',
        canActivate: [AuthGuard, RouteGuard],
        loadChildren: () =>
          import(
            './task-list-review-create-wizard/task-list-review-create-wizard.module'
          ).then((m) => m.TaskListReviewCreateWizardModule),
      },
      {
        path: 'edit/:id',
        canActivate: [AuthGuard, RouteGuard],
        loadChildren: () =>
          import('./task-list-review-create-wizard/task-list-review-create-wizard.module').then(
            (m) => m.TaskListReviewCreateWizardModule
          ),
      },
      {
        path: ':taskListReviewId/edit/taskReview/:id',
        canActivate: [AuthGuard, RouteGuard],
        loadChildren: () =>
          import('./task-review/task-review.module').then(
            (m) => m.TaskReviewModule
          ),
      },
    ],
  },
];

@NgModule({
  imports: [CommonModule, RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class TaskListReviewRoutingModule {}

import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { EvaluationComponent } from './evaluation.component';
import { AuthGuard } from 'src/app/_Guards/auth.guard';
import { RouteGuard } from 'src/app/_Guards/route.guard';
const routes: Routes = [
  {
    path: '',
    component: EvaluationComponent,
    children: [
      {
        path: '',
        redirectTo: 'studentevaluation',
        pathMatch: 'full',
      },
      {
        path: 'studentevaluation',
        canActivate: [AuthGuard, RouteGuard],
        loadChildren: () =>
          import('./student-evaluation/student-evaluation.module').then(
            (m) => m.StudentEvaluationModule
          ),
      },
      {
        path: 'bulkedit',
        canActivate: [AuthGuard, RouteGuard],
        loadChildren: () =>
          import('./bulk-edit-evaluation/bulk-edit-evaluation.module').then((m) => m.BulkEditEvaluationModule),
      },
      {
        path: 'trainingprogram-review',
        canActivate: [AuthGuard, RouteGuard],
        loadChildren: () =>
          import('./training-program-review/training-program-review.module').then((m) => m.TrainingProgramReviewModule),
      },  
      {
        path: 'task-list-review',
        canActivate: [AuthGuard, RouteGuard],
        loadChildren: () =>
          import('./task-list-review/task-list-review.module').then(
            (m) => m.TaskListReviewModule
          ),
      },
      {
        path: 'training-issues',
        canActivate: [AuthGuard, RouteGuard],
        loadChildren: () =>
          import('./training-issues/training-issues.module').then(
            (m) => m.TrainingIssuesModule
          ),
      },
    ],
  },
];

@NgModule({
  declarations: [
    EvaluationComponent
  ],
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
  ]
})
export class EvaluationModule { }

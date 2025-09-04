import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ProcedureReviewComponent } from './procedure-review.component';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from 'src/app/_Guards/auth.guard';
import { RouteGuard } from 'src/app/_Guards/route.guard';
import { StartPrcedureReviewDialogModule } from './start-prcedure-review-dialog/start-prcedure-review-dialog.module';


const routes: Routes = [
  {
    path: '',
    component: ProcedureReviewComponent,
    children: [
      {

        path: 'overview',
        canActivate: [AuthGuard, RouteGuard],
        loadChildren: () =>
          import('./procedure-review-overview/procedure-review-overview.module').then(
            (m) => m.ProcedureReviewOverviewModule
          ),
      },
      {

        path: 'start-review',
        canActivate: [AuthGuard, RouteGuard],
        loadChildren: () =>
          import('./fly-panel-start-procedure-review/fly-panel-start-procedure-review.module').then(
            (m) => m.FlyPanelStartProcedureReviewModule
          ),
      },
      {

        path: 'review-result',
        canActivate: [AuthGuard, RouteGuard],
        loadChildren: () =>
          import('./fly-panel-result-procedure-review/fly-panel-result-procedure-review.module').then(
            (m) => m.FlyPanelResultProcedureReviewModule
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
    ProcedureReviewComponent
  ],
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    StartPrcedureReviewDialogModule
  ]
})
export class ProcedureReviewModule { }

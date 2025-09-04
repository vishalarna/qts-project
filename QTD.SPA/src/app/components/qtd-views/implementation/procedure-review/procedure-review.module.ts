import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ProcedureReviewComponent } from './procedure-review.component';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from 'src/app/_Guards/auth.guard';
import { RouteGuard } from 'src/app/_Guards/route.guard';

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

        path: 'add',
        canActivate: [AuthGuard, RouteGuard],
        loadChildren: () =>
          import('./fly-panel-add-procedure-review/fly-panel-add-procedure-review.module').then(
            (m) => m.FlyPanelAddProcedureReviewModule
          ),
      },
      {

        path: 'edit',
        canActivate: [AuthGuard, RouteGuard],
        loadChildren: () =>
          import('./fly-panel-add-procedure-review/fly-panel-add-procedure-review.module').then(
            (m) => m.FlyPanelAddProcedureReviewModule
          ),
      },
      {

        path: 'procedure-enroll',
        canActivate: [AuthGuard, RouteGuard],
        loadChildren: () =>
          import('./procedure-review-detail/procedure-review-detail.module').then(
            (m) => m.ProcedureReviewDetailModule
          ),
      },
      // {

      //   path: 'start-test',
      //   canActivate: [AuthGuard, RouteGuard],
      //   loadChildren: () =>
      //     import('./fly-panel-start-test/fly-panel-start-test.module').then(
      //       (m) => m.FlyPanelStartTestModule
      //     ),
      // },
      // {

      //   path: 'test-result',
      //   canActivate: [AuthGuard, RouteGuard],
      //   loadChildren: () =>
      //     import('./fly-panel-test-result/fly-panel-test-result.module').then(
      //       (m) => m.FlyPanelTestResultModule
      //     ),
      // },
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
  ]
})
export class ProcedureReviewModule { }

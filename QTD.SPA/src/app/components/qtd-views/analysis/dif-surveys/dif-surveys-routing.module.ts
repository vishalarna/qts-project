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
          import('./dif-survey-overview/dif-survey-overview.module').then(
            (m) => m.DifSurveyOverviewModule
          ),
      },
      {
        path: 'create',
        canActivate: [AuthGuard, RouteGuard],
        loadChildren: () =>
          import(
            './dif-survey-create-wizard/dif-survey-create-wizard.module'
          ).then((m) => m.DifSurveyCreateWizardModule),
      },
      {
        path: 'edit/:id',
        canActivate: [AuthGuard, RouteGuard],
        loadChildren: () =>
          import('./dif-survey-create-wizard/dif-survey-create-wizard.module').then(
            (m) => m.DifSurveyCreateWizardModule
          ),
      },
      {
        path: ':id/enrollments',
        canActivate: [AuthGuard, RouteGuard],
        loadChildren: () =>
          import(
            './dif-survey-overview/view-enrollment/view-enrollment.module'
          ).then((m) => m.ViewEnrollmentModule),
      },
      {
        path: ':id/results',
        canActivate: [AuthGuard, RouteGuard],
        loadChildren: () =>
          import(
            './dif-survey-overview/view-dif-results/view-dif-results.module'
          ).then((m) => m.ViewDifResultsModule),
      },
      {
        path: ':id/import-results',
        canActivate: [AuthGuard, RouteGuard],
        loadChildren: () =>
          import(
            './dif-survey-overview/import-dif-survey/import-dif-survey.module'
          ).then((m) => m.ImportDifSurveyModule),
      },
    ],
  },
];

@NgModule({
  imports: [CommonModule, RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class DifSurveysRoutingModule {}

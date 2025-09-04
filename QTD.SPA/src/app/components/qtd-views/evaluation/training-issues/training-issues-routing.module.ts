import { NgModule } from '@angular/core';
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
          import(
            './training-issues-overview/training-issues-overview.module'
          ).then((m) => m.TrainingIssuesOverviewModule),
      },
      {
        path: 'create',
        canActivate: [AuthGuard, RouteGuard],
        loadChildren: () =>
          import(
            './training-issues-create-wizard/training-issues-create-wizard.module'
          ).then((m) => m.TrainingIssuesCreateWizardModule),
      },
      {
        path: ':id/actionItems',
        canActivate: [AuthGuard, RouteGuard],
        loadChildren: () =>
          import(
            './training-issue-view-action-items/training-issue-view-action-items.module'
          ).then((m) => m.TrainingIssueViewActionItemsModule),
      },
      {
        path: 'edit/:id',
        canActivate: [AuthGuard, RouteGuard],
        loadChildren: () =>
          import(
            './training-issues-create-wizard/training-issues-create-wizard.module'
          ).then((m) => m.TrainingIssuesCreateWizardModule),
      },
    ],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class TrainingIssuesRoutingModule {}

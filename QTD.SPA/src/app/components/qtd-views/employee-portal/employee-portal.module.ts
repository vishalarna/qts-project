import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { EmployeePortalComponent } from './employee-portal.component';
import { LocalizeModule } from 'src/app/_Shared/modules/localize.module';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule, Routes } from '@angular/router';
import { RouteGuard } from 'src/app/_Guards/route.guard';
import { AuthGuard } from 'src/app/_Guards/auth.guard';


const routes: Routes = [{
  path: '',
  component: EmployeePortalComponent,
  children: [
    {
      path: '',
      redirectTo: 'dashboard',
      pathMatch: 'full',
    },
    {
      path: 'evaluation',
      canActivate: [AuthGuard, RouteGuard],
      loadChildren: () =>
        import('./evaluation/evaluation.module').then(
          (m) => m.EvaluationModule
        )
    },
    {
      path:'evaluation/:id',
      canActivate:[AuthGuard, RouteGuard],
      loadChildren: () =>
        import('./evaluation/start-evaluation/start-evaluation.module').then(
          (m) => m.StartEvaluationModule
        )
    },
    {
      path: 'online-courses',
      canActivate: [AuthGuard, RouteGuard],
      loadChildren: () =>
        import('./online-courses/online-courses-overview/online-courses-overview.module').then(
          (m) => m.OnlineCoursesOverviewModule
        ),
    },
    {
      path: 'self-registration',
      canActivate: [AuthGuard, RouteGuard],
      loadChildren: () =>
        import('./self-registration/self-registration-overview/self-registration-overview.module').then(
          (m) => m.SelfRegistrationOverviewModule
        ),
    },
    {
      path: 'course-detail',
      canActivate: [AuthGuard, RouteGuard],
      loadChildren: () =>
        import('./self-registration/self-registration-overview/fly-panel-detail-self-registration/fly-panel-detail-self-registration.module').then(
          (m) => m.FlyPanelDetailSelfRegistrationModule
        ),
    },
    {
      path: 'procedure-review',
      canActivate: [AuthGuard, RouteGuard],
      loadChildren: () =>
        import('./procedure-review/procedure-review.module').then(
          (m) => m.ProcedureReviewModule
        )
    },
    {
      path: 'task-re-qualification',
      canActivate: [AuthGuard, RouteGuard],
      loadChildren: () =>
        import('./task-re-qualification/task-re-qualification.module').then(
          (m) => m.TaskReQualificationModule
        )
    },
    {
      path:'dashboard',
      canActivate: [AuthGuard, RouteGuard],
      loadChildren: () =>
        import(`./dashboard/dashboard.module`).then(
          (m) => m.DashboardModule
        )
    },
    {
      path:'dif-survey',
      canActivate: [AuthGuard, RouteGuard],
      loadChildren: () =>
        import(`./dif/dif.module`).then(
          (m) => m.DifModule
        )
    }
  ]
},
];


@NgModule({
  declarations: [
    EmployeePortalComponent
  ],
  imports: [
    CommonModule,
    LocalizeModule,
    HttpClientModule,
    RouterModule.forChild(routes),
  ],
  exports:[
    EmployeePortalComponent
  ]
})
export class EmployeePortalModule { }

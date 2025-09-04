import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ImplementationComponent } from './implementation.component';
import { HttpClientModule } from '@angular/common/http';
import { Routes, RouterModule } from '@angular/router';
import { AuthGuard } from 'src/app/_Guards/auth.guard';
import { RouteGuard } from 'src/app/_Guards/route.guard';
import { LocalizeModule } from 'src/app/_Shared/modules/localize.module';
import { ILAModule } from './ila/ila.module';
const routes: Routes = [
  {
    path: '',
    component: ImplementationComponent,
    children: [
      {
        path: 'employees',
        canActivate: [AuthGuard, RouteGuard],
        loadChildren: () => import('./emp/emp.module').then((m) => m.EmpModule),
      },
      {
        path: 'employee',
        canActivate: [AuthGuard, RouteGuard],
        loadChildren: () =>
          import('./emp/add-emp/add-emp.module').then((m) => m.AddEmpModule),
      },
      {
        path: 'employees/:id',
        canActivate: [AuthGuard, RouteGuard],
        loadChildren: () =>
          import('./emp/edit-view-emp/edit-view-emp.module').then(
            (m) => m.EditViewEmpModule
          ),
      },
      {
        path: 'position',
        canActivate: [AuthGuard, RouteGuard],
        loadChildren: () =>
          import('./positions/positions.module').then((m) => m.PositionsModule),
      },
      {
        path: 'certfication',
        canActivate: [AuthGuard, RouteGuard],
        loadChildren: () =>
          import('./certifications/certifications.module').then(
            (m) => m.CertificationsModule
          ),
      },
      {
        path: 'cert-body',
        canActivate: [AuthGuard, RouteGuard],
        loadChildren: () =>
          import('./certifying-body/certifying-body.module').then(
            (m) => m.CertifyingBodyModule
          ),
      },
      {
        path: 'organization',
        canActivate: [AuthGuard, RouteGuard],
        loadChildren: () =>
          import('./organizations/organizations.module').then(
            (m) => m.OrganizationsModule
          ),
      },
      {
        path: 'taskReQualification',
        canActivate: [AuthGuard, RouteGuard],
        loadChildren: () =>
        import('./task-requalification/task-requalification.module').then(
          (m) => m.TaskRequalificationModule
        ),
      },
      {
        path: 'sc',
        canActivate: [AuthGuard, RouteGuard],
        loadChildren: () =>
          import('./schedulingclasses/schedulingclasses.module').then(
            (m) => m.SchedulingclassesModule
          ),
      },
      {
        path: 'test',
        canActivate: [AuthGuard, RouteGuard],
        loadChildren: () =>
          import('./test/test.module').then(
            (m) => m.TestModule
          ),
      },
    ],
  },
];

@NgModule({
  declarations: [ImplementationComponent],
  imports: [
    CommonModule,
    HttpClientModule,
    RouterModule.forChild(routes),
    LocalizeModule,
    ILAModule
   ],
})
export class ImplementationModule {}

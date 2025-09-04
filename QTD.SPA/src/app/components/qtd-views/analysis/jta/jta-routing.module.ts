import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { RouteGuard } from 'src/app/_Guards/route.guard';
import { JTAMenuResolver } from 'src/app/_Resolvers/jtamenu.resolver';
import { JtaComponent } from './jta.component';

const routes: Routes = [
  {
    path: '',
    component: JtaComponent,

    children: [
      {
        path: 'task-detail',
        canActivate: [RouteGuard],
       // resolve: { jtamenu: JTAMenuResolver },
        loadChildren: () =>
          import('./task-detail/task-detail.module').then(
            (m) => m.TaskDetailModule
          ),
      },
      {
        path: 'task-pos-link',
        canActivate: [RouteGuard],
        loadChildren: () =>
          import('./task-position-link/task-position-link.module').then(
            (m) => m.TaskPositionLinkModule
          ),
      },
      {
        path: 'task-eo-link',
        canActivate: [RouteGuard],
        loadChildren: () =>
          import('./task-eo-link/task-eo-link.module').then(
            (m) => m.TaskEOLinkModule
          ),
      },
      {
        path: 'task-proc-link',
        canActivate: [RouteGuard],
        loadChildren: () =>
          import('./task-procedure-link/task-procedure-link.module').then(
            (m) => m.TaskProcedureLinkModule
          ),
      },
      {
        path: 'task-sh-link',
        canActivate: [RouteGuard],
        loadChildren: () =>
          import('./task-safty-hazard-link/task-safty-hazard-link.module').then(
            (m) => m.TaskSaftyHazardLinkModule
          ),
      },
      {
        path: 'task-ojt-link',
        canActivate: [RouteGuard],
        loadChildren: () =>
          import('./task-ojt-link/task-ojt-link.module').then(
            (m) => m.TaskOJTLinkModule
          ),
      },
    ],
  },
];
@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class JTARoutingModule {}

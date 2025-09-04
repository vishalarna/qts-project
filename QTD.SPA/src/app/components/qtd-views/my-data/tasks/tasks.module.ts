import { NgModule } from '@angular/core';
import { CommonModule, TitleCasePipe } from '@angular/common';
import { TasksComponent } from '../tasks/tasks.component';
import { TaskNavBarComponent } from './task-nav-bar/task-nav-bar.component';
import { TaskOverviewModule } from './task-overview/task-overview.module';
import { FlypanelAddTaskModule } from './flypanel-add-task/flypanel-add-task.module';
import { FlypanelAddDutyareaModule } from './flypanel-add-dutyarea/flypanel-add-dutyarea.module';
import { FlypanelAddSubdutyareaModule } from './flypanel-add-subdutyarea/flypanel-add-subdutyarea.module';

import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { MatLegacyMenuModule as MatMenuModule } from '@angular/material/legacy-menu';
import { MatSidenavModule } from '@angular/material/sidenav';
import { RouterModule, Routes } from '@angular/router';
import { BaseModule } from 'src/app/components/base/base.module';
import { LocalizeModule } from 'src/app/_Shared/modules/localize.module';
import { LayoutModule } from '../../layout/layout.module';
import { AuthGuard } from 'src/app/_Guards/auth.guard';
import { RouteGuard } from 'src/app/_Guards/route.guard';
import { MatTreeModule } from '@angular/material/tree';
import { MatIconModule } from '@angular/material/icon';

const routes: Routes = [
  {
    path: '',
    component: TasksComponent,
    children: [
      {
        path: '',
        redirectTo: 'overview',
        pathMatch: 'full',
      },
      {
        path: 'overview',
        canActivate: [AuthGuard, RouteGuard],
        loadChildren: () =>
          import('./task-overview/task-overview.module').then(
            (m) => m.TaskOverviewModule
          ),
      },
      {
        path: 'detail',
        canActivate: [AuthGuard, RouteGuard],
        loadChildren: () =>
          import('./task-detail/task-detail.module').then(
            (m) => m.TaskDetailModule
          ),
      },
      {
        path: 'da',
        canActivate: [AuthGuard, RouteGuard],
        loadChildren: () =>
          import('./dutyarea-detail/dutyarea-detail.module').then(
            (m) => m.DutyareaDetailModule
          ),
      },
      {
        path: 'sda',
        canActivate: [AuthGuard, RouteGuard],
        loadChildren: () =>
          import('./sub-dutyarea-detail/sub-dutyarea-detail.module').then(
            (m) => m.SubDutyareaDetailModule
          ),
      },
    ],
  },
];

@NgModule({
  declarations: [TasksComponent, TaskNavBarComponent],
  imports: [
    CommonModule,
    HttpClientModule,
    RouterModule.forChild(routes),
    LocalizeModule,
    MatSidenavModule,
    LayoutModule,
    BaseModule,
    MatMenuModule,
    TaskOverviewModule,
    FormsModule,
    FlypanelAddTaskModule,
    FlypanelAddDutyareaModule,
    FlypanelAddSubdutyareaModule,
    MatTreeModule,
    MatIconModule,
  ],
  providers: [TitleCasePipe],
})
export class TasksModule {}

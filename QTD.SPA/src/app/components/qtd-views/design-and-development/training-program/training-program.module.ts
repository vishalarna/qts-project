import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatLegacyCardModule as MatCardModule } from '@angular/material/legacy-card';
import { DragDropModule } from '@angular/cdk/drag-drop';
import { TrainingProgramComponent } from './training-program.component';
import { BaseModule } from 'src/app/components/base/base.module';
import { RouterModule, Routes } from '@angular/router';
import { LocalizeModule } from 'src/app/_Shared/modules/localize.module';
import { MatLegacySelectModule as MatSelectModule } from '@angular/material/legacy-select';
import { FlyPanelAddTrainingProgramComponent } from './fly-panel-add-training-program/fly-panel-add-training-program.component';
import { AuthGuard } from 'src/app/_Guards/auth.guard';
import { RouteGuard } from 'src/app/_Guards/route.guard';
import { MatSidenavModule } from '@angular/material/sidenav';
import { LayoutModule } from '../../layout/layout.module';

const routes: Routes = [
  {
    path: '',
    component: TrainingProgramComponent,
    children: [
      {
        path: '',
        canActivate: [AuthGuard, RouteGuard],
        loadChildren: () =>
          import('./training-program-overview/training-program-overview.module').then(
            (m) => m.TrainingProgramOverviewModule
          ),
      },
      {
        path: 'create',
        canActivate: [AuthGuard, RouteGuard],
        loadChildren: () =>
          import('./add-new-training-program/add-new-training-program.module').then(
            (m) => m.AddNewTrainingProgramModule
          ),
      },
      {
        path: 'edit/:id',
        canActivate: [AuthGuard, RouteGuard],
        loadChildren: () =>
          import('./add-new-training-program/add-new-training-program.module').then(
            (m) => m.AddNewTrainingProgramModule
          ),
      },
      {
        path: 'copy/:id',
        canActivate: [AuthGuard, RouteGuard],
        loadChildren: () =>
          import('./add-new-training-program/add-new-training-program.module').then(
            (m) => m.AddNewTrainingProgramModule
          ),
      },
    ],
  },
];

@NgModule({
  declarations: [TrainingProgramComponent],
  imports: [
    CommonModule,
    MatCardModule,
    DragDropModule,
    LocalizeModule,
    BaseModule,
    RouterModule.forChild(routes),
    MatSelectModule,
    CommonModule,
    MatSidenavModule,
    LayoutModule
  ],
})
export class TrainingProgramModule {}

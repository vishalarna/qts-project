import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TrainingProgramReviewComponent } from './training-program-review.component';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from 'src/app/_Guards/auth.guard';
import { RouteGuard } from 'src/app/_Guards/route.guard';
import { LayoutModule } from '../../layout/layout.module';
import { MatLegacyCardModule as MatCardModule } from '@angular/material/legacy-card';
import { MatSidenavModule } from '@angular/material/sidenav';
import { BaseModule } from 'src/app/components/base/base.module';
import { LocalizeModule } from 'src/app/_Shared/modules/localize.module';
import { MatLegacyTableModule as MatTableModule } from '@angular/material/legacy-table';
import { TrainingProgramReviewWizardModule } from './trainingprogramreview-wizard/trainingprogramreview-wizard.module';

const routes: Routes = [
  {
    path: '',
    component: TrainingProgramReviewComponent,
    children: [
      {
        
        path: 'overview',
        canActivate: [AuthGuard, RouteGuard],
        loadChildren: () =>
          import('./training-program-review-overview/training-program-review-overview.module').then(
            (m) => m.TrainingProgramReviewOverviewModule
          ),
      },
      {
        path: 'create',
        canActivate: [AuthGuard, RouteGuard],
        loadChildren: () =>
          import('./trainingprogramreview-wizard/trainingprogramreview-wizard.module').then(
            (m) => m.TrainingProgramReviewWizardModule
          ),
      },
      {
        path: 'create/:id',
        canActivate: [AuthGuard, RouteGuard],
        loadChildren: () =>
          import('./trainingprogramreview-wizard/trainingprogramreview-wizard.module').then(
            (m) => m.TrainingProgramReviewWizardModule
          ),
      }
    ],
  },
];

@NgModule({
  declarations: [
    TrainingProgramReviewComponent,
  ],
  imports: [
    CommonModule,
    MatCardModule,
    LocalizeModule,
    BaseModule,
    RouterModule.forChild(routes),
    CommonModule,
    MatSidenavModule,
    LayoutModule,
    MatTableModule,
    TrainingProgramReviewWizardModule
  ]
})
export class TrainingProgramReviewModule { }

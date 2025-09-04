import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DesignAndDevelopmentComponent } from './design-and-development.component';
import { RouterModule, Routes } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';
import { LocalizeModule } from 'src/app/_Shared/modules/localize.module';
import { AuthGuard } from 'src/app/_Guards/auth.guard';
import { RouteGuard } from 'src/app/_Guards/route.guard';

const routes: Routes = [
  {
    path: '',
    component: DesignAndDevelopmentComponent,
    children: [
      {
        path: '',
        redirectTo: 'trainingmap',
        pathMatch: 'full',
      },
      {
        path: 'trainingmap',
        canActivate: [AuthGuard, RouteGuard],
        loadChildren: () =>
          import('./training-map/training-map.module').then(
            (m) => m.TrainingMapModule
          ),
      },
      {
        path: 'ila',
        canActivate: [AuthGuard, RouteGuard],
        loadChildren: () =>
          import('./providers-and-ila/providers-and-ila.module').then(
            (m) => m.ProvidersAndIlaModule
          ),
      },
      {
        path: 'trainingprogram',
        canActivate: [AuthGuard, RouteGuard],
        loadChildren: () =>
          import('./training-program/training-program.module').then(
            (m) => m.TrainingProgramModule
          ),
      },
      {
        path: 'tests',
        canActivate: [AuthGuard, RouteGuard],
        loadChildren: () =>
          import('./tests/tests.module').then((m) => m.TestsModule),
      },
      {
        path: 'tests/questions',
        canActivate: [AuthGuard, RouteGuard],
        loadChildren: () =>
          import('./tests/test-question-bank/test-question-bank.module').then(
            (m) => m.TestQuestionBankModule
          ),
      },
      {
        path: 'edit',
        canActivate: [AuthGuard, RouteGuard],
        loadChildren: () =>
          import(`./bulk-edit-design/bulk-edit-design.module`).then(
            (m) => m.BulkEditDesignModule
          ),
      },
      {
        path: 'edit/tests',
        canActivate: [AuthGuard, RouteGuard],
        loadChildren: () =>
          import('./tests/test-edit-or-update/test-edit-or-update.module').then(
            (m) => m.TestEditOrUpdateModule
          ),
      },
      {
        path: 'copy/tests',
        canActivate: [AuthGuard, RouteGuard],
        loadChildren: () =>
          import('./tests/test-edit-or-update/test-edit-or-update.module').then(
            (m) => m.TestEditOrUpdateModule
          ),
      },
      {
        path: 'simulatorscenarios',
        canActivate: [AuthGuard, RouteGuard],
        loadChildren: () =>
          import('./simulator-scenarios/simulator-scenarios.module').then(
            (m) => m.SimulatorScenariosModule
          ),
      },
    ],
  },
];

@NgModule({
  declarations: [DesignAndDevelopmentComponent],
  imports: [
    CommonModule,
    HttpClientModule,
    RouterModule.forChild(routes),
    LocalizeModule,
  ],
})
export class DesignAndDevelopmentModule {}

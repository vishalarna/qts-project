import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from 'src/app/_Guards/auth.guard';
import { RouteGuard } from 'src/app/_Guards/route.guard';
import { AnalysisComponent } from './analysis.component';

const routes: Routes = [
  {
    path: '',
    component: AnalysisComponent,
    children: [
      {
        path: 'jta',
        canActivate: [AuthGuard, RouteGuard],
        loadChildren: () => import('./jta/jta.module').then((m) => m.JtaModule),
      },
      {
        path: 'dif-survey',
        canActivate: [AuthGuard, RouteGuard],
        loadChildren: () => import('./dif-surveys/dif-surveys.module').then((m) => m.DifSurveysModule),
      },
    ],
  },
];

@NgModule({ imports: [RouterModule.forChild(routes)], exports: [RouterModule] })
export class AnalysisRoutingModule {}

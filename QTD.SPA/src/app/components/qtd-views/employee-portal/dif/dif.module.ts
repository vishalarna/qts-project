import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from 'src/app/_Guards/auth.guard';
import { RouteGuard } from 'src/app/_Guards/route.guard';
import { DifComponent } from './dif.component';


const routes: Routes = [
  {
    path: '',
    component: DifComponent,
    children: [
      {
        path: 'overview',
        canActivate: [AuthGuard, RouteGuard],
        loadChildren: () =>
          import('./dif-overview/dif-overview.module').then(
            (m) => m.DifOverviewModule
          ),
      },
      {
        path:':id/view-response',
        canActivate: [AuthGuard, RouteGuard],
        loadChildren: () =>
          import(`./dif-overview/view-response/view-response.module`).then(
            (m) => m.ViewResponseModule
          )
      },
      {
        path:':id/dif-survey-page',
        canActivate: [AuthGuard, RouteGuard],
        loadChildren: () =>
          import(`./dif-overview/dif-survey-page/dif-survey-page.module`).then(
            (m) => m.DifSurveyPageModule
          )
      }
    ],
  },
];
@NgModule({
  declarations: [
    DifComponent
  ],
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
  ]
})
export class DifModule { }

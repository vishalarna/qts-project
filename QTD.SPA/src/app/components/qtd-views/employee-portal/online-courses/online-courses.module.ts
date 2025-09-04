import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { OnlineCoursesComponent } from './online-courses.component';
import { RouterModule, Routes } from '@angular/router';
import { RouteGuard } from 'src/app/_Guards/route.guard';
import { AuthGuard } from 'src/app/_Guards/auth.guard';

const routes: Routes = [
  {
    path: '',
    component: OnlineCoursesComponent,
    children: [
      {

        path: 'overview',
        canActivate: [AuthGuard, RouteGuard],
        loadChildren: () =>
          import('./online-courses-overview/online-courses-overview.module').then(
            (m) => m.OnlineCoursesOverviewModule
          ),
      },
      // {

      //   path: 'start-review',
      //   canActivate: [AuthGuard, RouteGuard],
      //   loadChildren: () =>
      //     import('./fly-panel-start-procedure-review/fly-panel-start-procedure-review.module').then(
      //       (m) => m.FlyPanelStartProcedureReviewModule
      //     ),
      // },
      
      {

        path: '',
        redirectTo:'overview',
        pathMatch:'full'
      },

    ],
  },
];


@NgModule({
  declarations: [
    OnlineCoursesComponent
  ],
  imports: [
    CommonModule,
    RouterModule.forChild(routes)
  ]
})
export class OnlineCoursesModule { }

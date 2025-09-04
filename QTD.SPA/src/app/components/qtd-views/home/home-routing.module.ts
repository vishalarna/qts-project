import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home.component';
import { IndexComponent } from './index/index.component';
import { RouteGuard } from 'src/app/_Guards/route.guard';
import { AuthGuard } from 'src/app/_Guards/auth.guard';
import { PublicRequestGuard } from 'src/app/_Guards/public-request.guard';

const routes: Routes = [
  {
    path: '',
    component: HomeComponent,
    children: [
      {
        path: '',
        redirectTo: 'index',
        pathMatch: 'full',
      },
      {
        path: 'index',
        component: IndexComponent,
      },

      {
        path: 'dashboard',
        canActivate:[AuthGuard,RouteGuard],
        loadChildren: () =>
      import('./admin-home-dashboard/admin-home-dashboard.module').then(
        (m) => m.AdminHomeDashboardModule
      ),
      },
      {
        path: 'instance-selection',
        loadChildren: () =>
          import('../instance-selection/instance-selection.module').then(
            (m) => m.InstanceSelectionModule
          ),
      },
      {
        path: 'public-request',
        canActivate:[AuthGuard,RouteGuard,PublicRequestGuard],
        loadChildren: () =>
          import('./public-requests/public-requests.module').then(
            (m) => m.PublicRequestsComponentModule
          ),
      },
    ],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class HomeRoutingModule {}

import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  {
    path: '',
    children: [
      {
        path: '',
        canActivate: [],
        loadChildren: () =>
          import(
            './public-portal-dashboard/public-portal-dashboard.module'
          ).then((m) => m.PublicPortalDashboardComponentModule),
      },
      
    ],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class PublicPortalRoutingModule {}

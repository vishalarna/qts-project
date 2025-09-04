import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PositionsComponent } from './positions.component';
import { PositionsNavbarComponent } from './positions-navbar/positions-navbar.component';
import { AuthGuard } from 'src/app/_Guards/auth.guard';
import { RouteGuard } from 'src/app/_Guards/route.guard';
import { RouterModule, Routes } from '@angular/router';
import { MatSidenavModule } from '@angular/material/sidenav';
import { BaseModule } from 'src/app/components/base/base.module';
import { HttpClientModule } from '@angular/common/http';
import { LayoutModule } from '../../layout/layout.module';
import { FormsModule } from '@angular/forms';
import { MatLegacyMenuModule as MatMenuModule } from '@angular/material/legacy-menu';
import { MatTreeModule } from '@angular/material/tree';
import { FlyPanelAddPositionModule } from './fly-panel-add-position/fly-panel-add-position.module';
const routes: Routes = [
  {
    path: '',
    component: PositionsComponent,
    children: 
    [
      {
        path: '',
        redirectTo: 'overview',
        pathMatch: 'full',
      },
      {
        path: 'overview',
        canActivate: [AuthGuard, RouteGuard],
        loadChildren: () =>
          import('./positions-overview/positions-overview.module').then(
            (m) => m.PositionsOverviewModule
          ),
      },
      {
        path: 'details',
        canActivate: [AuthGuard, RouteGuard],
        loadChildren: () =>
          import('./position-details/position-details.module').then(
            (m) => m.PositionDetailsModule
          ),
      },
      
    ]
  }
]


@NgModule({
  declarations: [
    PositionsComponent,
    PositionsNavbarComponent,
  ],
  imports: [
    BaseModule,
    CommonModule,
    RouterModule.forChild(routes),
    MatSidenavModule,
    BaseModule,
    HttpClientModule,
    LayoutModule,
    FormsModule,
    MatMenuModule,
    FlyPanelAddPositionModule,
    MatTreeModule
  ]
})
export class PositionsModule { }

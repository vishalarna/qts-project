import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LocationsComponent } from './locations.component';
import { LocationsNavbarComponent } from './locations-navbar/locations-navbar.component';
import { MatLegacyMenuModule as MatMenuModule } from '@angular/material/legacy-menu';
import { MatLegacyTabsModule as MatTabsModule } from '@angular/material/legacy-tabs';
import { Routes, RouterModule } from '@angular/router';
import { BaseModule } from 'src/app/components/base/base.module';
import { AuthGuard } from 'src/app/_Guards/auth.guard';
import { RouteGuard } from 'src/app/_Guards/route.guard';
import { FlyPanelAddLocationModule } from './fly-panel-add-location/fly-panel-add-location.module';
import { FormsModule } from '@angular/forms';
import { FlyPanelAddLocationComponent } from './fly-panel-add-location/fly-panel-add-location.component';
import { FlyPanelAddLocationCategoryModule } from './fly-panel-add-location-category/fly-panel-add-location-category.module';
import { LayoutModule } from '../../layout/layout.module';
import { MatSidenavModule } from '@angular/material/sidenav';
import { FlyPanelViewLocationHistoryModule } from './fly-panel-view-location-history/fly-panel-view-location-history.module';
import { MatTreeModule } from '@angular/material/tree';


const routes: Routes = [
  {
    path: '',
    component: LocationsComponent,
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
          import('./locations-overview/locations-overview.module').then(
            (m) => m.LocationsOverviewModule
          ),
      },
      {
        path: 'details',
        canActivate: [AuthGuard, RouteGuard],
        loadChildren: () =>
          import('./locations-details/locations-details.module').then(
            (m) => m.LocationsDetailsModule
          ),
      },
      {
        path: 'category-details',
        canActivate: [AuthGuard, RouteGuard],
        loadChildren: () =>
          import(
            './locations-category-details/location-category-details.module'
          ).then((m) => m.LocationCategorydetailsComponent),
      },
    ]
  }
]

@NgModule({
  declarations: [LocationsComponent,  LocationsNavbarComponent ],
  imports: [
    CommonModule,
    BaseModule,
    MatMenuModule,
    MatTabsModule,
     MatSidenavModule,
    FormsModule,
    LayoutModule,
    RouterModule.forChild(routes),
    FlyPanelAddLocationModule,
    FormsModule,
    FlyPanelAddLocationCategoryModule,
    FlyPanelViewLocationHistoryModule,
    MatTreeModule
    
  ],
})
export class LocationsModule { } 

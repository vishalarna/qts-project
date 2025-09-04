import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LocationsOverviewComponent } from './locations-overview.component';
import { RouterModule, Routes } from '@angular/router';
import { MatLegacyTableModule as MatTableModule } from '@angular/material/legacy-table';
import { MatLegacyPaginatorModule as MatPaginatorModule } from '@angular/material/legacy-paginator';
import { MatSortModule } from '@angular/material/sort';
import { BaseModule } from 'src/app/components/base/base.module';
import { FlyPanelViewLocationHistoryModule } from '../fly-panel-view-location-history/fly-panel-view-location-history.module';
import { FlyPanelLocationsListModule } from '../fly-panel-locations-list/fly-panel-locations-list.module';
const routes: Routes = [
  {
    path: '',
    component: LocationsOverviewComponent,
  }
 ]



@NgModule({
  declarations: [
    LocationsOverviewComponent
  ],
  imports: [
    CommonModule,
    MatTableModule,
    MatPaginatorModule,
    MatSortModule,
    BaseModule,
    RouterModule.forChild(routes),
    FlyPanelViewLocationHistoryModule,
    FlyPanelLocationsListModule

  ],
  exports: [LocationsOverviewComponent],
})
export class LocationsOverviewModule { }

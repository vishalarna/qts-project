import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { BaseModule } from 'src/app/components/base/base.module';
import { MatLegacyMenuModule as MatMenuModule } from '@angular/material/legacy-menu';
import { MatLegacyTabsModule as MatTabsModule } from '@angular/material/legacy-tabs';
import {MatLegacyTooltipModule as MatTooltipModule} from '@angular/material/legacy-tooltip';

import { FlyPanelAddProcedureModule } from '../../procedures/fly-panel-add-procedure/fly-panel-add-procedure.module';
import { LocationCategoryDetailsComponent } from './location-category-details.component';
import { FlyPanelAddLocationCategoryModule } from '../fly-panel-add-location-category/fly-panel-add-location-category.module';
import { FlyPanelAddLocationModule } from '../fly-panel-add-location/fly-panel-add-location.module';

const routes: Routes = [
  {
    path: ':id',
    component: LocationCategoryDetailsComponent,
  }
 ]



@NgModule({
  declarations: [
    LocationCategoryDetailsComponent
  ],
  imports: [
    CommonModule,
    BaseModule,
    RouterModule.forChild(routes),
    MatMenuModule,
    MatTabsModule,
    MatTooltipModule,

    FlyPanelAddProcedureModule,
    FlyPanelAddLocationCategoryModule,


  ]
})
export class LocationCategorydetailsComponent { }

import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LocationDetailsComponent } from './locations-details.component';
import { RouterModule, Routes } from '@angular/router';
import { BaseModule } from 'src/app/components/base/base.module';
import { MatLegacyMenuModule as MatMenuModule } from '@angular/material/legacy-menu';
import { MatLegacyTabsModule as MatTabsModule } from '@angular/material/legacy-tabs';

import { ProcedureEnablingObjectiveModule } from '../../procedures/procedure-details/procedure-enabling-objective/procedure-enabling-objective.module';
import { ProcedureIlaModule } from '../../procedures/procedure-details/procedure-ila/procedure-ila.module';
import { ProcedureRegRequirementModule } from '../../procedures/procedure-details/procedure-reg-requirement/procedure-reg-requirement.module';
import { ProcedureSafetyHazardModule } from '../../procedures/procedure-details/procedure-safety-hazard/procedure-safety-hazard.module';
import { ProcedureTaskModule } from '../../procedures/procedure-details/procedure-task/procedure-task.module';
import { FlyPanelAddLocationModule } from '../fly-panel-add-location/fly-panel-add-location.module';
import { FlyPanelAddLocationCategoryModule } from '../fly-panel-add-location-category/fly-panel-add-location-category.module';
import {MatLegacyTooltipModule as MatTooltipModule} from '@angular/material/legacy-tooltip';


const routes: Routes = [
  {
    path: ':id',
    component: LocationDetailsComponent,
  }
 ]


@NgModule({
  declarations: [
    LocationDetailsComponent
  ],
  imports: [
    CommonModule,
    BaseModule,
    RouterModule.forChild(routes),
    MatMenuModule,
    MatTabsModule,
    ProcedureTaskModule,
    ProcedureRegRequirementModule,
    ProcedureIlaModule,
    ProcedureEnablingObjectiveModule,
    ProcedureSafetyHazardModule,
    FlyPanelAddLocationModule,
    FlyPanelAddLocationCategoryModule,
    RouterModule.forChild(routes),
    MatTooltipModule
  ]
  
})
export class LocationsDetailsModule { }

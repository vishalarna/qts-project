import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RRDetailsComponent } from './rr-details.component';
import { MatLegacyMenuModule as MatMenuModule } from '@angular/material/legacy-menu';
import { MatLegacyTabsModule as MatTabsModule } from '@angular/material/legacy-tabs';
import { Routes, RouterModule } from '@angular/router';
import { BaseModule } from 'src/app/components/base/base.module';
import { RRTaskModule } from './rr-task/rr-task.module';
import { RRProcedureModule } from './rr-procedure/rr-procedure.module';
import { RRIlaModule } from './rr-ila/rr-ila.module';
import { RREnablingObjectiveModule } from './rr-enabling-objective/rr-enabling-objective.module';
import { RRSafetyHazardModule } from './rr-safety-hazard/rr-safety-hazard.module';
import { FlyPanelAddRrModule } from '../fly-panel-add-rr/fly-panel-add-rr.module';

const routes: Routes = [
  {
    path: ':id',
    component: RRDetailsComponent,
  },
];

@NgModule({
  declarations: [RRDetailsComponent],
  imports: [
    CommonModule,
    BaseModule,
    MatMenuModule,
    MatTabsModule,
    RouterModule.forChild(routes),
    RRTaskModule,
    RRProcedureModule,
    RRIlaModule,
    RREnablingObjectiveModule,
    RRSafetyHazardModule,
    FlyPanelAddRrModule,
  ],
})
export class RRDetailsModule {}

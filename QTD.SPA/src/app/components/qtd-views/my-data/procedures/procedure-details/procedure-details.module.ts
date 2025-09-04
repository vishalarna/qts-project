import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ProcedureDetailsComponent } from './procedure-details.component';
import { RouterModule, Routes } from '@angular/router';
import { BaseModule } from 'src/app/components/base/base.module';
import { MatLegacyMenuModule as MatMenuModule } from '@angular/material/legacy-menu';
import { MatLegacyTabsModule as MatTabsModule } from '@angular/material/legacy-tabs';
import { ProcedureTaskModule } from './procedure-task/procedure-task.module';
import { ProcedureRegRequirementModule } from './procedure-reg-requirement/procedure-reg-requirement.module';
import { ProcedureIlaModule } from './procedure-ila/procedure-ila.module';
import { ProcedureEnablingObjectiveModule } from './procedure-enabling-objective/procedure-enabling-objective.module';
import { ProcedureSafetyHazardComponent } from './procedure-safety-hazard/procedure-safety-hazard.component';
import { ProcedureSafetyHazardModule } from './procedure-safety-hazard/procedure-safety-hazard.module';
import { FlyPanelAddProcedureModule } from '../fly-panel-add-procedure/fly-panel-add-procedure.module';

const routes: Routes = [
  {
    path: ':id',
    component: ProcedureDetailsComponent,
  },
];

@NgModule({
  declarations: [ProcedureDetailsComponent],
  imports: [
    CommonModule,
    BaseModule,
    MatMenuModule,
    MatTabsModule,
    RouterModule.forChild(routes),
    ProcedureTaskModule,
    ProcedureRegRequirementModule,
    ProcedureIlaModule,
    ProcedureEnablingObjectiveModule,
    ProcedureSafetyHazardModule,
    FlyPanelAddProcedureModule,
  ],
})
export class ProcedureDetailsModule {}

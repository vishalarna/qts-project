import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { InstructorDetailsComponent } from './instructor-details.component';
import { RouterModule, Routes } from '@angular/router';
import { BaseModule } from 'src/app/components/base/base.module';
import { MatLegacyMenuModule as MatMenuModule } from '@angular/material/legacy-menu';
import { MatLegacyTabsModule as MatTabsModule } from '@angular/material/legacy-tabs';
import { ProcedureTaskModule } from '../../procedures/procedure-details/procedure-task/procedure-task.module';
import { ProcedureRegRequirementModule } from '../../procedures/procedure-details/procedure-reg-requirement/procedure-reg-requirement.module';
import { ProcedureIlaModule } from '../../procedures/procedure-details/procedure-ila/procedure-ila.module';
import { ProcedureEnablingObjectiveModule } from '../../procedures/procedure-details/procedure-enabling-objective/procedure-enabling-objective.module';
import { ProcedureSafetyHazardModule } from '../../procedures/procedure-details/procedure-safety-hazard/procedure-safety-hazard.module';
import { FlyPanelAddProcedureModule } from '../../procedures/fly-panel-add-procedure/fly-panel-add-procedure.module';
import { FlyPanelAddInstructorModule } from '../fly-panel-add-instructor/fly-panel-add-instructor.module';
import {MatLegacyTooltipModule as MatTooltipModule} from '@angular/material/legacy-tooltip';

const routes: Routes = [
  {
    path: ':id',
    component: InstructorDetailsComponent,
  }
 ]


@NgModule({
  declarations: [
    InstructorDetailsComponent
  ],
  imports: [
    CommonModule,
    BaseModule,
    MatMenuModule,
    MatTabsModule,
    ProcedureTaskModule,
    ProcedureRegRequirementModule,
    ProcedureIlaModule,
    ProcedureEnablingObjectiveModule,
    ProcedureSafetyHazardModule,
    FlyPanelAddInstructorModule,
    MatTooltipModule,
    RouterModule.forChild(routes),
  ]
})
export class InstructorDetailsModule { }

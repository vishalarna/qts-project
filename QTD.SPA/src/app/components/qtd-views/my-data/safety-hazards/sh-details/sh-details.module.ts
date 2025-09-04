import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ShDetailsComponent } from './sh-details.component';
import { MatLegacyMenuModule as MatMenuModule } from '@angular/material/legacy-menu';
import { MatLegacyTabsModule as MatTabsModule } from '@angular/material/legacy-tabs';
import { Routes, RouterModule } from '@angular/router';
import { BaseModule } from 'src/app/components/base/base.module';
import { ShEnablingObjectiveModule } from './sh-enabling-objective/sh-enabling-objective.module';
import { ShIlaModule } from './sh-ila/sh-ila.module';
import { ShProcedureModule } from './sh-procedure/sh-procedure.module';
import { ShTaskModule } from './sh-task/sh-task.module';
import { ShRrModule } from './sh-rr/sh-rr.module';
import { ShDetailTabModule } from './sh-detail-tab/sh-detail-tab.module';
import { FlypanelAddSafetyHazardsModule } from '../flypanel-add-safety-hazards/flypanel-add-safety-hazards.module';

const routes: Routes = [
  {
    path: ':id',
    component: ShDetailsComponent,
  },
];

@NgModule({
  declarations: [ShDetailsComponent],
  imports: [
    CommonModule,
    BaseModule,
    MatMenuModule,
    MatTabsModule,
    RouterModule.forChild(routes),
    ShTaskModule,
    ShProcedureModule,
    ShIlaModule,
    ShEnablingObjectiveModule,
    ShRrModule,
    ShDetailTabModule,
    FlypanelAddSafetyHazardsModule,
  ],
})
export class ShDetailsModule {}

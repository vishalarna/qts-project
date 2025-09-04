import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ProcedureSafetyHazardComponent } from './procedure-safety-hazard.component';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { MatLegacyPaginatorModule as MatPaginatorModule } from '@angular/material/legacy-paginator';
import { MatSortModule } from '@angular/material/sort';
import { MatLegacyTableModule as MatTableModule } from '@angular/material/legacy-table';
import { MatLegacyTooltipModule as MatTooltipModule } from '@angular/material/legacy-tooltip';
import { BaseModule } from 'src/app/components/base/base.module';
import { FlyPanelLinkedProceduresModule } from '../../fly-panel-linked-procedures/fly-panel-linked-procedures.module';
import { FlyPanelProcedureEosLinkModule } from '../../fly-panel-procedure-eos-link/fly-panel-procedure-eos-link.module';
import { FlyPanelProcedureTasksLinkModule } from '../../fly-panel-procedure-tasks-link/fly-panel-procedure-tasks-link.module';
import { FlyPanelProcedureSafetyHazardsLinkModule } from '../../fly-panel-procedure-safety-hazards-link/fly-panel-procedure-safety-hazards-link.module';



@NgModule({
  declarations: [ProcedureSafetyHazardComponent],
  imports: [
    CommonModule,
    BaseModule,
    MatTableModule,
    MatSortModule,
    MatPaginatorModule,
    MatCheckboxModule,
    MatTooltipModule,
    FlyPanelProcedureSafetyHazardsLinkModule,
    FlyPanelProcedureTasksLinkModule,
    FlyPanelLinkedProceduresModule,
  ],
  exports: [ProcedureSafetyHazardComponent]
})
export class ProcedureSafetyHazardModule { }

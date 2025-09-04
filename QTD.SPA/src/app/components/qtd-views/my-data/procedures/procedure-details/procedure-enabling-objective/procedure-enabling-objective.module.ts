import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ProcedureEnablingObjectiveComponent } from './procedure-enabling-objective.component';
import { BaseModule } from 'src/app/components/base/base.module';
import { MatLegacyTableModule as MatTableModule } from '@angular/material/legacy-table';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { MatLegacyPaginatorModule as MatPaginatorModule } from '@angular/material/legacy-paginator';
import { MatSortModule } from '@angular/material/sort';
import { MatLegacyTooltipModule as MatTooltipModule } from '@angular/material/legacy-tooltip';
import { FlyPanelProcedureEosLinkModule } from '../../fly-panel-procedure-eos-link/fly-panel-procedure-eos-link.module';
import { FlyPanelProcedureTasksLinkModule } from '../../fly-panel-procedure-tasks-link/fly-panel-procedure-tasks-link.module';
import { FlyPanelLinkedProceduresModule } from '../../fly-panel-linked-procedures/fly-panel-linked-procedures.module';



@NgModule({
  declarations: [ProcedureEnablingObjectiveComponent],
  imports: [
    CommonModule,
    BaseModule,
    MatTableModule,
    MatSortModule,
    MatPaginatorModule,
    MatCheckboxModule,
    MatTooltipModule,
    FlyPanelProcedureEosLinkModule,
    FlyPanelProcedureTasksLinkModule,
    FlyPanelLinkedProceduresModule,
  ],
  exports: [ProcedureEnablingObjectiveComponent]
})
export class ProcedureEnablingObjectiveModule { }

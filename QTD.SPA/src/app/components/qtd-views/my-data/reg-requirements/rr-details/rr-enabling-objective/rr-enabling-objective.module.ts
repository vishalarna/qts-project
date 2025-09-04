import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RREnablingObjectiveComponent } from './rr-enabling-objective.component';
import { FlyPanelRREOsLinkModule } from '../../fly-panel-rr-eos-link/fly-panel-rr-eos-link.module';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { MatLegacyPaginatorModule as MatPaginatorModule } from '@angular/material/legacy-paginator';
import { MatSortModule } from '@angular/material/sort';
import { MatLegacyTableModule as MatTableModule } from '@angular/material/legacy-table';
import { MatLegacyTooltipModule as MatTooltipModule } from '@angular/material/legacy-tooltip';
import { BaseModule } from 'src/app/components/base/base.module';
import { FlyPanelLinkedRRModule } from '../../fly-panel-linked-rr/fly-panel-linked-rr.module';
import { FlyPanelRRTasksLinkModule } from '../../fly-panel-rr-tasks-link/fly-panel-rr-tasks-link.module';

@NgModule({
  declarations: [RREnablingObjectiveComponent],
  imports: [
    CommonModule,
    BaseModule,
    MatTableModule,
    MatSortModule,
    MatPaginatorModule,
    MatCheckboxModule,
    MatTooltipModule,
    FlyPanelRREOsLinkModule,
    FlyPanelRRTasksLinkModule,
    FlyPanelLinkedRRModule,
  ],
  exports: [RREnablingObjectiveComponent],
})
export class RREnablingObjectiveModule {}

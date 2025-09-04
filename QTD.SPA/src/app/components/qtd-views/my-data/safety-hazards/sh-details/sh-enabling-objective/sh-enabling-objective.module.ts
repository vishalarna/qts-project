import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ShEnablingObjectiveComponent } from './sh-enabling-objective.component';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { MatLegacyPaginatorModule as MatPaginatorModule } from '@angular/material/legacy-paginator';
import { MatSortModule } from '@angular/material/sort';
import { MatLegacyTableModule as MatTableModule } from '@angular/material/legacy-table';
import { MatLegacyTooltipModule as MatTooltipModule } from '@angular/material/legacy-tooltip';
import { BaseModule } from 'src/app/components/base/base.module';
import { FlyPanelLinkedShModule } from '../../fly-panel-linked-sh/fly-panel-linked-sh.module';
import { FlyPanelShEosLinkModule } from '../../fly-panel-sh-eos-link/fly-panel-sh-eos-link.module';
import { FlyPanelShTasksLinkModule } from '../../fly-panel-sh-tasks-link/fly-panel-sh-tasks-link.module';

@NgModule({
  declarations: [ShEnablingObjectiveComponent],
  imports: [  CommonModule,
    BaseModule,
    MatTableModule,
    MatSortModule,
    MatPaginatorModule,
    MatCheckboxModule,
    MatTooltipModule,
    FlyPanelShEosLinkModule,
    FlyPanelShTasksLinkModule,
    FlyPanelLinkedShModule,],
  exports: [ShEnablingObjectiveComponent],
})
export class ShEnablingObjectiveModule {}

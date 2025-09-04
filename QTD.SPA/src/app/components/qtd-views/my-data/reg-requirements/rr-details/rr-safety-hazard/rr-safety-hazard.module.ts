import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RRSafetyHazardComponent } from './rr-safety-hazard.component';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { MatLegacyPaginatorModule as MatPaginatorModule } from '@angular/material/legacy-paginator';
import { MatSortModule } from '@angular/material/sort';
import { MatLegacyTableModule as MatTableModule } from '@angular/material/legacy-table';
import { MatLegacyTooltipModule as MatTooltipModule } from '@angular/material/legacy-tooltip';
import { BaseModule } from 'src/app/components/base/base.module';
import { FlyPanelLinkedRRModule } from '../../fly-panel-linked-rr/fly-panel-linked-rr.module';
import { FlyPanelRRTasksLinkModule } from '../../fly-panel-rr-tasks-link/fly-panel-rr-tasks-link.module';
import { FlyPanelRRSafetyHazardLinkModule } from '../../fly-panel-rr-safety-hazard-link/fly-panel-rr-safety-hazard-link.module';

@NgModule({
  declarations: [RRSafetyHazardComponent],
  imports: [
    CommonModule,
    BaseModule,
    MatTableModule,
    MatSortModule,
    MatPaginatorModule,
    MatCheckboxModule,
    MatTooltipModule,
    FlyPanelRRSafetyHazardLinkModule,
    FlyPanelRRTasksLinkModule,
    FlyPanelLinkedRRModule,
  ],
  exports: [RRSafetyHazardComponent],
})
export class RRSafetyHazardModule {}

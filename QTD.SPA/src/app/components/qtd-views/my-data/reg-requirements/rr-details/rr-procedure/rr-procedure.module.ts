import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RRProcedureComponent } from './rr-procedure.component';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { MatLegacyPaginatorModule as MatPaginatorModule } from '@angular/material/legacy-paginator';
import { MatSortModule } from '@angular/material/sort';
import { MatLegacyTableModule as MatTableModule } from '@angular/material/legacy-table';
import { MatLegacyTooltipModule as MatTooltipModule } from '@angular/material/legacy-tooltip';
import { BaseModule } from 'src/app/components/base/base.module';
import { FlyPanelLinkedRRModule } from '../../fly-panel-linked-rr/fly-panel-linked-rr.module';
import { FlyPanelRRProceduresLinkModule } from '../../fly-panel-rr-procedures-link/fly-panel-rr-procedures-link.module';

@NgModule({
  declarations: [RRProcedureComponent],
  imports: [
    CommonModule,
    BaseModule,
    MatTableModule,
    MatSortModule,
    MatPaginatorModule,
    MatCheckboxModule,
    MatTooltipModule,
    FlyPanelLinkedRRModule,
    FlyPanelRRProceduresLinkModule,
  ],
  exports: [RRProcedureComponent],
})
export class RRProcedureModule {}

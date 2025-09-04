import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ProcedureTaskComponent } from './procedure-task.component';
import { BaseModule } from 'src/app/components/base/base.module';
import { MatLegacyTableModule as MatTableModule } from '@angular/material/legacy-table';
import { MatSortModule } from '@angular/material/sort';
import { MatLegacyPaginatorModule as MatPaginatorModule } from '@angular/material/legacy-paginator';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { MatLegacyTooltipModule as MatTooltipModule } from '@angular/material/legacy-tooltip';
import { FlyPanelProcedureTasksLinkModule } from '../../fly-panel-procedure-tasks-link/fly-panel-procedure-tasks-link.module';
import { FlyPanelLinkedProceduresModule } from '../../fly-panel-linked-procedures/fly-panel-linked-procedures.module';

@NgModule({
  declarations: [ProcedureTaskComponent],
  imports: [
    CommonModule,
    BaseModule,
    MatTableModule,
    MatSortModule,
    MatPaginatorModule,
    MatCheckboxModule,
    MatTooltipModule,
    FlyPanelProcedureTasksLinkModule,
    FlyPanelLinkedProceduresModule,
  ],
  exports: [ProcedureTaskComponent],
})
export class ProcedureTaskModule {}

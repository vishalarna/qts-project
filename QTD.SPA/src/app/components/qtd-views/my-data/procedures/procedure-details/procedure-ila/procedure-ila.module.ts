import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ProcedureIlaComponent } from './procedure-ila.component';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { MatLegacyPaginatorModule as MatPaginatorModule } from '@angular/material/legacy-paginator';
import { MatSortModule } from '@angular/material/sort';
import { MatLegacyTableModule as MatTableModule } from '@angular/material/legacy-table';
import { BaseModule } from 'src/app/components/base/base.module';
import { MatLegacyTooltipModule as MatTooltipModule } from '@angular/material/legacy-tooltip';
import { FlyPanelLinkedProceduresModule } from '../../fly-panel-linked-procedures/fly-panel-linked-procedures.module';
import { FlyPanelProcedureIlasLinkModule } from '../../fly-panel-procedure-ilas-link/fly-panel-procedure-ilas-link.module';

@NgModule({
  declarations: [ProcedureIlaComponent],
  imports: [
    CommonModule,
    BaseModule,
    MatTableModule,
    MatSortModule,
    MatPaginatorModule,
    MatCheckboxModule,
    MatTooltipModule,
    FlyPanelLinkedProceduresModule,
    FlyPanelProcedureIlasLinkModule,
  ],
  exports: [ProcedureIlaComponent],
})
export class ProcedureIlaModule {}

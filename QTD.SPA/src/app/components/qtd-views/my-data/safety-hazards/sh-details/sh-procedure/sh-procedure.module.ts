import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ShProcedureComponent } from './sh-procedure.component';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { MatLegacyPaginatorModule as MatPaginatorModule } from '@angular/material/legacy-paginator';
import { MatSortModule } from '@angular/material/sort';
import { MatLegacyTableModule as MatTableModule } from '@angular/material/legacy-table';
import { MatLegacyTooltipModule as MatTooltipModule } from '@angular/material/legacy-tooltip';
import { BaseModule } from 'src/app/components/base/base.module';
import { FlyPanelLinkedShModule } from '../../fly-panel-linked-sh/fly-panel-linked-sh.module';
import { FlyPanelShProceduresLinkModule } from '../../fly-panel-sh-procedures-link/fly-panel-sh-procedures-link.module';

@NgModule({
  declarations: [ShProcedureComponent],
  imports: [
    CommonModule,
    BaseModule,
    MatTableModule,
    MatSortModule,
    MatPaginatorModule,
    MatCheckboxModule,
    MatTooltipModule,
    FlyPanelLinkedShModule,
    FlyPanelShProceduresLinkModule,
  ],
  exports: [ShProcedureComponent],
})
export class ShProcedureModule {}

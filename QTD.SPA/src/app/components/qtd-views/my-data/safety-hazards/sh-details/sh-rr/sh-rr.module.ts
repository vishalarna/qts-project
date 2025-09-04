import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ShRrComponent } from './sh-rr.component';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { MatLegacyPaginatorModule as MatPaginatorModule } from '@angular/material/legacy-paginator';
import { MatSortModule } from '@angular/material/sort';
import { MatLegacyTableModule as MatTableModule } from '@angular/material/legacy-table';
import { MatLegacyTooltipModule as MatTooltipModule } from '@angular/material/legacy-tooltip';
import { BaseModule } from 'src/app/components/base/base.module';
import { FlyPanelLinkedShModule } from '../../fly-panel-linked-sh/fly-panel-linked-sh.module';
import { FlyPanelShRrLinkModule } from '../../fly-panel-sh-rr-link/fly-panel-sh-rr-link.module';

@NgModule({
  declarations: [ShRrComponent],
  imports: [
    CommonModule,
    BaseModule,
    MatTableModule,
    MatSortModule,
    MatPaginatorModule,
    MatCheckboxModule,
    MatTooltipModule,
    FlyPanelLinkedShModule,
    FlyPanelShRrLinkModule,
  ],
  exports: [ShRrComponent],
})
export class ShRrModule {}

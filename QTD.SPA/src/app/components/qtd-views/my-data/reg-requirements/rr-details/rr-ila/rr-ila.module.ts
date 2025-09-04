import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RRIlaComponent } from './rr-ila.component';
import { FlyPanelLinkedRRModule } from '../../fly-panel-linked-rr/fly-panel-linked-rr.module';
import { FlyPanelRRIlasLinkModule } from '../../fly-panel-rr-ilas-link/fly-panel-rr-ilas-link.module';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { MatLegacyPaginatorModule as MatPaginatorModule } from '@angular/material/legacy-paginator';
import { MatSortModule } from '@angular/material/sort';
import { MatLegacyTableModule as MatTableModule } from '@angular/material/legacy-table';
import { MatLegacyTooltipModule as MatTooltipModule } from '@angular/material/legacy-tooltip';
import { BaseModule } from 'src/app/components/base/base.module';

@NgModule({
  declarations: [RRIlaComponent],
  imports: [
    CommonModule,
    BaseModule,
    MatTableModule,
    MatSortModule,
    MatPaginatorModule,
    MatCheckboxModule,
    MatTooltipModule,
    FlyPanelLinkedRRModule,
    FlyPanelRRIlasLinkModule,
  ],
  exports: [RRIlaComponent],
})
export class RRIlaModule {}

import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { EoSafetyHazardsComponent } from './eo-safety-hazards.component';
import { BaseModule } from 'src/app/components/base/base.module';
import { MatLegacyTableModule as MatTableModule } from '@angular/material/legacy-table';
import { MatLegacyPaginatorModule as MatPaginatorModule } from '@angular/material/legacy-paginator';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { FlypanelEoSafetyHazardLinkModule } from '../../flypanel-eo-safety-hazard-link/flypanel-eo-safety-hazard-link.module';
import { FlypanelLinkedEosModule } from '../../flypanel-linked-eos/flypanel-linked-eos.module';
import { MatSortModule } from '@angular/material/sort';
import { MatLegacyTooltipModule as MatTooltipModule } from '@angular/material/legacy-tooltip';


@NgModule({
  declarations: [
    EoSafetyHazardsComponent
  ],
  imports: [
    CommonModule,
    BaseModule,
    MatTableModule,
    MatPaginatorModule,
    MatCheckboxModule,
    MatTooltipModule,
    FlypanelEoSafetyHazardLinkModule,
    FlypanelLinkedEosModule,
    MatSortModule
  ],
  exports : [EoSafetyHazardsComponent]
})
export class EoSafetyHazardsModule { }

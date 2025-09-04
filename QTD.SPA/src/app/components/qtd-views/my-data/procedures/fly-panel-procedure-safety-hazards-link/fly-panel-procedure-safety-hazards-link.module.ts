import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FlyPanelProcedureSafetyHazardsLinkComponent } from './fly-panel-procedure-safety-hazards-link.component';
import { FormsModule } from '@angular/forms';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { MatLegacyMenuModule as MatMenuModule } from '@angular/material/legacy-menu';
import { MatTreeModule } from '@angular/material/tree';
import { BaseModule } from 'src/app/components/base/base.module';
import { FlypanelAddSafetyHazardsModule } from '../../safety-hazards/flypanel-add-safety-hazards/flypanel-add-safety-hazards.module';



@NgModule({
  declarations: [FlyPanelProcedureSafetyHazardsLinkComponent],
  imports: [
    CommonModule,
    BaseModule,
    MatMenuModule,
    FormsModule,
    MatCheckboxModule,
    MatTreeModule,
    FlypanelAddSafetyHazardsModule
  ],
  exports: [FlyPanelProcedureSafetyHazardsLinkComponent]
})
export class FlyPanelProcedureSafetyHazardsLinkModule { }

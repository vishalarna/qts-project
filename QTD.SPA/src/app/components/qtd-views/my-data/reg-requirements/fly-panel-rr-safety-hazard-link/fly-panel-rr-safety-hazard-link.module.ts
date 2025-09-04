import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FlyPanelRRSafetyHazardLinkComponent } from './fly-panel-rr-safety-hazard-link.component';
import { FormsModule } from '@angular/forms';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { MatLegacyMenuModule as MatMenuModule } from '@angular/material/legacy-menu';
import { MatTreeModule } from '@angular/material/tree';
import { BaseModule } from 'src/app/components/base/base.module';
import { FlypanelAddSafetyHazardsModule } from '../../safety-hazards/flypanel-add-safety-hazards/flypanel-add-safety-hazards.module';

@NgModule({
  declarations: [FlyPanelRRSafetyHazardLinkComponent],
  imports: [
    CommonModule,
    BaseModule,
    MatMenuModule,
    FormsModule,
    MatCheckboxModule,
    MatTreeModule,
    FlypanelAddSafetyHazardsModule,
  ],
  exports: [FlyPanelRRSafetyHazardLinkComponent],
})
export class FlyPanelRRSafetyHazardLinkModule {}

import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FlypanelEoSafetyHazardLinkComponent } from './flypanel-eo-safety-hazard-link.component';
import { BaseModule } from 'src/app/components/base/base.module';
import { MatTreeModule } from '@angular/material/tree';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { MatLegacyTabsModule as MatTabsModule } from '@angular/material/legacy-tabs';
import { FlypanelAddSafetyHazardsModule } from '../../safety-hazards/flypanel-add-safety-hazards/flypanel-add-safety-hazards.module';
import { FormsModule } from '@angular/forms';
import { MatLegacyMenuModule as MatMenuModule } from '@angular/material/legacy-menu';



@NgModule({
  declarations: [
    FlypanelEoSafetyHazardLinkComponent
  ],
  imports: [
    CommonModule,
    BaseModule,
    MatTreeModule,
    MatCheckboxModule,
    MatTabsModule,
    FlypanelAddSafetyHazardsModule,
    FormsModule,
    MatMenuModule,
  ],
  exports : [FlypanelEoSafetyHazardLinkComponent]
})
export class FlypanelEoSafetyHazardLinkModule { }

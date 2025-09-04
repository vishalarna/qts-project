import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FlyPanelToolShLinkComponent } from './fly-panel-tool-sh-link.component';
import { BaseModule } from 'src/app/components/base/base.module';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { MatTreeModule } from '@angular/material/tree';
import { FormsModule } from '@angular/forms';
import { FlypanelAddSafetyHazardsModule } from '../../safety-hazards/flypanel-add-safety-hazards/flypanel-add-safety-hazards.module';


@NgModule({
  declarations: [
    FlyPanelToolShLinkComponent
  ],
  imports: [
    CommonModule,
    BaseModule,
    MatTreeModule,
    MatCheckboxModule,
    FormsModule,
    FlypanelAddSafetyHazardsModule,
  ],
  exports:[FlyPanelToolShLinkComponent]
})
export class FlyPanelToolShLinkModule { }

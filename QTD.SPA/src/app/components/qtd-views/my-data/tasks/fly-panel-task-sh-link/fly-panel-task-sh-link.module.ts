import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FlyPanelTaskShLinkComponent } from './fly-panel-task-sh-link.component';
import { BaseModule } from 'src/app/components/base/base.module';
import { MatTreeModule } from '@angular/material/tree';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { MatLegacyMenuModule as MatMenuModule } from '@angular/material/legacy-menu';
import { FormsModule } from '@angular/forms';
import { FlypanelAddSafetyHazardsModule } from '../../safety-hazards/flypanel-add-safety-hazards/flypanel-add-safety-hazards.module';



@NgModule({
  declarations: [
    FlyPanelTaskShLinkComponent
  ],
  imports: [
    CommonModule,
    BaseModule,
    MatTreeModule,
    MatCheckboxModule,
    MatMenuModule,
    FormsModule,
    FlypanelAddSafetyHazardsModule,
  ],
  exports : [
    FlyPanelTaskShLinkComponent,
  ]
})
export class FlyPanelTaskShLinkModule { }

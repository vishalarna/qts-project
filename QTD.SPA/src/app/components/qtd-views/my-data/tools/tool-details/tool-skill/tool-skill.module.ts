import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ToolSkillComponent } from './tool-skill.component';
import { BaseModule } from 'src/app/components/base/base.module';
import { MatLegacyTableModule as MatTableModule } from '@angular/material/legacy-table';
import { MatLegacyTooltipModule as MatTooltipModule } from '@angular/material/legacy-tooltip';
import { MatLegacyPaginatorModule as MatPaginatorModule } from '@angular/material/legacy-paginator';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { FlyPanelToolSkillLinkModule } from '../../fly-panel-tool-skill-link/fly-panel-tool-skill-link.module';
import { FlyPanelLinkedToolsModule } from '../../fly-panel-linked-tools/fly-panel-linked-tools.module';



@NgModule({
  declarations: [
    ToolSkillComponent
  ],
  imports: [
    CommonModule,
    BaseModule,
    MatTableModule,
    MatTooltipModule,
    MatPaginatorModule,
    MatCheckboxModule,
    FlyPanelToolSkillLinkModule,
    FlyPanelLinkedToolsModule
  ],
  exports:[ToolSkillComponent]
})
export class ToolSkillModule { }

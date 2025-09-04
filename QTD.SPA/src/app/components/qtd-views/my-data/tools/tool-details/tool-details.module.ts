import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ToolDetailsComponent } from './tool-details.component';
import { BaseModule } from 'src/app/components/base/base.module';
import { RouterModule, Routes } from '@angular/router';
import { ToolShModule } from './tool-sh/tool-sh.module';
import { ToolSkillModule } from './tool-skill/tool-skill.module';
import { MatLegacyMenuModule as MatMenuModule } from '@angular/material/legacy-menu';
import { MatLegacyTabsModule as MatTabsModule } from '@angular/material/legacy-tabs';
import { ToolTaskModule } from './tool-task/tool-task.module';
import { FlyPanelAddToolModule } from '../fly-panel-add-tool/fly-panel-add-tool.module';
import { MatLegacyTooltipModule as MatTooltipModule } from '@angular/material/legacy-tooltip';

const routes: Routes = [
  {
    path: ':id',
    component: ToolDetailsComponent,
  },
];


@NgModule({
  declarations: [
    ToolDetailsComponent
  ],
  imports: [
    CommonModule,
    BaseModule,
    RouterModule.forChild(routes),
    ToolShModule,
    ToolSkillModule,
    ToolTaskModule,
    MatMenuModule,
    MatTabsModule,
    FlyPanelAddToolModule,
    MatTooltipModule
  ]
})
export class ToolDetailsModule { }

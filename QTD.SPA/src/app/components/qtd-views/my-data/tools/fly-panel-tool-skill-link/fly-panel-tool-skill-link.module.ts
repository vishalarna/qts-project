import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FlyPanelToolSkillLinkComponent } from './fly-panel-tool-skill-link.component';
import { FormsModule } from '@angular/forms';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { MatLegacyMenuModule as MatMenuModule } from '@angular/material/legacy-menu';
import { MatTreeModule } from '@angular/material/tree';
import { BaseModule } from 'src/app/components/base/base.module';
import { FlypanelAddEoModule } from '../../enabling-objectives/flypanel-add-eo/flypanel-add-eo.module';


@NgModule({
  declarations: [
    FlyPanelToolSkillLinkComponent
  ],
  imports: [
    CommonModule,
    MatMenuModule,
    FormsModule,
    MatCheckboxModule,
    MatTreeModule,
    BaseModule,
    FlypanelAddEoModule
  ],exports: [FlyPanelToolSkillLinkComponent]
})
export class FlyPanelToolSkillLinkModule { }

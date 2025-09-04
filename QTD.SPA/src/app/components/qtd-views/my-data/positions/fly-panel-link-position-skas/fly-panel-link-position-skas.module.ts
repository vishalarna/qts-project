import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FlyPanelLinkPositionSkasComponent } from './fly-panel-link-position-skas.component';
import { FormsModule } from '@angular/forms';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { MatLegacyMenuModule as MatMenuModule } from '@angular/material/legacy-menu';
import { MatTreeModule } from '@angular/material/tree';
import { BaseModule } from 'src/app/components/base/base.module';
import { FlyPanelAddSkaCategoryModule } from '../../../design-and-development/skills-assessment/fly-panel-add-ska-category/fly-panel-add-ska-category.module';
import { FlyPanelAddSkaSubCategoryModule } from '../../../design-and-development/skills-assessment/fly-panel-add-ska-sub-category/fly-panel-add-ska-sub-category.module';
import { FlyPanelAddSkaTopicModule } from '../../../design-and-development/skills-assessment/fly-panel-add-ska-topic/fly-panel-add-ska-topic.module';
import { FlyPanelAddSkaModule } from '../../../design-and-development/skills-assessment/fly-panel-add-ska/fly-panel-add-ska.module';
import { FlypanelAddEoModule } from '../../enabling-objectives/flypanel-add-eo/flypanel-add-eo.module';



@NgModule({
  declarations: [
    FlyPanelLinkPositionSkasComponent
  ],
  imports: [
    CommonModule,
    BaseModule,
    // FlyPanelAddSkaModule,
    // FlyPanelAddSkaCategoryModule,
    // FlyPanelAddSkaSubCategoryModule,
    // FlyPanelAddSkaTopicModule,
    FlypanelAddEoModule,
    MatMenuModule,
    FormsModule,
    MatCheckboxModule,
    MatTreeModule
  ],
  exports: [FlyPanelLinkPositionSkasComponent],
})
export class FlyPanelLinkPositionSkasModule { }

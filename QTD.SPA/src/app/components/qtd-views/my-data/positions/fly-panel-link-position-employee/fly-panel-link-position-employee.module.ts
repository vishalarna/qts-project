import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FlyPanelLinkPositionEmployeeComponent } from './fly-panel-link-position-employee.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { MatLegacyMenuModule as MatMenuModule } from '@angular/material/legacy-menu';
import { MatTreeModule } from '@angular/material/tree';
import { BaseModule } from 'src/app/components/base/base.module';
import { FlyPanelAddSkaCategoryModule } from '../../../design-and-development/skills-assessment/fly-panel-add-ska-category/fly-panel-add-ska-category.module';
import { FlyPanelAddSkaSubCategoryModule } from '../../../design-and-development/skills-assessment/fly-panel-add-ska-sub-category/fly-panel-add-ska-sub-category.module';
import { FlyPanelAddSkaTopicModule } from '../../../design-and-development/skills-assessment/fly-panel-add-ska-topic/fly-panel-add-ska-topic.module';
import { FlyPanelAddSkaModule } from '../../../design-and-development/skills-assessment/fly-panel-add-ska/fly-panel-add-ska.module';



@NgModule({
  declarations: [
    FlyPanelLinkPositionEmployeeComponent
  ],
  imports: [
    CommonModule,
    BaseModule,
    FlyPanelAddSkaModule,
    FlyPanelAddSkaCategoryModule,
    FlyPanelAddSkaSubCategoryModule,
    FlyPanelAddSkaTopicModule,
    MatMenuModule,
    FormsModule,
    MatCheckboxModule,
    MatTreeModule,
    FormsModule,
    ReactiveFormsModule
  ],

  exports:[FlyPanelLinkPositionEmployeeComponent]
})
export class FlyPanelLinkPositionEmployeeModule { }

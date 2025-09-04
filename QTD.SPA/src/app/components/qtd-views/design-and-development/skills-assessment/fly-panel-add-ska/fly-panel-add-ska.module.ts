import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FlyPanelAddSkaComponent } from './fly-panel-add-ska.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { MatLegacyChipsModule as MatChipsModule } from '@angular/material/legacy-chips';
import { MatLegacySelectModule as MatSelectModule } from '@angular/material/legacy-select';
import { MatStepperModule } from '@angular/material/stepper';
import { BaseModule } from 'src/app/components/base/base.module';
import { FlyPanelAddSkaCategoryModule } from '../fly-panel-add-ska-category/fly-panel-add-ska-category.module';
import { FlyPanelAddSkaSubCategoryModule } from '../fly-panel-add-ska-sub-category/fly-panel-add-ska-sub-category.module';
import { FlyPanelAddSkaTopicModule } from '../fly-panel-add-ska-topic/fly-panel-add-ska-topic.module';



@NgModule({
  declarations: [
    FlyPanelAddSkaComponent
  ],
  imports: [
    CommonModule,
    BaseModule,
    FormsModule,
    ReactiveFormsModule,
    MatStepperModule,
    MatSelectModule,
    MatChipsModule,
    MatCheckboxModule,
    FlyPanelAddSkaCategoryModule,
    FlyPanelAddSkaSubCategoryModule,
    FlyPanelAddSkaTopicModule
  
  ],
  exports: [FlyPanelAddSkaComponent]
})
export class FlyPanelAddSkaModule { }

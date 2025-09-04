import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FlypanelAddEoComponent } from './flypanel-add-eo.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { MatLegacyChipsModule as MatChipsModule } from '@angular/material/legacy-chips';
import { MatLegacySelectModule as MatSelectModule } from '@angular/material/legacy-select';
import { MatStepperModule } from '@angular/material/stepper';
import { BaseModule } from 'src/app/components/base/base.module';
import { FlypanelEOCategoryModule } from '../flypanel-eo-category/flypanel-eo-category.module';
import { FlypanelEOSubCategoryModule } from '../flypanel-eo-sub-category/flypanel-eo-sub-category.module';
import { FlypanelEOTopicModule } from '../flypanel-eo-topic/flypanel-eo-topic.module';



@NgModule({
  declarations: [FlypanelAddEoComponent],
  imports: [
    CommonModule,
    BaseModule,
    FormsModule,
    ReactiveFormsModule,
    MatStepperModule,
    MatSelectModule,
    MatChipsModule,
    MatCheckboxModule,
    FlypanelEOCategoryModule,
    FlypanelEOSubCategoryModule,
    FlypanelEOTopicModule
  ],
  exports: [FlypanelAddEoComponent]
})
export class FlypanelAddEoModule { }

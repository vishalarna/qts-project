import { FlyPanelAddToolModule } from './../../tools/fly-panel-add-tool/fly-panel-add-tool.module';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FlypanelAddSafetyHazardsComponent } from './flypanel-add-safety-hazards.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { MatLegacyChipsModule as MatChipsModule } from '@angular/material/legacy-chips';
import { MatLegacySelectModule as MatSelectModule } from '@angular/material/legacy-select';
import { MatStepperModule } from '@angular/material/stepper';
import { BaseModule } from 'src/app/components/base/base.module';
import { FlypanelEOCategoryModule } from '../../enabling-objectives/flypanel-eo-category/flypanel-eo-category.module';
import { FlyPanelShCategoryModule } from '../fly-panel-sh-category/fly-panel-sh-category.module';
import { CKEditorModule } from '@ckeditor/ckeditor5-angular';


@NgModule({
  declarations: [FlypanelAddSafetyHazardsComponent],
  imports: [
    CommonModule,
    BaseModule,
    FormsModule,
    ReactiveFormsModule,
    MatStepperModule,
    MatSelectModule,
    MatChipsModule,
    MatCheckboxModule,
    FlyPanelShCategoryModule,
    CKEditorModule,
    FlyPanelAddToolModule
  ],
  exports: [FlypanelAddSafetyHazardsComponent]
})
export class FlypanelAddSafetyHazardsModule { }

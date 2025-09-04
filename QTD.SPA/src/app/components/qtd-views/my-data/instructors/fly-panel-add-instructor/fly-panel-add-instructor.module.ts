import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FlyPanelAddInstructorComponent } from './fly-panel-add-instructor.component';
import { BaseModule } from 'src/app/components/base/base.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { MatLegacyFormFieldModule as MatFormFieldModule } from '@angular/material/legacy-form-field';
import { MatLegacySelectModule as MatSelectModule } from '@angular/material/legacy-select';
import { FlyPanelIssuingAuthorityModule } from '../../procedures/fly-panel-add-issuing-authority/fly-panel-add-issuing-authority.module';
import { MatStepperModule } from '@angular/material/stepper';
import { FlyPanelAddInstructorCategoryModule } from '../fly-panel-add-instructor-category/fly-panel-add-instructor-category.module';



@NgModule({
  declarations: [
    FlyPanelAddInstructorComponent
  ],
  imports: [
    CommonModule,
    BaseModule,
    FormsModule,
    ReactiveFormsModule,
    MatCheckboxModule,
    MatFormFieldModule,
    MatSelectModule,
    FlyPanelIssuingAuthorityModule,
    MatStepperModule,
    FlyPanelAddInstructorCategoryModule
  ],
  exports: [FlyPanelAddInstructorComponent]
})
export class FlyPanelAddInstructorModule { }

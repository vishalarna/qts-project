import { FlyPanelAddLocationCategoryModule } from './../fly-panel-add-location-category/fly-panel-add-location-category.module';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FlyPanelAddLocationComponent } from './fly-panel-add-location.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { MatLegacyFormFieldModule as MatFormFieldModule } from '@angular/material/legacy-form-field';
import { MatLegacySelectModule as MatSelectModule } from '@angular/material/legacy-select';
import { MatStepperModule } from '@angular/material/stepper';
import { BaseModule } from 'src/app/components/base/base.module';
import { FlyPanelIssuingAuthorityModule } from '../../procedures/fly-panel-add-issuing-authority/fly-panel-add-issuing-authority.module';


@NgModule({
  declarations: [
    FlyPanelAddLocationComponent
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
    FlyPanelAddLocationCategoryModule
  ],
  exports: [FlyPanelAddLocationComponent]
})
export class FlyPanelAddLocationModule { }
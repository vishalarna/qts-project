import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FlyPanelAddDefinitionComponent } from './fly-panel-add-definition.component';
import { BaseModule } from 'src/app/components/base/base.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { MatLegacyFormFieldModule as MatFormFieldModule } from '@angular/material/legacy-form-field';
import { MatLegacySelectModule as MatSelectModule } from '@angular/material/legacy-select';
import { MatStepperModule } from '@angular/material/stepper';



@NgModule({
  declarations: [
    FlyPanelAddDefinitionComponent
  ],
  imports: [
    CommonModule,
    BaseModule,
    FormsModule,
    ReactiveFormsModule,
    MatCheckboxModule,
    MatFormFieldModule,
    MatSelectModule,
    MatStepperModule
  ],
  exports: [FlyPanelAddDefinitionComponent]
})
export class FlyPanelAddDefinitionModule { }

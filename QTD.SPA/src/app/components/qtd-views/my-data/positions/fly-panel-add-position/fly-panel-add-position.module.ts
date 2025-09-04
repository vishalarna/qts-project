import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FlyPanelAddPositionComponent } from './fly-panel-add-position.component';
import { BaseModule } from 'src/app/components/base/base.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { MatLegacyFormFieldModule as MatFormFieldModule } from '@angular/material/legacy-form-field';
import { MatLegacySelectModule as MatSelectModule } from '@angular/material/legacy-select';
import { MatStepperModule } from '@angular/material/stepper';



@NgModule({
  declarations: [
    FlyPanelAddPositionComponent
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
  exports: [FlyPanelAddPositionComponent]
})
export class FlyPanelAddPositionModule { }

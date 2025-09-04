import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FlyPanelAddEmployeePositionComponent } from './fly-panel-add-employee-position.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { MatLegacyFormFieldModule as MatFormFieldModule } from '@angular/material/legacy-form-field';
import { MatLegacySelectModule as MatSelectModule } from '@angular/material/legacy-select';
import { MatStepperModule } from '@angular/material/stepper';
import { BaseModule } from 'src/app/components/base/base.module';
import { MatLegacyTooltipModule as MatTooltipModule } from '@angular/material/legacy-tooltip';
import { FlyPanelAddPositionModule } from '../../../my-data/positions/fly-panel-add-position/fly-panel-add-position.module';



@NgModule({
  declarations: [
    FlyPanelAddEmployeePositionComponent
  ],
  imports: [
    CommonModule,
    BaseModule,
    FormsModule,
    ReactiveFormsModule,
    MatCheckboxModule,
    MatFormFieldModule,
    MatSelectModule,
    MatStepperModule,
    MatTooltipModule,
    FlyPanelAddPositionModule
  ],
  exports:[FlyPanelAddEmployeePositionComponent]
})
export class FlyPanelAddEmployeePositionModule { }

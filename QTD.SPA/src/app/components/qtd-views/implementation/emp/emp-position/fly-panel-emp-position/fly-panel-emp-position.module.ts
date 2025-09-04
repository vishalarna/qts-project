import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FlyPanelEmpPositionComponent } from './fly-panel-emp-position.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { FlyPanelPositionsModule } from '../../../positions/fly-panel-positions/fly-panel-positions.module';
import { MatLegacyFormFieldModule as MatFormFieldModule } from '@angular/material/legacy-form-field';
import { MatLegacySelectModule as MatSelectModule } from '@angular/material/legacy-select';
import { BaseModule } from 'src/app/components/base/base.module';

@NgModule({
  declarations: [FlyPanelEmpPositionComponent],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    FlyPanelPositionsModule,
    MatFormFieldModule,
    MatSelectModule,
    BaseModule,
  ],
  exports: [FlyPanelEmpPositionComponent],
})
export class FlyPanelEmpPositionModule {}

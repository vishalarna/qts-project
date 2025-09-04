import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FlyPanelTrainingprogramYearFilterComponent } from './fly-panel-trainingprogram-year-filter.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BaseModule } from 'src/app/components/base/base.module';
import { LayoutModule } from '@angular/cdk/layout';
import { MatLegacyOptionModule as MatOptionModule } from '@angular/material/legacy-core';
import { MatLegacySelectModule as MatSelectModule } from '@angular/material/legacy-select';



@NgModule({
  declarations: [
    FlyPanelTrainingprogramYearFilterComponent
  ],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    FormsModule,
    BaseModule,
    LayoutModule,
    MatOptionModule,
    MatSelectModule
  ],
  exports:[FlyPanelTrainingprogramYearFilterComponent]
})
export class FlyPanelTrainingprogramYearFilterModule { }

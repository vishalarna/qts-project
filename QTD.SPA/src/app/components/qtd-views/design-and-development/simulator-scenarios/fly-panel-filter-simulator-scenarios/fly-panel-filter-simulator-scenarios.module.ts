import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LayoutModule } from '../../../layout/layout.module';
import { BaseModule } from 'src/app/components/base/base.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatLegacySelectModule as MatSelectModule } from '@angular/material/legacy-select';
import { MatLegacyFormFieldModule as MatFormFieldModule } from '@angular/material/legacy-form-field';
import { MatIconModule } from '@angular/material/icon';
import { FlyPanelFilterSimulatorScenariosComponent } from './fly-panel-filter-simulator-scenarios.component';



@NgModule({
  declarations: [FlyPanelFilterSimulatorScenariosComponent],
  imports: [
    CommonModule,
    LayoutModule,
    BaseModule,
    FormsModule,
    ReactiveFormsModule,
    MatSelectModule,
    MatFormFieldModule,
    MatIconModule
  ],
  exports:[FlyPanelFilterSimulatorScenariosComponent]
})
export class FlyPanelFilterSimulatorScenariosModule { }

import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FlyPanelFilterLocationComponent } from './fly-panel-filter-location.component';
import { LayoutModule } from '@angular/cdk/layout';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatLegacySelectModule as MatSelectModule } from '@angular/material/legacy-select';
import { BaseModule } from 'src/app/components/base/base.module';



@NgModule({
  declarations: [FlyPanelFilterLocationComponent],
  imports: [
    FormsModule,
    ReactiveFormsModule,
    CommonModule,
    BaseModule,
    LayoutModule,
    MatSelectModule,
  ]
  , exports: [FlyPanelFilterLocationComponent]
})
export class FlyPanelFilterLocationModule { }

import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FlyPanelLinkPositionFilterComponent } from './fly-panel-link-position-filter.component';
import { BaseModule } from 'src/app/components/base/base.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatLegacySelectModule as MatSelectModule } from '@angular/material/legacy-select';
import { MatIconModule } from '@angular/material/icon';



@NgModule({
  declarations: [FlyPanelLinkPositionFilterComponent],
  imports: [
    CommonModule,
    FormsModule,
    BaseModule,
    MatSelectModule,
    ReactiveFormsModule,
    MatIconModule
  ],
  exports: [FlyPanelLinkPositionFilterComponent],
})
export class FlyPanelLinkPositionFilterModule { }

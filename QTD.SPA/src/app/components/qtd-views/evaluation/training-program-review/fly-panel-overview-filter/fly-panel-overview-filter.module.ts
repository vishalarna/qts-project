import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatLegacyFormFieldModule as MatFormFieldModule } from '@angular/material/legacy-form-field';
import { BaseModule } from 'src/app/components/base/base.module';
import { MatLegacySelectModule as MatSelectModule } from '@angular/material/legacy-select';
import { LayoutModule } from '../../../layout/layout.module';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { FlyPanelOverviewFilterComponent } from './fly-panel-overview-filter.component';
import { Routes } from '@angular/router';
import { MatIconModule } from '@angular/material/icon';

const routes: Routes = [
  {
    path: '',
    component: FlyPanelOverviewFilterComponent,
  },
];

@NgModule({
  declarations: [
    FlyPanelOverviewFilterComponent
  ],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatFormFieldModule,
    MatSelectModule,
    BaseModule,
    LayoutModule,
    FormsModule,
    MatDatepickerModule,
    MatIconModule
  ],
  exports:[FlyPanelOverviewFilterComponent]
})
export class FlyPanelOverviewFilterModule { }

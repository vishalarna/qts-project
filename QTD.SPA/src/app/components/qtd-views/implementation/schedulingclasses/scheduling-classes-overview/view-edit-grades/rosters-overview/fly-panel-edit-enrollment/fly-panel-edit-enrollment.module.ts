import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FlyPanelEditEnrollmentComponent } from './fly-panel-edit-enrollment.component';
import { BaseModule } from 'src/app/components/base/base.module';
import { MatLegacySelectModule as MatSelectModule } from '@angular/material/legacy-select';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatLegacyTooltipModule as MatTooltipModule } from '@angular/material/legacy-tooltip';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { LayoutModule } from 'src/app/components/qtd-views/layout/layout.module';
import { MatIconModule } from '@angular/material/icon';



@NgModule({
  declarations: [FlyPanelEditEnrollmentComponent],
  imports: [
    FormsModule,
    ReactiveFormsModule,
    CommonModule,
    BaseModule,
    LayoutModule,
    MatSelectModule,
    MatTooltipModule,
    MatCheckboxModule,
    MatIconModule
  ],
  exports: [FlyPanelEditEnrollmentComponent]
})
export class FlyPanelEditEnrollmentModule { }

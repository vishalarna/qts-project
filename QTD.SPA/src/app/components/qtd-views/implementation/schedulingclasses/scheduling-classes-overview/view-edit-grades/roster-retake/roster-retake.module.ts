import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RosterRetakeComponent } from './roster-retake.component';
import { BaseModule } from 'src/app/components/base/base.module';
import { NgChartsModule } from 'ng2-charts';
import { MatLegacyMenuModule as MatMenuModule } from '@angular/material/legacy-menu';
import { MatLegacySelectModule as MatSelectModule } from '@angular/material/legacy-select';
import { MatLegacyTableModule as MatTableModule } from '@angular/material/legacy-table';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatIconModule } from '@angular/material/icon';
import { MatLegacyTooltipModule as MatTooltipModule } from '@angular/material/legacy-tooltip';



@NgModule({
  declarations: [
    RosterRetakeComponent
  ],
  imports: [
    CommonModule,
    BaseModule,
    NgChartsModule,
    MatMenuModule,
    MatSelectModule,
    MatTableModule,
    ReactiveFormsModule,
    MatIconModule,
    FormsModule,
    MatIconModule,
    MatTooltipModule,
  ],
  exports: [
    RosterRetakeComponent,
  ]
})
export class RosterRetakeModule { }

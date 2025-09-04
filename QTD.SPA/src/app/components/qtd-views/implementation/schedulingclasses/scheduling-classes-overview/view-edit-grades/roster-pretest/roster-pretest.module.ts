import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RosterPretestComponent } from './roster-pretest.component';
import { BaseModule } from 'src/app/components/base/base.module';
import { MatLegacyTableModule as MatTableModule } from '@angular/material/legacy-table';
import { MatLegacySelectModule as MatSelectModule } from '@angular/material/legacy-select';
import { NgChartsModule } from 'ng2-charts';
import { MatLegacyMenuModule as MatMenuModule } from '@angular/material/legacy-menu';
import { FlyPanelEditGradesModule } from '../fly-panel-edit-grades/fly-panel-edit-grades.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatIconModule } from '@angular/material/icon';
import { MatLegacyTooltipModule as MatTooltipModule } from '@angular/material/legacy-tooltip';



@NgModule({
  declarations: [
    RosterPretestComponent
  ],
  imports: [
    CommonModule,
    BaseModule,
    MatTableModule,
    MatSelectModule,
    NgChartsModule,
    MatMenuModule,
    FlyPanelEditGradesModule,
    ReactiveFormsModule,
    MatIconModule,
    FormsModule,
    MatTooltipModule,
  ],
  exports: [
    RosterPretestComponent
  ]
})
export class RosterPretestModule { }

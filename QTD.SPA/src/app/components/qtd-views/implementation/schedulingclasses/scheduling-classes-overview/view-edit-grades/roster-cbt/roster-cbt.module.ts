import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RosterCbtComponent } from './roster-cbt.component';
import { BaseModule } from 'src/app/components/base/base.module';
import { MatLegacyMenuModule as MatMenuModule } from '@angular/material/legacy-menu';
import { MatLegacySelectModule as MatSelectModule } from '@angular/material/legacy-select';
import { NgChartsModule } from 'ng2-charts';
import { MatLegacyTableModule as MatTableModule } from '@angular/material/legacy-table';
import { FlyPanelEditGradesModule } from '../fly-panel-edit-grades/fly-panel-edit-grades.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatIconModule } from '@angular/material/icon';



@NgModule({
  declarations: [
    RosterCbtComponent
  ],
  imports: [
    CommonModule,
    BaseModule,
    MatMenuModule,
    MatSelectModule,
    NgChartsModule,
    MatTableModule,
    FlyPanelEditGradesModule,
    ReactiveFormsModule,
    MatIconModule,
    FormsModule,
  ],
  exports : [
    RosterCbtComponent
  ]
})
export class RosterCbtModule { }

import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RostersOverviewComponent } from './rosters-overview.component';
import { BaseModule } from 'src/app/components/base/base.module';
import { MatLegacyTableModule as MatTableModule } from '@angular/material/legacy-table';
import { MatLegacyMenuModule as MatMenuModule } from '@angular/material/legacy-menu';
import { NgChartsModule } from 'ng2-charts';
import { FlyPanelEditScoreModule } from '../fly-panel-edit-score/fly-panel-edit-score.module';
import { MatIconModule } from '@angular/material/icon';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatLegacyDialogModule as MatDialogModule } from '@angular/material/legacy-dialog';
import { FlypanelRetakeStatusModule } from '../flypanel-retake-status/flypanel-retake-status.module';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { FlyPanelEditGradesModule } from '../fly-panel-edit-grades/fly-panel-edit-grades.module';
import { FlyPanelEditEnrollmentComponent } from './fly-panel-edit-enrollment/fly-panel-edit-enrollment.component';
import { FlyPanelEditEnrollmentModule } from './fly-panel-edit-enrollment/fly-panel-edit-enrollment.module';
import { RosterBulkUpdateDialogComponent } from './roster-bulk-update-dialog/roster-bulk-update-dialog.component';
import { RosterBulkUpdateDialogModule } from './roster-bulk-update-dialog/roster-bulk-update-dialog.module';
import { MatSortModule } from '@angular/material/sort';


@NgModule({
  declarations: [
    RostersOverviewComponent,
  ],
  imports: [
    CommonModule,
    BaseModule,
    MatTableModule,
    MatMenuModule,
    NgChartsModule,
    FlyPanelEditScoreModule,
    FlyPanelEditGradesModule,
    MatIconModule,
    FormsModule,
    MatDialogModule,
    FlypanelRetakeStatusModule,
    MatCheckboxModule,
    ReactiveFormsModule,
    FlyPanelEditEnrollmentModule,
    RosterBulkUpdateDialogModule,
    MatSortModule
  ],
  exports: [
    RostersOverviewComponent
  ]
})
export class RostersOverviewModule { }

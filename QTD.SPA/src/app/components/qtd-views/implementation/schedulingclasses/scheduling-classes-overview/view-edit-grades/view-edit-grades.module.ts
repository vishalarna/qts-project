import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { ViewEditGradesComponent } from './view-edit-grades.component';
import { BaseModule } from 'src/app/components/base/base.module';
import { LayoutModule } from 'src/app/components/qtd-views/layout/layout.module';
import { MatLegacyTabsModule as MatTabsModule } from '@angular/material/legacy-tabs';
import { MatLegacySelectModule as MatSelectModule } from '@angular/material/legacy-select';
import { MatLegacyPaginatorModule as MatPaginatorModule } from '@angular/material/legacy-paginator';
import { MatLegacyTableModule as MatTableModule } from '@angular/material/legacy-table';
import { MatSortModule } from '@angular/material/sort';
import { MatLegacyMenuModule as MatMenuModule } from '@angular/material/legacy-menu';
import { NgChartsModule } from 'ng2-charts';
import { FlyPanelEditGradesModule } from './fly-panel-edit-grades/fly-panel-edit-grades.module';
import { FlyPanelEditRoasterModule } from './fly-panel-edit-roaster/fly-panel-edit-roaster.module';
import { FlyPanelEditScoreModule } from './fly-panel-edit-score/fly-panel-edit-score.module';
import { StudentEvalWithoutEmpModule } from './student-eval-without-emp/student-eval-without-emp.module';
import { RostersOverviewModule } from './rosters-overview/rosters-overview.module';
import { RosterPretestModule } from './roster-pretest/roster-pretest.module';
import { RosterCbtModule } from './roster-cbt/roster-cbt.module';
import { RosterTestModule } from './roster-test/roster-test.module';
import { RosterRetakeModule } from './roster-retake/roster-retake.module';
import { StudentEvalWithEmpModule } from './student-eval-with-emp/student-eval-with-emp.module';

const routes: Routes = [
  {
    path: '',
    pathMatch:'full',
    component: ViewEditGradesComponent,
  },
];

@NgModule({
  declarations: [ViewEditGradesComponent],
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    BaseModule,
    LayoutModule,
    MatTabsModule,
    MatSelectModule,
    MatTableModule,
    MatPaginatorModule,
    MatSortModule,
    MatMenuModule,
    NgChartsModule,
    FlyPanelEditGradesModule,
    FlyPanelEditRoasterModule,
    FlyPanelEditScoreModule,
    StudentEvalWithoutEmpModule,
    RostersOverviewModule,
    RosterPretestModule,
    RosterCbtModule,
    RosterTestModule,
    RosterRetakeModule,
    StudentEvalWithEmpModule,
  ],
})
export class ViewEditGradesModule { }

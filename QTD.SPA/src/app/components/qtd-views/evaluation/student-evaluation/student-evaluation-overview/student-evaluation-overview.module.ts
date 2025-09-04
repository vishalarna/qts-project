import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { StudentEvaluationOverviewComponent } from './student-evaluation-overview.component';
import { RouterModule, Routes } from '@angular/router';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { MatLegacyMenuModule as MatMenuModule } from '@angular/material/legacy-menu';
import { MatLegacyPaginatorModule as MatPaginatorModule } from '@angular/material/legacy-paginator';
import { MatLegacyTableModule as MatTableModule } from '@angular/material/legacy-table';
import { BaseModule } from 'src/app/components/base/base.module';
import { FlyPanelEditEvaluationModule } from '../fly-panel-edit-evaluation/fly-panel-edit-evaluation.module';
import { LayoutModule } from '../../../layout/layout.module';
import { MatSortModule } from '@angular/material/sort';
import { MatLegacyTooltipModule as MatTooltipModule } from '@angular/material/legacy-tooltip';
import { FlyPanelStudentEvaluationListModule } from '../fly-panel-student-evaluation-list/fly-panel-student-evaluation-list.module';
import { FormsModule } from '@angular/forms';

const routes: Routes = [
  {
    path: '',
    component: StudentEvaluationOverviewComponent,
    
  },
];

@NgModule({
  declarations: [
    StudentEvaluationOverviewComponent
  ],
  imports: [
    CommonModule,
    MatTableModule,
    MatPaginatorModule,
    MatSortModule,
    LayoutModule,
    BaseModule,
    RouterModule.forChild(routes),
    LayoutModule,
    MatMenuModule,
    MatCheckboxModule,
    FlyPanelEditEvaluationModule,
    MatTooltipModule,
    FlyPanelStudentEvaluationListModule,
    FormsModule
  ]
})
export class StudentEvaluationOverviewModule { }

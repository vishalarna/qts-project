import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BaseModule } from 'src/app/components/base/base.module';

import { FormsModule } from '@angular/forms';
import { MatLegacyTableModule as MatTableModule } from '@angular/material/legacy-table';
import { MatLegacyPaginatorModule as MatPaginatorModule } from '@angular/material/legacy-paginator';
import { MatSortModule } from '@angular/material/sort';
import { MatLegacyMenuModule as MatMenuModule } from '@angular/material/legacy-menu';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { MatIconModule } from '@angular/material/icon';
import { ViewEnrollmentComponent } from './view-enrollment.component';
import { RouterModule, Routes } from '@angular/router';
import { MatToolbarModule } from '@angular/material/toolbar';
import { LayoutModule } from 'src/app/components/qtd-views/layout/layout.module';
import { NgChartsModule } from 'ng2-charts';
import { FlyPanelDifSurveyEmployeesModule } from '../../dif-survey-create-wizard/dif-assign-employees/fly-panel-dif-survey-employees/fly-panel-dif-survey-employees.module';
import { MatLegacyTooltipModule as MatTooltipModule } from '@angular/material/legacy-tooltip';

const routes: Routes = [
  {
    path: '',
    component: ViewEnrollmentComponent,
  }
]

@NgModule({
  declarations: [
    ViewEnrollmentComponent
  ],
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    BaseModule,
    LayoutModule,
    FormsModule,
    MatTableModule,
    MatPaginatorModule,
    MatSortModule,
    MatMenuModule,
    MatCheckboxModule,
    MatIconModule,
    MatToolbarModule,
    NgChartsModule,
    FlyPanelDifSurveyEmployeesModule,
    MatTooltipModule
  ],
  exports:[ViewEnrollmentComponent]
})
export class ViewEnrollmentModule { }

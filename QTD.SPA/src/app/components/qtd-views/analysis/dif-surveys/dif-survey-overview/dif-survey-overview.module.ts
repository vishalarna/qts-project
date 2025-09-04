import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { BaseModule } from 'src/app/components/base/base.module';
import { LayoutModule } from '../../../layout/layout.module';
import { FormsModule } from '@angular/forms';
import { MatLegacyTableModule as MatTableModule } from '@angular/material/legacy-table';
import { MatLegacyPaginatorModule as MatPaginatorModule } from '@angular/material/legacy-paginator';
import { MatSortModule } from '@angular/material/sort';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { MatLegacyMenuModule as MatMenuModule } from '@angular/material/legacy-menu';
import { DifSurveyOverviewComponent } from './dif-survey-overview.component';
import { FlyPanelFilterDifSurveyModule } from '../fly-panel-filter-dif-survey/fly-panel-filter-dif-survey.module';
import { MatIconModule } from '@angular/material/icon';

const routes: Routes = [
  {
    path: '',
    component: DifSurveyOverviewComponent,
  }
];

@NgModule({
  declarations: [
    DifSurveyOverviewComponent
  ],
  imports: [
    CommonModule,
    BaseModule,
    RouterModule.forChild(routes),
    LayoutModule,
    FormsModule,
    MatTableModule,
    MatPaginatorModule,
    MatSortModule,
    MatMenuModule,
    MatCheckboxModule,
    FlyPanelFilterDifSurveyModule,
    MatIconModule
  ],
  exports:[DifSurveyOverviewComponent]
})
export class DifSurveyOverviewModule { }

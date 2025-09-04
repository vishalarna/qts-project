import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TrainingProgramOverviewComponent } from './training-program-overview.component';
import { RouterModule, Routes } from '@angular/router';
import { MatLegacyPaginatorModule as MatPaginatorModule } from '@angular/material/legacy-paginator';
import { MatSortModule } from '@angular/material/sort';
import { MatLegacyTableModule as MatTableModule } from '@angular/material/legacy-table';
import { BaseModule } from 'src/app/components/base/base.module';
import { LayoutModule } from '../../../layout/layout.module';
import { MatLegacyMenu as MatMenu, MatLegacyMenuModule as MatMenuModule } from '@angular/material/legacy-menu';
import { FlyPanelTrainingprogramPositionFilterComponent } from '../fly-panel-trainingprogram-position-filter/fly-panel-trainingprogram-position-filter.component';
import { FlyPanelTrainingprogramPositionFilterModule } from '../fly-panel-trainingprogram-position-filter/fly-panel-trainingprogram-position-filter.module';
import { FlyPanelTrainingprogramYearFilterModule } from '../fly-panel-trainingprogram-year-filter/fly-panel-trainingprogram-year-filter.module';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { AuthGuard } from 'src/app/_Guards/auth.guard';
import { RouteGuard } from 'src/app/_Guards/route.guard';
import { FormsModule } from '@angular/forms';
import { FlyPanelActiveInactiveListModule } from '../fly-panel-active-inactive-list/fly-panel-active-inactive-list.module';


const routes: Routes = [
  {
    path: '',
    component: TrainingProgramOverviewComponent,
  },
  
];

@NgModule({
  declarations: [
    TrainingProgramOverviewComponent
  ],
  imports: [
    CommonModule,
    MatTableModule,
    MatPaginatorModule,
    MatSortModule,
    BaseModule,
    RouterModule.forChild(routes),
    LayoutModule,
    MatMenuModule,
    FlyPanelTrainingprogramPositionFilterModule,
    FlyPanelTrainingprogramYearFilterModule,
    MatCheckboxModule,
    FlyPanelActiveInactiveListModule,
    FormsModule,
  ]
})
export class TrainingProgramOverviewModule { }

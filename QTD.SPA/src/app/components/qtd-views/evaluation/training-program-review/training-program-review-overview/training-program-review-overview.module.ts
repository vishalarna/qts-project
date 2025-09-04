import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TrainingProgramReviewOverviewComponent } from './training-program-review-overview.component';
import { RouterModule, Routes } from '@angular/router';
import { BaseModule } from 'src/app/components/base/base.module';
import { LayoutModule } from '../../../layout/layout.module';
import { FormsModule } from '@angular/forms';
import { MatLegacyTableModule as MatTableModule } from '@angular/material/legacy-table';
import { MatLegacyPaginatorModule as MatPaginatorModule } from '@angular/material/legacy-paginator';
import { MatSortModule } from '@angular/material/sort';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { MatLegacyMenuModule as MatMenuModule } from '@angular/material/legacy-menu';
import { FlyPanelOverviewFilterModule } from '../fly-panel-overview-filter/fly-panel-overview-filter.module';
import { TrainingProgramReviewWizardModule } from '../trainingprogramreview-wizard/trainingprogramreview-wizard.module';
import { FlyPanelOverviewTpNoreviewsModule } from '../fly-panel-overview-tp-noreviews/fly-panel-overview-tp-noreviews.module';

const routes: Routes = [
  {
    path: '',
    component: TrainingProgramReviewOverviewComponent,
  },
];

@NgModule({
  declarations: [
    TrainingProgramReviewOverviewComponent
  ],
  imports: [
    CommonModule,
    LayoutModule,
    BaseModule,
    RouterModule.forChild(routes),
    LayoutModule,
    FormsModule,    
    BaseModule,
    LayoutModule,
    MatTableModule,
    MatPaginatorModule,
    MatSortModule,
    MatMenuModule,
    MatCheckboxModule,
    FlyPanelOverviewFilterModule,
    TrainingProgramReviewWizardModule,
    FlyPanelOverviewTpNoreviewsModule
  ],
  exports:[TrainingProgramReviewOverviewComponent]
})
export class TrainingProgramReviewOverviewModule { }

import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TrainingIssuesOverviewComponent } from './training-issues-overview.component';
import { LayoutModule } from '../../../layout/layout.module';
import { BaseModule } from 'src/app/components/base/base.module';
import { RouterModule, Routes } from '@angular/router';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { MatLegacyTableModule as MatTableModule } from '@angular/material/legacy-table';
import { MatLegacyPaginatorModule as MatPaginatorModule } from '@angular/material/legacy-paginator';
import { MatSortModule } from '@angular/material/sort';
import { MatLegacyMenuModule as MatMenuModule } from '@angular/material/legacy-menu';
import { FlyPanelFilterTrainingIssuesModule } from '../fly-panel-filter-training-issues/fly-panel-filter-training-issues.module';
import { FlyPanelTrainingIssuesPendingActionItemModule } from '../training-issues-pending-action-item/fly-panel-training-issues-pending-action-item.module';

const routes: Routes = [
  {
    path: '',
    component: TrainingIssuesOverviewComponent,
  }
];

@NgModule({
  declarations: [TrainingIssuesOverviewComponent],
  imports: [
    CommonModule,
    LayoutModule,
    BaseModule,
    RouterModule.forChild(routes),
    MatCheckboxModule,
    MatTableModule,
    MatPaginatorModule,
    MatSortModule,
    MatMenuModule,
    FlyPanelFilterTrainingIssuesModule,
    FlyPanelTrainingIssuesPendingActionItemModule
  ],
  exports:[TrainingIssuesOverviewComponent]
})
export class TrainingIssuesOverviewModule { }

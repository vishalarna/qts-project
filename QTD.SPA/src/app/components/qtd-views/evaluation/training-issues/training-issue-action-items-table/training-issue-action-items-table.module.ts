import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LayoutModule } from '../../../layout/layout.module';
import { BaseModule } from 'src/app/components/base/base.module';
import { RouterModule, Routes } from '@angular/router';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { MatLegacyTableModule as MatTableModule } from '@angular/material/legacy-table';
import { MatLegacyPaginatorModule as MatPaginatorModule } from '@angular/material/legacy-paginator';
import { MatSortModule } from '@angular/material/sort';
import { MatLegacyMenuModule as MatMenuModule } from '@angular/material/legacy-menu';
import { MatToolbarModule } from '@angular/material/toolbar';
import { FlypanelAddTrainingIssueActionItemModule } from '../flypanel-add-training-issue-action-item/flypanel-add-training-issue-action-item.module';
import { MatLegacySelectModule as MatSelectModule } from '@angular/material/legacy-select';
import { DragDropModule } from '@angular/cdk/drag-drop';
import { MatIconModule } from '@angular/material/icon';
import { TrainingIssueActionItemsTableComponent } from './training-issue-action-items-table.component';
import { FlypanelFilterTrainingIssueActionItemsModule } from '../training-issue-view-action-items/flypanel-filter-training-issue-action-items/flypanel-filter-training-issue-action-items.module';


@NgModule({
  declarations: [TrainingIssueActionItemsTableComponent],
  imports: [
    CommonModule,
    LayoutModule,
    BaseModule,
    MatCheckboxModule,
    MatTableModule,
    MatPaginatorModule,
    MatSortModule,
    MatMenuModule,
    MatToolbarModule,
    FlypanelFilterTrainingIssueActionItemsModule,
    FlypanelAddTrainingIssueActionItemModule,
    MatSelectModule,
    DragDropModule,
    MatIconModule
  ],
  exports:[TrainingIssueActionItemsTableComponent]
})
export class TrainingIssueActionItemsTableModule { }

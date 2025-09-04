import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LayoutModule } from '../../../layout/layout.module';
import { BaseModule } from 'src/app/components/base/base.module';
import { RouterModule, Routes } from '@angular/router';
import { TrainingIssueViewActionItemsComponent } from './training-issue-view-action-items.component';
import { MatToolbarModule } from '@angular/material/toolbar';
import { TrainingIssueActionItemsTableModule } from '../training-issue-action-items-table/training-issue-action-items-table.module';


const routes: Routes = [
  {
    path: '',
    component: TrainingIssueViewActionItemsComponent,
  }
];

@NgModule({
  declarations: [TrainingIssueViewActionItemsComponent],
  imports: [
    CommonModule,
    LayoutModule,
    BaseModule,
    RouterModule.forChild(routes),
    MatToolbarModule,
    TrainingIssueActionItemsTableModule
  ],
  exports:[TrainingIssueViewActionItemsComponent]
})
export class TrainingIssueViewActionItemsModule { }

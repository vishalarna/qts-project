import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BaseModule } from 'src/app/components/base/base.module';
import { LayoutModule } from '@angular/cdk/layout';
import { MatLegacySelectModule as MatSelectModule } from '@angular/material/legacy-select';
import { TrainingIssuesActionItemsComponent } from './training-issues-action-items.component';
import { TrainingIssueActionItemsTableModule } from '../../training-issue-action-items-table/training-issue-action-items-table.module';
import { CKEditorModule } from '@ckeditor/ckeditor5-angular';


@NgModule({
  declarations: [
    TrainingIssuesActionItemsComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    BaseModule,
    ReactiveFormsModule,
    LayoutModule,
    MatSelectModule,
    TrainingIssueActionItemsTableModule,
    CKEditorModule
  ],
  exports:[TrainingIssuesActionItemsComponent]
})
export class TrainingIssuesActionItemsModule { }

import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BaseModule } from 'src/app/components/base/base.module';
import { LayoutModule } from '@angular/cdk/layout';
import { MatLegacySelectModule as MatSelectModule } from '@angular/material/legacy-select';
import { TrainingIssuesReviewAndPublishComponent } from './training-issues-review-and-publish.component';
import { MatLegacyTableModule as MatTableModule } from '@angular/material/legacy-table';
import { MatSortModule } from '@angular/material/sort';
import { MatLegacyPaginatorModule as MatPaginatorModule } from '@angular/material/legacy-paginator';

@NgModule({
  declarations: [
    TrainingIssuesReviewAndPublishComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    BaseModule,
    ReactiveFormsModule,
    LayoutModule,
    MatSelectModule,
    MatTableModule,
    MatSortModule,
    MatPaginatorModule
  ],
  exports:[TrainingIssuesReviewAndPublishComponent]
})
export class TrainingIssuesReviewAndPublishModule { }

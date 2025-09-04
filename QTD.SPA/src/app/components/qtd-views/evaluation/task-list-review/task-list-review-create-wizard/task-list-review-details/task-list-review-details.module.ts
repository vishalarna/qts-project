import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TaskListReviewDetailsComponent } from './task-list-review-details.component';
import { BaseModule } from 'src/app/components/base/base.module';
import { LayoutModule } from '@angular/cdk/layout';
import { MatLegacySelectModule as MatSelectModule } from '@angular/material/legacy-select';
import { FlyPanelAddReviewersModule } from './fly-panel-add-reviewers/fly-panel-add-reviewers.module';
import { MatExpansionModule } from '@angular/material/expansion';
import { MatLegacyMenuModule as MatMenuModule } from '@angular/material/legacy-menu';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { MatIconModule } from '@angular/material/icon';
import { MatTreeModule } from '@angular/material/tree';
import { MatLegacyTableModule as MatTableModule } from '@angular/material/legacy-table';
import { ReactiveFormsModule } from '@angular/forms';
import { MatSortModule } from '@angular/material/sort';
import { MatLegacyFormFieldModule as MatFormFieldModule } from '@angular/material/legacy-form-field';
import { MatLegacyChipsModule as MatChipsModule } from '@angular/material/legacy-chips';



@NgModule({
  declarations: [TaskListReviewDetailsComponent],
  imports: [
    CommonModule,
    BaseModule,
    LayoutModule,
    MatSelectModule,
    ReactiveFormsModule,
    FlyPanelAddReviewersModule,
    MatExpansionModule,
    MatMenuModule,
    MatCheckboxModule,
    MatIconModule,
    MatTreeModule,
    MatTableModule,
    MatSortModule,
    MatFormFieldModule,
    MatChipsModule
  ],
  exports:[TaskListReviewDetailsComponent]
})
export class TaskListReviewDetailsModule { }
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TaskListReviewCreateWizardComponent } from './task-list-review-create-wizard.component';
import { RouterModule, Routes } from '@angular/router';
import { BaseModule } from 'src/app/components/base/base.module';
import { LayoutModule } from '../../../layout/layout.module';
import { MatToolbarModule } from '@angular/material/toolbar';
import { ActionItemsModule } from './action-items/action-items.module';
import { ConclusionAndTrainingModule } from './conclusion-and-training/conclusion-and-training.module';
import { SupportingDocumentsModule } from './supporting-documents/supporting-documents.module';
import { TaskListReviewDetailsModule } from './task-list-review-details/task-list-review-details.module';
import { MatStepperModule } from '@angular/material/stepper';
import { TaskListReviewTasksModule } from './task-list-review-tasks/task-list-review-tasks.module';
import { TaskListReviewGenerateReportModule } from '../task-list-review-generate-report/task-list-review-generate-report.module';

const routes: Routes = [
  {
    path: '',
    component: TaskListReviewCreateWizardComponent,
  },
];

@NgModule({
  declarations: [TaskListReviewCreateWizardComponent],
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    BaseModule,
    LayoutModule,
    MatToolbarModule,
    ActionItemsModule,
    ConclusionAndTrainingModule,
    SupportingDocumentsModule,
    TaskListReviewDetailsModule,
    MatStepperModule,
    TaskListReviewTasksModule,
    TaskListReviewGenerateReportModule
  ]
})
export class TaskListReviewCreateWizardModule { }

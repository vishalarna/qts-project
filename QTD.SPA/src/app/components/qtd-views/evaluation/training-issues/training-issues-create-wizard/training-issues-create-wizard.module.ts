import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TrainingIssuesCreateWizardComponent } from './training-issues-create-wizard.component';
import { BaseModule } from 'src/app/components/base/base.module';
import { LayoutModule } from '../../../layout/layout.module';
import { RouterModule, Routes } from '@angular/router';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatStepperModule } from '@angular/material/stepper';
import { TrainingIssuesDetailsModule } from './training-issues-details/training-issues-details.module';
import { TrainingIssuesDriversAndTrainingModule } from './training-issues-drivers-and-training/training-issues-drivers-and-training.module';
import { TrainingIssuesActionItemsModule } from './training-issues-action-items/training-issues-action-items.module';
import { TrainingIssuesReviewAndPublishModule } from './training-issues-review-and-publish/training-issues-review-and-publish.module';

const routes: Routes = [
  {
    path: '',
    component: TrainingIssuesCreateWizardComponent,
  },
];

@NgModule({
  declarations: [TrainingIssuesCreateWizardComponent],
  imports: [
    CommonModule,
    BaseModule,
    LayoutModule,
    RouterModule.forChild(routes),
    MatToolbarModule,
    MatStepperModule,
    TrainingIssuesDetailsModule,
    TrainingIssuesDriversAndTrainingModule,
    TrainingIssuesActionItemsModule,
    TrainingIssuesReviewAndPublishModule
  ]
})
export class TrainingIssuesCreateWizardModule { }

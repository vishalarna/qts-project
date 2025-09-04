import { FormsModule } from '@angular/forms';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TrainingProgramReviewWizardComponent } from './trainingprogramreview-wizard.component';
import { RouterModule, Routes } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';
import { LocalizeModule } from 'src/app/_Shared/modules/localize.module';
import { BaseModule } from 'src/app/components/base/base.module';
import { LayoutModule } from '../../../layout/layout.module';
import { MatStepperModule } from '@angular/material/stepper';
import { MatLegacyDialogModule as MatDialogModule } from '@angular/material/legacy-dialog';
import { DragDropModule } from '@angular/cdk/drag-drop';
import {MatLegacyTooltipModule as MatTooltipModule} from '@angular/material/legacy-tooltip';
import { CKEditorModule } from '@ckeditor/ckeditor5-angular';
import { MatToolbarModule } from '@angular/material/toolbar';
import { CreateNewProgramReviewModule } from './trainingprogramreview-wizard-components/create-new-program-review/create-new-program-review.module';
import { PurposeOfReviewModule } from './trainingprogramreview-wizard-components/purpose-of-review/purpose-of-review.module';
import { ReviewDesignAndDevelopmentModule } from './trainingprogramreview-wizard-components/review-design-and-development/review-design-and-development.module';
import { ReviewEvaluationModule } from './trainingprogramreview-wizard-components/review-evaluation/review-evaluation.module';
import { SupportingDocumentsAndIssuesModule } from './trainingprogramreview-wizard-components/supporting-documents-and-issues/supporting-documents-and-issues.module';
import { ConclusionAndActionItemsModule } from './trainingprogramreview-wizard-components/conclusion-and-action-items/conclusion-and-action-items.module';
import { TrainingDepartmentSignOffModule } from './trainingprogramreview-wizard-components/training-department-sign-off/training-department-sign-off.module';
import { DialogueShareReportModule } from './trainingprogramreview-wizard-components/dialogue-share-report/dialogue-share-report.module';

const routes: Routes = [
  // {
  //     path: '',
  //     component: TrainingProgramReviewWizardComponent,
  
  // },
  {
    path: 'create',
    component: TrainingProgramReviewWizardComponent,

  },
];

@NgModule({
  declarations: [TrainingProgramReviewWizardComponent],
  imports: [
    CommonModule,
    HttpClientModule,
    RouterModule.forChild(routes),
    LocalizeModule,
    BaseModule,
    LayoutModule,
    MatDialogModule,
    MatStepperModule,
    DragDropModule,
    MatTooltipModule,
    CKEditorModule,
    FormsModule,
    FormsModule,
    MatToolbarModule,
    CreateNewProgramReviewModule,
    PurposeOfReviewModule,
    ReviewDesignAndDevelopmentModule,
    ReviewEvaluationModule,
    SupportingDocumentsAndIssuesModule,
    ConclusionAndActionItemsModule,
    TrainingDepartmentSignOffModule,
    DialogueShareReportModule
  ],
  exports:[TrainingProgramReviewWizardComponent]
})
export class TrainingProgramReviewWizardModule {}

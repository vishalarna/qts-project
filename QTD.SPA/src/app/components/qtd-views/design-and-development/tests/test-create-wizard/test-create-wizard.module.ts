import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TestCreateWizardComponent } from './test-create-wizard.component';
import { MatToolbarModule } from '@angular/material/toolbar';
import { LayoutModule } from '../../../layout/layout.module';
import { BaseModule } from 'src/app/components/base/base.module';
import { RouterModule, Routes } from '@angular/router';
import { MatStepperModule } from '@angular/material/stepper';
import { CreateTestInformationModule } from './test-wizard-components/create-test-information/create-test-information.module';
import { ImportTestQuestionsModule } from './test-wizard-components/import-test-questions/import-test-questions.module';
import { AddNewTestQuestionsComponent } from './test-wizard-components/add-new-test-questions/add-new-test-questions.component';
import { AddNewTestQuestionsModule } from './test-wizard-components/add-new-test-questions/add-new-test-questions.module';
import { PreviewAndPublishModule } from './test-wizard-components/preview-and-publish/preview-and-publish.module';
import { SequenceTestQuestionsComponent } from './test-wizard-components/sequence-test-questions/sequence-test-questions.component';
import { SequenceTestQuestionsModule } from './test-wizard-components/sequence-test-questions/sequence-test-questions.module';
import { MatIconModule } from '@angular/material/icon';

const routes: Routes = [
  {
    path: '',
    component: TestCreateWizardComponent,
  },
  {
    path: ':id',
    component: TestCreateWizardComponent,
  },
  {
    path: ':id/:sI',
    component: TestCreateWizardComponent
  }
];

@NgModule({
  declarations: [TestCreateWizardComponent],
  imports: [
    CommonModule,
    MatToolbarModule,
    LayoutModule,
    BaseModule,
    RouterModule.forChild(routes),
    MatStepperModule,
    CreateTestInformationModule,
    ImportTestQuestionsModule,
    AddNewTestQuestionsModule,
    SequenceTestQuestionsModule,
    MatIconModule,
  ],
  exports: [TestCreateWizardComponent]
})
export class TestCreateWizardModule { }

import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FlyPanelEditEvaluationComponent } from './fly-panel-edit-evaluation.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { MatLegacyFormFieldModule as MatFormFieldModule } from '@angular/material/legacy-form-field';
import { MatLegacySelectModule as MatSelectModule } from '@angular/material/legacy-select';
import { MatStepperModule } from '@angular/material/stepper';
import { BaseModule } from 'src/app/components/base/base.module';
import { CKEditorModule } from '@ckeditor/ckeditor5-angular';
import { MatLegacyTableModule as MatTableModule } from '@angular/material/legacy-table';
import { FlyPanelAddQuestionModule } from '../student-evaluation-question-bank/fly-panel-add-question/fly-panel-add-question.module';
import { FlyPanelImportExistingQuestionsModule } from '../add-new-student-evaluation/fly-panel-import-existing-questions/fly-panel-import-existing-questions.module';


@NgModule({
  declarations: [
    FlyPanelEditEvaluationComponent
  ],
  imports: [
    CommonModule,
    BaseModule,
    FormsModule,
    ReactiveFormsModule,
    MatCheckboxModule,
    MatFormFieldModule,
    MatSelectModule,
    MatStepperModule,
    CKEditorModule,
    MatTableModule,
    FlyPanelAddQuestionModule,
    FlyPanelImportExistingQuestionsModule
  ],
  exports:[FlyPanelEditEvaluationComponent]
})
export class FlyPanelEditEvaluationModule { }

import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FlypanelPreviewTestQuestionsComponent } from './flypanel-preview-test-questions.component';
import { LayoutModule } from '../../../layout/layout.module';
import { BaseModule } from 'src/app/components/base/base.module';
import { ReactiveFormsModule } from '@angular/forms';
import { MatIconModule } from '@angular/material/icon';
import { MatLegacyTooltipModule as MatTooltipModule } from '@angular/material/legacy-tooltip';
import { FlypanelAddEoModule } from '../flypanel-add-eo/flypanel-add-eo.module';
import { AddFillInTheBlankModule } from '../test-question-bank/add-fill-in-the-blank/add-fill-in-the-blank.module';
import { AddMatchTheColumnModule } from '../test-question-bank/add-match-the-column/add-match-the-column.module';
import { AddMcqModule } from '../test-question-bank/add-mcq/add-mcq.module';
import { AddMultipleCorrectAnswerModule } from '../test-question-bank/add-multiple-correct-answer/add-multiple-correct-answer.module';
import { AddShortQuestionsModule } from '../test-question-bank/add-short-questions/add-short-questions.module';
import { AddTrueFalseModule } from '../test-question-bank/add-true-false/add-true-false.module';
import { MatLegacySelectModule as MatSelectModule } from '@angular/material/legacy-select';



@NgModule({
  declarations: [FlypanelPreviewTestQuestionsComponent],
  imports: [
    CommonModule,
    LayoutModule,
    BaseModule,
    MatSelectModule,
    ReactiveFormsModule,
    AddTrueFalseModule,
    AddShortQuestionsModule,
    AddMcqModule,
    AddFillInTheBlankModule,
    AddMultipleCorrectAnswerModule,
    AddMatchTheColumnModule,
    MatTooltipModule,
    FlypanelAddEoModule,
    MatIconModule,
  ],
  exports: [FlypanelPreviewTestQuestionsComponent]
})
export class FlypanelPreviewTestQuestionsModule { }

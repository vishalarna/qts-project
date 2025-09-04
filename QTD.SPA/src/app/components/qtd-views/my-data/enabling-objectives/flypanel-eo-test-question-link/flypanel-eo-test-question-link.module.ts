import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FlypanelEoTestQuestionLinkComponent } from './flypanel-eo-test-question-link.component';
import { BaseModule } from 'src/app/components/base/base.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatStepperModule } from '@angular/material/stepper';
import { MatLegacySelectModule as MatSelectModule } from '@angular/material/legacy-select';
import { McqsTestModule } from '../../../design-and-development/providers-and-ila/ila-create-wizard/ila-wizard-components/trainee-evaluation/mcqs-test/mcqs-test.module';
import { FillInTheBlanksModule } from '../../../design-and-development/providers-and-ila/ila-create-wizard/ila-wizard-components/trainee-evaluation/fill-in-the-blanks/fill-in-the-blanks.module';
import { ShortAnswersModule } from '../../../design-and-development/providers-and-ila/ila-create-wizard/ila-wizard-components/trainee-evaluation/short-answers/short-answers.module';
import { MatchTheColumnModule } from '../../../design-and-development/providers-and-ila/ila-create-wizard/ila-wizard-components/trainee-evaluation/match-the-column/match-the-column.module';
import { TrueFalseModule } from '../../../design-and-development/providers-and-ila/ila-create-wizard/ila-wizard-components/trainee-evaluation/true-false/true-false.module';
import { MatLegacyTooltipModule as MatTooltipModule } from '@angular/material/legacy-tooltip';
import { AddTrueFalseModule } from '../../../design-and-development/tests/test-question-bank/add-true-false/add-true-false.module';
import { AddShortQuestionsModule } from '../../../design-and-development/tests/test-question-bank/add-short-questions/add-short-questions.module';
import { AddMcqModule } from '../../../design-and-development/tests/test-question-bank/add-mcq/add-mcq.module';
import { AddMultipleCorrectAnswerModule } from '../../../design-and-development/tests/test-question-bank/add-multiple-correct-answer/add-multiple-correct-answer.module';
import { AddMatchTheColumnModule } from '../../../design-and-development/tests/test-question-bank/add-match-the-column/add-match-the-column.module';
import { AddFillInTheBlankModule } from '../../../design-and-development/tests/test-question-bank/add-fill-in-the-blank/add-fill-in-the-blank.module';



@NgModule({
  declarations: [
    FlypanelEoTestQuestionLinkComponent
  ],
  imports: [
    CommonModule,
    BaseModule,
    FormsModule,
    ReactiveFormsModule,
    MatStepperModule,
    MatSelectModule,
    AddMcqModule,
    AddFillInTheBlankModule,
    AddShortQuestionsModule,
    AddMatchTheColumnModule,
    AddTrueFalseModule,
    MatTooltipModule,
    AddMultipleCorrectAnswerModule,
  ],
  exports : [
    FlypanelEoTestQuestionLinkComponent
  ]
})
export class FlypanelEoTestQuestionLinkModule { }

import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FlypanelAddTestItemComponent } from './flypanel-add-test-item.component';
import { BaseModule } from 'src/app/components/base/base.module';
import { MatLegacySelectModule as MatSelectModule } from '@angular/material/legacy-select';
import { AddTrueFalseModule } from '../add-true-false/add-true-false.module';
import { AddShortQuestionsModule } from '../add-short-questions/add-short-questions.module';
import { AddMcqModule } from '../add-mcq/add-mcq.module';
import { AddFillInTheBlankModule } from '../add-fill-in-the-blank/add-fill-in-the-blank.module';
import { AddMultipleCorrectAnswerModule } from '../add-multiple-correct-answer/add-multiple-correct-answer.module';
import { AddMatchTheColumnModule } from '../add-match-the-column/add-match-the-column.module';
import { ReactiveFormsModule } from '@angular/forms';
import { FlypanelAddEoModule } from '../../flypanel-add-eo/flypanel-add-eo.module';
import { MatLegacyTooltipModule as MatTooltipModule } from '@angular/material/legacy-tooltip';
import { MatIconModule } from '@angular/material/icon';



@NgModule({
  declarations: [
    FlypanelAddTestItemComponent
  ],
  imports: [
    CommonModule,
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
  exports : [
    FlypanelAddTestItemComponent,
  ]
})
export class FlypanelAddTestItemModule { }

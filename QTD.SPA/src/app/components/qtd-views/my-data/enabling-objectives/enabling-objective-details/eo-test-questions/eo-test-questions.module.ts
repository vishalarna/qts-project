import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { EoTestQuestionsComponent } from './eo-test-questions.component';
import { FlypanelEoTestQuestionLinkModule } from '../../flypanel-eo-test-question-link/flypanel-eo-test-question-link.module';
import { BaseModule } from 'src/app/components/base/base.module';
import { MatLegacyTableModule as MatTableModule } from '@angular/material/legacy-table';
import { MatLegacyPaginatorModule as MatPaginatorModule } from '@angular/material/legacy-paginator';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { FlypanelLinkedEosModule } from '../../flypanel-linked-eos/flypanel-linked-eos.module';
import { MatSortModule } from '@angular/material/sort';
import { MatLegacyTooltipModule as MatTooltipModule } from '@angular/material/legacy-tooltip';


@NgModule({
  declarations: [
    EoTestQuestionsComponent
  ],
  imports: [
    CommonModule,
    BaseModule,
    MatTableModule,
    MatPaginatorModule,
    MatCheckboxModule,
    FlypanelEoTestQuestionLinkModule,
    FlypanelLinkedEosModule,
    MatSortModule,
    MatTooltipModule,
  ],
  exports : [EoTestQuestionsComponent]
})
export class EoTestQuestionsModule { }

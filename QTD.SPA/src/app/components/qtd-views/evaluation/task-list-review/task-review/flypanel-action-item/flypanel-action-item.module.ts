import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatLegacyTableModule as MatTableModule } from '@angular/material/legacy-table';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BaseModule } from 'src/app/components/base/base.module';
import { MatLegacyMenuModule as MatMenuModule } from '@angular/material/legacy-menu';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatLegacySelectModule as MatSelectModule } from '@angular/material/legacy-select';
import { MatLegacyRadioModule as MatRadioModule } from '@angular/material/legacy-radio';
import { LayoutModule } from '@angular/cdk/layout';
import { FlypanelActionItemComponent } from './flypanel-action-item.component';
import { FlypanelSubdutyOperationModule } from '../flypanel-subduty-operation/flypanel-subduty-operation.module';
import { FlypanelStepOperationModule } from '../flypanel-step-operation/flypanel-step-operation.module';
import { FlypanelQuestionAndAnswerOperationModule } from '../flypanel-question-and-answer-operation/flypanel-question-and-answer-operation.module';
import { FlypanelSuggestionOperationModule } from '../flypanel-suggestion-operation/flypanel-suggestion-operation.module';
import { FlypanelToolOperationModule } from '../flypanel-tool-operation/flypanel-tool-operation.module';
import { FlypanelEnablingObjectiveOperationModule } from '../flypanel-enabling-objective-operation/flypanel-enabling-objective-operation.module';
import { FlypanelProcedureOperationModule } from '../flypanel-procedure-operation/flypanel-procedure-operation.module';
import { FlypanelRegulatoryRequirementOperationModule } from '../flypanel-regulatory-requirement-operation/flypanel-regulatory-requirement-operation.module';
import { FlypanelSafetyHazardOperationModule } from '../flypanel-safety-hazard-operation/flypanel-safety-hazard-operation.module';
import { MatIconModule } from '@angular/material/icon';
import { CKEditorModule } from '@ckeditor/ckeditor5-angular';


@NgModule({
  declarations: [FlypanelActionItemComponent],
  imports: [
    CommonModule,
    MatTableModule,
    FormsModule,
    LayoutModule,
    BaseModule,
    MatMenuModule,
    MatCheckboxModule,
    MatToolbarModule,
    MatSelectModule,
    MatRadioModule,
    ReactiveFormsModule,
    FlypanelSubdutyOperationModule,
    FlypanelStepOperationModule,
    FlypanelQuestionAndAnswerOperationModule,
    FlypanelSuggestionOperationModule,
    FlypanelToolOperationModule,
    FlypanelEnablingObjectiveOperationModule,
    FlypanelProcedureOperationModule,
    FlypanelRegulatoryRequirementOperationModule,
    FlypanelSafetyHazardOperationModule,
    MatIconModule,
    CKEditorModule,
  ],
  exports:[FlypanelActionItemComponent]
})
export class FlypanelActionItemModule { }

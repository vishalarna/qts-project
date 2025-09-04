import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TaskOjtGuideComponent } from './task-ojt-guide.component';
import { BaseModule } from 'src/app/components/base/base.module';
import { MatLegacyMenuModule as MatMenuModule } from '@angular/material/legacy-menu';
import { FlyPanelAddQuestionModule } from '../../fly-panel-add-question/fly-panel-add-question.module';
import { FlyPanelAddStepModule } from '../../fly-panel-add-step/fly-panel-add-step.module';
import { FlyPanelAddSuggestionModule } from '../../fly-panel-add-suggestion/fly-panel-add-suggestion.module';
import { FlyPanelEditConditionModule } from '../../fly-panel-edit-condition/fly-panel-edit-condition.module';
import { FlyPanelEditCriteriaModule } from '../../fly-panel-edit-criteria/fly-panel-edit-criteria.module';
import { FlyPanelEditReferencesModule } from '../../fly-panel-edit-references/fly-panel-edit-references.module';
import { FlyPanelEditToolsModule } from '../../fly-panel-edit-tools/fly-panel-edit-tools.module';
import { MatLegacyTableModule as MatTableModule } from '@angular/material/legacy-table';
import { MatLegacyPaginatorModule as MatPaginatorModule } from '@angular/material/legacy-paginator';
import { DragDropModule } from '@angular/cdk/drag-drop';
import { FlyPanelEditTaskTrainingGroupModule } from '../../fly-panel-edit-task-training-group/fly-panel-edit-task-training-group.module';



@NgModule({
  declarations: [
    TaskOjtGuideComponent
  ],
  imports: [
    CommonModule,
    BaseModule,
    MatMenuModule,
    MatTableModule,
    MatPaginatorModule,
    DragDropModule,
    FlyPanelAddStepModule,
    FlyPanelAddQuestionModule,
    FlyPanelAddSuggestionModule,
    FlyPanelEditConditionModule,
    FlyPanelEditCriteriaModule,
    FlyPanelEditReferencesModule,
    FlyPanelEditToolsModule,
    FlyPanelEditTaskTrainingGroupModule,
  ],
  exports: [
    TaskOjtGuideComponent
  ]
})
export class TaskOjtGuideModule { }

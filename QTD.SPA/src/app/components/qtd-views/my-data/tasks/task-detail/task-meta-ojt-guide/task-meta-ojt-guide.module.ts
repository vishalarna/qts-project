import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TaskMetaOjtGuideComponent } from './task-meta-ojt-guide.component';
import { BaseModule } from 'src/app/components/base/base.module';
import { FlyPanelAddStepModule } from '../../fly-panel-add-step/fly-panel-add-step.module';
import { FlyPanelAddSuggestionModule } from '../../fly-panel-add-suggestion/fly-panel-add-suggestion.module';
import { FlyPanelAddQuestionModule } from '../../fly-panel-add-question/fly-panel-add-question.module';
import { DragDropModule } from '@angular/cdk/drag-drop';
import { MatLegacyMenuModule as MatMenuModule } from '@angular/material/legacy-menu';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { MatLegacyTableModule as MatTableModule } from '@angular/material/legacy-table';
import { MatLegacyDialogModule as MatDialogModule } from '@angular/material/legacy-dialog';
import { MatLegacyPaginatorModule as MatPaginatorModule } from '@angular/material/legacy-paginator';



@NgModule({
  declarations: [
    TaskMetaOjtGuideComponent
  ],
  imports: [
    CommonModule,
    BaseModule,
    FlyPanelAddStepModule,
    FlyPanelAddSuggestionModule,
    FlyPanelAddQuestionModule,
    DragDropModule,
    MatMenuModule,
    MatCheckboxModule,
    MatTableModule,
    MatDialogModule,
    MatPaginatorModule,
  ],
  exports:[
    TaskMetaOjtGuideComponent,
  ]
})
export class TaskMetaOjtGuideModule { }

import { FlyPanelLinkScenarioObjectiveModule } from './../trainee-evaluation/fly-panel-link-scenario-objective/fly-panel-link-scenario-objective.module';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TrainingMaterialComponent } from './training-material.component';
import { BaseModule } from 'src/app/components/base/base.module';
import { LayoutModule } from 'src/app/components/qtd-views/layout/layout.module';
import { FormsModule } from '@angular/forms';
import { MatLegacySelectModule as MatSelectModule } from '@angular/material/legacy-select';
import { FlyPanelLessonSegmentModule } from './fly-panel-lesson-segment/fly-panel-lesson-segment.module';
import { FlyPanelContentModule } from './fly-panel-content/fly-panel-content.module';
import { DragDropModule } from '@angular/cdk/drag-drop';
import { NgxDropzoneModule } from 'ngx-dropzone';
import { MatLegacyDialogModule as MatDialogModule } from '@angular/material/legacy-dialog';
import { DeleteSegmentComponent } from './delete-segment/delete-segment.component';
import { UploadDialogueComponent } from './upload-dialogue/upload-dialogue.component';
import { MatSortModule } from '@angular/material/sort';
import { MatLegacyTableModule as MatTableModule } from '@angular/material/legacy-table';
import { FlyPanelTrainingMaterialModule } from './fly-panel-training-material/fly-panel-training-material.module';
import { MatIconModule } from '@angular/material/icon';

@NgModule({

  declarations: [
    TrainingMaterialComponent,
    DeleteSegmentComponent,
    UploadDialogueComponent
  ],
  imports: [
    CommonModule,
    BaseModule,
    LayoutModule,
    FormsModule,
    MatSelectModule,
    FlyPanelLessonSegmentModule,
    FlyPanelContentModule,
    DragDropModule,
    NgxDropzoneModule,
    MatDialogModule,
    FlyPanelLinkScenarioObjectiveModule,
    MatSortModule,
    MatTableModule,
    FlyPanelTrainingMaterialModule,
    MatIconModule
  ],
  exports:[
    TrainingMaterialComponent
  ]
})

export class TrainingMaterialModule { }

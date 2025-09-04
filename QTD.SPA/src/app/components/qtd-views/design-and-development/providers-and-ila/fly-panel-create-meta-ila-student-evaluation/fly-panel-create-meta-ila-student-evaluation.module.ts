import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';
import { LocalizeModule } from 'src/app/_Shared/modules/localize.module';
import { BaseModule } from 'src/app/components/base/base.module';
import { LayoutModule } from '../../../layout/layout.module';
import { MatStepperModule } from '@angular/material/stepper';
import { MatLegacyDialogModule as MatDialogModule } from '@angular/material/legacy-dialog';
import { DragDropModule } from '@angular/cdk/drag-drop';
import {MatLegacyTooltipModule as MatTooltipModule} from '@angular/material/legacy-tooltip';
import { CKEditorModule } from '@ckeditor/ckeditor5-angular';
import { MatToolbarModule } from '@angular/material/toolbar';
import { FlyPanelCreateMetaIlaStudentEvaluationComponent } from './fly-panel-create-meta-ila-student-evaluation.component';
import { MatLegacyFormFieldModule as MatFormFieldModule } from '@angular/material/legacy-form-field';
import { MatLegacyOptionModule as MatOptionModule } from '@angular/material/legacy-core';
import { MatLegacyTableModule as MatTableModule } from '@angular/material/legacy-table';
import { FlyPanelPublishStudentEvaluationModule } from '../../../evaluation/student-evaluation/fly-panel-publish-student-evaluation/fly-panel-publish-student-evaluation.module';
import { FlyPanelImportExistingQuestionsModule } from '../../../evaluation/student-evaluation/add-new-student-evaluation/fly-panel-import-existing-questions/fly-panel-import-existing-questions.module';
import { FlyPanelAddQuestionModule } from '../../../evaluation/student-evaluation/student-evaluation-question-bank/fly-panel-add-question/fly-panel-add-question.module';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { MatLegacySelectModule as MatSelectModule } from '@angular/material/legacy-select';

const routes: Routes = [
];

@NgModule({
  declarations: [FlyPanelCreateMetaIlaStudentEvaluationComponent],
  imports: [
    CommonModule,
    HttpClientModule,
    RouterModule.forChild(routes),
    LocalizeModule,
    BaseModule,
    LayoutModule,
    MatDialogModule,
    MatStepperModule,
    DragDropModule,
    MatTooltipModule,
    CKEditorModule,
    FormsModule,
    FormsModule,
    MatToolbarModule,
    MatFormFieldModule,
    MatOptionModule,
    MatTableModule,
    FlyPanelPublishStudentEvaluationModule,
    FlyPanelImportExistingQuestionsModule,
    FlyPanelAddQuestionModule,
    ReactiveFormsModule,
    MatCheckboxModule,
    MatSelectModule
  ],
  exports:[FlyPanelCreateMetaIlaStudentEvaluationComponent]
})
export class FlyPanelCreateMetaILAStudentEvaluationModule {}

import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AddNewStudentEvaluationComponent } from './add-new-student-evaluation.component';
import { RouterModule, Routes } from '@angular/router';
import { LayoutModule } from '../../../layout/layout.module';
import { MatLegacyPaginatorModule as MatPaginatorModule } from '@angular/material/legacy-paginator';
import { MatLegacyTableModule as MatTableModule } from '@angular/material/legacy-table';
import { BaseModule } from 'src/app/components/base/base.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CKEditorModule } from '@ckeditor/ckeditor5-angular';
import { MatLegacySelectModule as MatSelectModule } from '@angular/material/legacy-select';
import { MatStepperModule } from '@angular/material/stepper';
import { MatToolbarModule } from '@angular/material/toolbar';
import { FlyPanelAddQuestionModule } from '../student-evaluation-question-bank/fly-panel-add-question/fly-panel-add-question.module';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { FlyPanelImportExistingQuestionsModule } from './fly-panel-import-existing-questions/fly-panel-import-existing-questions.module';
import { FlyPanelPublishStudentEvaluationModule } from '../fly-panel-publish-student-evaluation/fly-panel-publish-student-evaluation.module';
import { DragDropModule } from '@angular/cdk/drag-drop';
import { MatSortModule } from '@angular/material/sort';

const routes: Routes = [
  {
    path: '',
    pathMatch:'full',
    component: AddNewStudentEvaluationComponent,
  }, 
];


@NgModule({
  declarations: [
    AddNewStudentEvaluationComponent,
  ],
  imports: [
    CommonModule,
    MatTableModule,
    MatPaginatorModule,
    BaseModule,
    LayoutModule,
    MatTableModule,
    RouterModule.forChild(routes),
    FormsModule,
    ReactiveFormsModule,
    CKEditorModule,
    MatStepperModule,
    MatSelectModule,
    MatToolbarModule,
    FlyPanelAddQuestionModule,
    MatCheckboxModule,
    FlyPanelImportExistingQuestionsModule,
    FlyPanelPublishStudentEvaluationModule,
    DragDropModule,
    MatSortModule
  ]
})
export class AddNewStudentEvaluationModule { }

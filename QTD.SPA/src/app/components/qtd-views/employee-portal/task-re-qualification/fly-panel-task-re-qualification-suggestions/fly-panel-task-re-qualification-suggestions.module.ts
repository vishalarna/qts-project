import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FlyPanelTaskReQualificationSuggestionsComponent } from './fly-panel-task-re-qualification-suggestions.component';
import { RouterModule, Routes } from '@angular/router';
import { MatLegacyTableModule as MatTableModule } from '@angular/material/legacy-table';
import { MatLegacyPaginatorModule as MatPaginatorModule } from '@angular/material/legacy-paginator';
import { BaseModule } from 'src/app/components/base/base.module';
import { LayoutModule } from '../../../layout/layout.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CKEditorModule } from '@ckeditor/ckeditor5-angular';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { MatLegacySelectModule as MatSelectModule } from '@angular/material/legacy-select';
import { MatStepperModule } from '@angular/material/stepper';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatLegacyRadioModule as MatRadioModule } from '@angular/material/legacy-radio';
import { FlyPanelTaskReQualificationTaskFeedbackModule } from '../fly-panel-task-re-qualification-task-feedback/fly-panel-task-re-qualification-task-feedback.module';

const routes: Routes = [
  {
    path: '',
    component: FlyPanelTaskReQualificationSuggestionsComponent,
  },
  {
    path: ':id',
    component: FlyPanelTaskReQualificationSuggestionsComponent,
  },
];

@NgModule({
  declarations: [
    FlyPanelTaskReQualificationSuggestionsComponent
  ],
  imports: [
    RouterModule.forChild(routes),
    CommonModule,
    MatPaginatorModule,
    BaseModule,
    LayoutModule,
    MatTableModule,
    FormsModule,
    ReactiveFormsModule,
    CKEditorModule,
    MatStepperModule,
    MatSelectModule,
    MatToolbarModule,
    MatCheckboxModule,
    MatRadioModule,
    FlyPanelTaskReQualificationTaskFeedbackModule
  ],
})
export class FlyPanelTaskReQualificationSuggestionsModule { }

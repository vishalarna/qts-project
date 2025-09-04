import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FlyPanelAddQuestionComponent } from './fly-panel-add-question.component';
import { BaseModule } from 'src/app/components/base/base.module';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { CKEditorModule } from '@ckeditor/ckeditor5-angular';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';



@NgModule({
  declarations: [
    FlyPanelAddQuestionComponent
  ],
  imports: [
    CommonModule,
    BaseModule,
    MatCheckboxModule,
    CKEditorModule,
    FormsModule,
    ReactiveFormsModule,
  ],
  exports : [
    FlyPanelAddQuestionComponent
  ]
})
export class FlyPanelAddQuestionModule { }

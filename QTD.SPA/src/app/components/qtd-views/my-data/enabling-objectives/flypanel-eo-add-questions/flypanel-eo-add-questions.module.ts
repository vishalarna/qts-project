import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FlypanelEoAddQuestionsComponent } from './flypanel-eo-add-questions.component';
import { BaseModule } from 'src/app/components/base/base.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CKEditorModule } from '@ckeditor/ckeditor5-angular';



@NgModule({
  declarations: [
    FlypanelEoAddQuestionsComponent
  ],
  imports: [
    CommonModule,
    BaseModule,
    ReactiveFormsModule,
    FormsModule,
    CKEditorModule,
  ],
  exports : [
    FlypanelEoAddQuestionsComponent,
  ]
})
export class FlypanelEoAddQuestionsModule { }

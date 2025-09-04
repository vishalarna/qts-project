import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AddMultipleCorrectAnswerComponent } from './add-multiple-correct-answer.component';
import { BaseModule } from 'src/app/components/base/base.module';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { CKEditorModule } from '@ckeditor/ckeditor5-angular';


@NgModule({
  declarations: [
    AddMultipleCorrectAnswerComponent
  ],
  imports: [
    CommonModule,
    BaseModule,
    MatCheckboxModule,
    ReactiveFormsModule,
    FormsModule,
    CKEditorModule
  ],
  exports : [
    AddMultipleCorrectAnswerComponent,
  ]
})
export class AddMultipleCorrectAnswerModule { }

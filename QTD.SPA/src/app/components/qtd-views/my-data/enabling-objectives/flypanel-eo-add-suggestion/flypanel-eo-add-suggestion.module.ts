import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FlypanelEoAddSuggestionComponent } from './flypanel-eo-add-suggestion.component';
import { BaseModule } from 'src/app/components/base/base.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CKEditorModule } from '@ckeditor/ckeditor5-angular';



@NgModule({
  declarations: [
    FlypanelEoAddSuggestionComponent
  ],
  imports: [
    CommonModule,
    BaseModule,
    FormsModule,
    ReactiveFormsModule,
    CKEditorModule,
  ],
  exports : [
    FlypanelEoAddSuggestionComponent
  ]
})
export class FlypanelEoAddSuggestionModule { }

import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FlyPanelAddSuggestionComponent } from './fly-panel-add-suggestion.component';
import { BaseModule } from 'src/app/components/base/base.module';
import { CKEditorModule } from '@ckeditor/ckeditor5-angular';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { FormsModule } from '@angular/forms';



@NgModule({
  declarations: [
    FlyPanelAddSuggestionComponent
  ],
  imports: [
    CommonModule,
    BaseModule,
    CKEditorModule,
    MatCheckboxModule,
    FormsModule,
  ],
  exports : [
    FlyPanelAddSuggestionComponent
  ]
})
export class FlyPanelAddSuggestionModule { }

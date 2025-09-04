import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FlyPanelEditConditionComponent } from './fly-panel-edit-condition.component';
import { CKEditorModule } from '@ckeditor/ckeditor5-angular';
import { BaseModule } from 'src/app/components/base/base.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';



@NgModule({
  declarations: [
    FlyPanelEditConditionComponent
  ],
  imports: [
    CommonModule,
    BaseModule,
    FormsModule,
    ReactiveFormsModule,
    CKEditorModule,
  ],
  exports : [
    FlyPanelEditConditionComponent
  ]
})
export class FlyPanelEditConditionModule { }

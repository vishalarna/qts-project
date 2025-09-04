import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FlyPanelEditCriteriaComponent } from './fly-panel-edit-criteria.component';
import { CKEditorModule } from '@ckeditor/ckeditor5-angular';
import { BaseModule } from 'src/app/components/base/base.module';
import { ReactiveFormsModule } from '@angular/forms';



@NgModule({
  declarations: [
    FlyPanelEditCriteriaComponent
  ],
  imports: [
    CommonModule,
    BaseModule,
    CKEditorModule,
    ReactiveFormsModule,
  ],
  exports : [
    FlyPanelEditCriteriaComponent
  ]
})
export class FlyPanelEditCriteriaModule { }

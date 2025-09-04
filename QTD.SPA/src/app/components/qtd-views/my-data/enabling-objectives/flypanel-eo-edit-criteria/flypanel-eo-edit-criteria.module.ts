import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FlypanelEoEditCriteriaComponent } from './flypanel-eo-edit-criteria.component';
import { BaseModule } from 'src/app/components/base/base.module';
import { ReactiveFormsModule } from '@angular/forms';
import { CKEditorModule } from '@ckeditor/ckeditor5-angular';



@NgModule({
  declarations: [
    FlypanelEoEditCriteriaComponent
  ],
  imports: [
    CommonModule,
    BaseModule,
    ReactiveFormsModule,
    CKEditorModule,
  ],
  exports : [
    FlypanelEoEditCriteriaComponent,
  ]
})
export class FlypanelEoEditCriteriaModule { }

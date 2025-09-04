import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FlypanelEoEditConditionsComponent } from './flypanel-eo-edit-conditions.component';
import { BaseModule } from 'src/app/components/base/base.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CKEditorModule } from '@ckeditor/ckeditor5-angular';



@NgModule({
  declarations: [
    FlypanelEoEditConditionsComponent
  ],
  imports: [
    CommonModule,
    BaseModule,
    FormsModule,
    ReactiveFormsModule,
    CKEditorModule,
  ],
  exports : [
    FlypanelEoEditConditionsComponent,
  ]
})
export class FlypanelEoEditConditionsModule { }

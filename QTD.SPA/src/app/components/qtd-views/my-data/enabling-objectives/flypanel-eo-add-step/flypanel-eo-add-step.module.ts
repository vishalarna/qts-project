import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FlypanelEoAddStepComponent } from './flypanel-eo-add-step.component';
import { BaseModule } from 'src/app/components/base/base.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CKEditorModule } from '@ckeditor/ckeditor5-angular';



@NgModule({
  declarations: [
    FlypanelEoAddStepComponent
  ],
  imports: [
    CommonModule,
    BaseModule,
    FormsModule,
    ReactiveFormsModule,
    CKEditorModule,
  ],
  exports : [
    FlypanelEoAddStepComponent
  ]
})
export class FlypanelEoAddStepModule { }

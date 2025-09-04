import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FlypanelEoEditReferencesComponent } from './flypanel-eo-edit-references.component';
import { BaseModule } from 'src/app/components/base/base.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CKEditorModule } from '@ckeditor/ckeditor5-angular';



@NgModule({
  declarations: [
    FlypanelEoEditReferencesComponent
  ],
  imports: [
    CommonModule,
    BaseModule,
    ReactiveFormsModule,
    FormsModule,
    CKEditorModule,
  ],
  exports : [
    FlypanelEoEditReferencesComponent,
  ]
})
export class FlypanelEoEditReferencesModule { }

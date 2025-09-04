import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AddFillInTheBlankComponent } from './add-fill-in-the-blank.component';
import { BaseModule } from 'src/app/components/base/base.module';
import { CKEditorModule } from '@ckeditor/ckeditor5-angular';
import { ReactiveFormsModule,FormsModule } from '@angular/forms';
import { MatLegacyFormFieldModule as MatFormFieldModule } from '@angular/material/legacy-form-field';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';




@NgModule({
  declarations: [
    AddFillInTheBlankComponent
  ],
  imports: [
    CommonModule,
    BaseModule,
    CKEditorModule,
    ReactiveFormsModule,
    MatFormFieldModule,
    MatCheckboxModule,
    FormsModule
  ],
  exports : [
    AddFillInTheBlankComponent
  ]
})
export class AddFillInTheBlankModule { }

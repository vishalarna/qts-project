import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BaseModule } from 'src/app/components/base/base.module';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FillInTheBlanksComponent } from './fill-in-the-blanks.component';
import { CKEditorModule } from '@ckeditor/ckeditor5-angular';
import {MatLegacyFormFieldModule as MatFormFieldModule} from '@angular/material/legacy-form-field';

@NgModule({
  declarations: [
    FillInTheBlanksComponent
  ],
  imports: [
    CommonModule,
    BaseModule,
    FormsModule,
    ReactiveFormsModule,
    CKEditorModule,
    MatFormFieldModule,
  ],
  exports: [
    FillInTheBlanksComponent
  ]
})
export class FillInTheBlanksModule { }

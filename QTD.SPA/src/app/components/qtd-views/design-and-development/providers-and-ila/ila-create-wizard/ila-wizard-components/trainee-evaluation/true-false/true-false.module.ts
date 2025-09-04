import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TrueFalseComponent } from './true-false.component';
import { CKEditorModule } from '@ckeditor/ckeditor5-angular';
import {MatLegacyFormFieldModule as MatFormFieldModule} from '@angular/material/legacy-form-field';
import { BaseModule } from 'src/app/components/base/base.module';
import {MatLegacyRadioModule as MatRadioModule} from '@angular/material/legacy-radio';

@NgModule({
  declarations: [
    TrueFalseComponent
  ],
  imports: [
    CommonModule,
    CKEditorModule,
    MatFormFieldModule,
    BaseModule,
    FormsModule,
    ReactiveFormsModule,
    MatRadioModule

  ],
  exports:[TrueFalseComponent]
})
export class TrueFalseModule { }

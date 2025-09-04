import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AddTrueFalseComponent } from './add-true-false.component';
import { BaseModule } from 'src/app/components/base/base.module';
import { MatLegacyRadioModule as MatRadioModule } from '@angular/material/legacy-radio';
import { ReactiveFormsModule,FormsModule } from '@angular/forms';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { CKEditorModule } from '@ckeditor/ckeditor5-angular';


@NgModule({
  declarations: [
    AddTrueFalseComponent
  ],
  imports: [
    CommonModule,
    BaseModule,
    MatRadioModule,
    ReactiveFormsModule,
    MatCheckboxModule,
    FormsModule,
    CKEditorModule
  ],
  exports : [
    AddTrueFalseComponent,
  ]
})
export class AddTrueFalseModule { }

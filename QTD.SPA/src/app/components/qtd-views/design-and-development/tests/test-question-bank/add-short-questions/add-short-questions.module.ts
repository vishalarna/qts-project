import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AddShortQuestionsComponent } from './add-short-questions.component';
import { BaseModule } from 'src/app/components/base/base.module';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { ReactiveFormsModule,FormsModule } from '@angular/forms';
import { MatIconModule } from '@angular/material/icon'
import { MatLegacySelectModule as MatSelectModule } from '@angular/material/legacy-select';
import { MatLegacyChipsModule as MatChipsModule } from '@angular/material/legacy-chips';
import { CKEditorModule } from '@ckeditor/ckeditor5-angular';



@NgModule({
  declarations: [
    AddShortQuestionsComponent
  ],
  imports: [
    CommonModule,
    BaseModule,
    MatCheckboxModule,
    ReactiveFormsModule,
    MatIconModule,
    MatSelectModule,
    MatChipsModule,
    FormsModule,
    CKEditorModule
  ],
  exports : [
    AddShortQuestionsComponent,
  ]
})
export class AddShortQuestionsModule { }

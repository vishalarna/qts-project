import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { EmailTemplateEditorComponent } from './email-template-editor.component';
import { BaseModule } from 'src/app/components/base/base.module';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { CKEditorModule } from '@ckeditor/ckeditor5-angular';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';



@NgModule({
  declarations: [
    EmailTemplateEditorComponent
  ],
  imports: [
    CommonModule,
    BaseModule,
    CKEditorModule,
    FormsModule,
    ReactiveFormsModule,
  ],
  exports : [
    EmailTemplateEditorComponent
  ]
})
export class EmailTemplateEditorModule { }

import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatIconModule } from '@angular/material/icon';
import { CreateGuideEditorComponent } from './create-guide-editor.component';
import { BaseModule } from 'src/app/components/base/base.module';
import { CKEditorModule } from '@ckeditor/ckeditor5-angular';



@NgModule({
  declarations: [CreateGuideEditorComponent],
  imports: [
    CommonModule,
    MatIconModule,
    BaseModule,
    CKEditorModule,
  ],
  exports:[CreateGuideEditorComponent]
})
export class CreateGuideEditorModule { }

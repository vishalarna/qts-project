import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ShortAnswersComponent } from './short-answers.component';
import { BaseModule } from 'src/app/components/base/base.module';
import { CKEditorModule } from '@ckeditor/ckeditor5-angular';
import {MatLegacyFormFieldModule as MatFormFieldModule} from '@angular/material/legacy-form-field';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { MatLegacyRadioModule as MatRadioModule } from '@angular/material/legacy-radio';

@NgModule({
  declarations: [
    ShortAnswersComponent
  ],
  imports: [
    CommonModule,
    BaseModule,
    FormsModule,
    ReactiveFormsModule,
    CKEditorModule,
    MatFormFieldModule,
    MatCheckboxModule,
    MatRadioModule,
  ],
  exports:[ShortAnswersComponent]
})
export class ShortAnswersModule { }

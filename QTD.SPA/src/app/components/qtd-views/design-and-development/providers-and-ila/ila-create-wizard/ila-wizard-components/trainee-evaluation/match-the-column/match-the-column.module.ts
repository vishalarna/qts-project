import { ReactiveFormsModule } from '@angular/forms';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatchTheColumnComponent } from './match-the-column.component';
import { CKEditorModule } from '@ckeditor/ckeditor5-angular';
import {MatLegacyFormFieldModule as MatFormFieldModule} from '@angular/material/legacy-form-field';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { BaseModule } from 'src/app/components/base/base.module';

@NgModule({
  declarations: [
    MatchTheColumnComponent
  ],
  imports: [
    CommonModule,
    CKEditorModule,
    MatFormFieldModule,
    MatCheckboxModule,
    ReactiveFormsModule,
    BaseModule
  ],
  exports:[MatchTheColumnComponent]
})
export class MatchTheColumnModule { }

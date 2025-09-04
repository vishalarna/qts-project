import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AddMatchTheColumnComponent } from './add-match-the-column.component';
import { BaseModule } from 'src/app/components/base/base.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatLegacySelectModule as MatSelectModule } from '@angular/material/legacy-select';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { CKEditorModule } from '@ckeditor/ckeditor5-angular';



@NgModule({
  declarations: [
    AddMatchTheColumnComponent
  ],
  imports: [
    CommonModule,
    BaseModule,
    ReactiveFormsModule,
    FormsModule,
    MatSelectModule,
    MatCheckboxModule,
    CKEditorModule
  ],
  exports : [
    AddMatchTheColumnComponent,
  ]
})
export class AddMatchTheColumnModule { }

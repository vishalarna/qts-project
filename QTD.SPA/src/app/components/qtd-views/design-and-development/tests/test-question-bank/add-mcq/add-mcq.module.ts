import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AddMcqComponent } from './add-mcq.component';
import { BaseModule } from 'src/app/components/base/base.module';
import { MatLegacyRadioModule as MatRadioModule } from '@angular/material/legacy-radio';
import { ReactiveFormsModule,FormsModule } from '@angular/forms';
import { CKEditorModule } from '@ckeditor/ckeditor5-angular';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import {DragDropModule} from '@angular/cdk/drag-drop';


@NgModule({
  declarations: [
    AddMcqComponent
  ],
  imports: [
    CommonModule,
    BaseModule,
    MatRadioModule,
    ReactiveFormsModule,
    CKEditorModule,
    MatCheckboxModule,
    FormsModule,
    DragDropModule
  ],
  exports : [
    AddMcqComponent,
  ]
})
export class AddMcqModule { }

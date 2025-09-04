import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BaseModule } from 'src/app/components/base/base.module';
import { LayoutModule } from '@angular/cdk/layout';
import { AddAndSequenceTestQuestionsComponent } from './add-and-sequence-test-questions.component';
import { MatLegacyTableModule as MatTableModule } from '@angular/material/legacy-table';
import { DragDropModule } from '@angular/cdk/drag-drop';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { MatIconModule } from '@angular/material/icon';


@NgModule({
  declarations: [
    AddAndSequenceTestQuestionsComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    BaseModule,
    ReactiveFormsModule,
    LayoutModule,
    MatTableModule,
    DragDropModule,
    MatCheckboxModule,
    MatIconModule,
  ],
  exports:[AddAndSequenceTestQuestionsComponent]
})
export class AddAndSequenceTestQuestionsModule { }

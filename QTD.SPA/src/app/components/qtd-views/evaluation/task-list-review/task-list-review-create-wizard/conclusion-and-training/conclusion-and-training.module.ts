import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ConclusionAndTrainingComponent } from './conclusion-and-training.component';
import { CKEditorModule } from '@ckeditor/ckeditor5-angular';
import { BaseModule } from 'src/app/components/base/base.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { LayoutModule } from '@angular/cdk/layout';



@NgModule({
  declarations: [ConclusionAndTrainingComponent],
  imports: [
    CommonModule,
    CKEditorModule,
    BaseModule,
    FormsModule,
    LayoutModule,
    ReactiveFormsModule
  ],
  exports:[ConclusionAndTrainingComponent]
})
export class ConclusionAndTrainingModule { }
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BaseModule } from 'src/app/components/base/base.module';
import { LayoutModule } from '@angular/cdk/layout';
import { ReviewDesignAndDevelopmentComponent } from './review-design-and-development.component';
import { CKEditorModule } from '@ckeditor/ckeditor5-angular';


@NgModule({
  declarations: [
    ReviewDesignAndDevelopmentComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    BaseModule,
    ReactiveFormsModule,
    LayoutModule,
    CKEditorModule
  ],
  exports:[ReviewDesignAndDevelopmentComponent]
})
export class ReviewDesignAndDevelopmentModule { }

import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FlyPanelEditReferencesComponent } from './fly-panel-edit-references.component';
import { BaseModule } from 'src/app/components/base/base.module';
import { CKEditorModule } from '@ckeditor/ckeditor5-angular';
import { ReactiveFormsModule } from '@angular/forms';



@NgModule({
  declarations: [
    FlyPanelEditReferencesComponent
  ],
  imports: [
    CommonModule,
    BaseModule,
    CKEditorModule,
    ReactiveFormsModule,
  ],
  exports : [
    FlyPanelEditReferencesComponent
  ]
})
export class FlyPanelEditReferencesModule { }

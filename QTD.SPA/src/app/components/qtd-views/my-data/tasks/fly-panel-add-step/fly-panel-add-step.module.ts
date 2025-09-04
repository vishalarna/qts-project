import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FlyPanelAddStepComponent } from './fly-panel-add-step.component';
import { BaseModule } from 'src/app/components/base/base.module';
import { CKEditorModule } from '@ckeditor/ckeditor5-angular';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { FormsModule } from '@angular/forms';


@NgModule({
  declarations: [
    FlyPanelAddStepComponent
  ],
  imports: [
    CommonModule,
    BaseModule,
    CKEditorModule,
    FormsModule,
    MatCheckboxModule,
  ],
  exports : [
    FlyPanelAddStepComponent
  ]
})
export class FlyPanelAddStepModule { }

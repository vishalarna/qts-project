import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FlyPanelAddShSetComponent } from './fly-panel-add-sh-set.component';
import { BaseModule } from 'src/app/components/base/base.module';
import { CKEditorModule } from '@ckeditor/ckeditor5-angular';
import { ReactiveFormsModule } from '@angular/forms';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';



@NgModule({
  declarations: [FlyPanelAddShSetComponent],
  imports: [
    CommonModule,
    BaseModule,
    CKEditorModule,
    ReactiveFormsModule,
    MatCheckboxModule,
  ],
  exports : [FlyPanelAddShSetComponent],
})
export class FlyPanelAddShSetModule { }

import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FlyPanelAddCoversheetsComponent } from './fly-panel-add-coversheets.component';
import { LayoutModule } from '../../../layout/layout.module';
import { BaseModule } from 'src/app/components/base/base.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatLegacySelectModule as MatSelectModule } from '@angular/material/legacy-select';
import { CKEditorModule } from '@ckeditor/ckeditor5-angular';



@NgModule({
  declarations: [
    FlyPanelAddCoversheetsComponent
  ],
  imports: [
    CommonModule,
    BaseModule,
    LayoutModule,
    ReactiveFormsModule,
    MatSelectModule,
    CKEditorModule,
    FormsModule,
  ],
  exports: [FlyPanelAddCoversheetsComponent]
})
export class FlyPanelAddCoversheetsModule { }

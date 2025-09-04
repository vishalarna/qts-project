import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FlyPanelTopicComponent } from './fly-panel-topic.component';
import { BaseModule } from 'src/app/components/base/base.module';
import { LayoutModule } from '../../../layout/layout.module';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { MatLegacySelectModule as MatSelectModule } from '@angular/material/legacy-select';
import { CKEditorModule } from '@ckeditor/ckeditor5-angular';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatLegacyChipsModule as MatChipsModule } from '@angular/material/legacy-chips';

@NgModule({
  declarations: [FlyPanelTopicComponent],
  imports: [
    CommonModule,
    FormsModule,
    BaseModule,
    LayoutModule,
    MatCheckboxModule,
    MatSelectModule,
    CKEditorModule,
    ReactiveFormsModule,
    MatChipsModule,
  ],
  exports: [FlyPanelTopicComponent],
})
export class FlyPanelTopicModule {}

import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FlyPanelProviderComponent } from './fly-panel-provider.component';
import { LayoutModule } from '../../../layout/layout.module';
import { BaseModule } from 'src/app/components/base/base.module';
import {MatLegacyCheckboxModule as MatCheckboxModule} from '@angular/material/legacy-checkbox';
import { MatLegacySelectModule as MatSelectModule } from '@angular/material/legacy-select';
import { CKEditorModule } from '@ckeditor/ckeditor5-angular';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgxDropzoneModule } from 'ngx-dropzone';
import {MatLegacyTooltipModule as MatTooltipModule} from '@angular/material/legacy-tooltip';


@NgModule({
  declarations: [FlyPanelProviderComponent],
  imports: [
    FormsModule,
    CommonModule,
    LayoutModule,
    BaseModule,
    MatCheckboxModule,
    MatSelectModule,
    CKEditorModule,
    ReactiveFormsModule,
    NgxDropzoneModule,
    MatTooltipModule
  ],
  exports: [FlyPanelProviderComponent]
})
export class FlyPanelProviderModule { }

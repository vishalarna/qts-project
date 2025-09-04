import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BaseModule } from 'src/app/components/base/base.module';
import { LayoutModule } from '@angular/cdk/layout';
import { PreviewAndPublishMetaILATestComponent } from './preview-and-publish-meta-ila-test.component';
import { MatLegacyRadioModule as MatRadioModule } from '@angular/material/legacy-radio';
import { MatIconModule } from '@angular/material/icon';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';


@NgModule({
  declarations: [
    PreviewAndPublishMetaILATestComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    BaseModule,
    ReactiveFormsModule,
    LayoutModule,
    MatRadioModule,
    MatIconModule,
    MatCheckboxModule
  ],
  exports:[PreviewAndPublishMetaILATestComponent]
})
export class PreviewAndPublishMetaILATestModule { }

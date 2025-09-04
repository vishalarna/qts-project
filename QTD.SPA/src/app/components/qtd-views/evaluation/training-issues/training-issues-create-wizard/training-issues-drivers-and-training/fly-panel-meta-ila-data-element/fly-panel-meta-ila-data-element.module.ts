import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BaseModule } from 'src/app/components/base/base.module';
import { MatLegacyMenuModule as MatMenuModule } from '@angular/material/legacy-menu';
import { MatLegacyRadioModule as MatRadioModule } from '@angular/material/legacy-radio';
import { FlyPanelMetaIlaDataElementComponent } from './fly-panel-meta-ila-data-element.component';

@NgModule({
  declarations: [
    FlyPanelMetaIlaDataElementComponent
  ],
  imports: [
    CommonModule,
    BaseModule,
    FormsModule,
    ReactiveFormsModule,
    MatRadioModule,
    MatMenuModule,
    MatRadioModule,
  ],
  exports: [FlyPanelMetaIlaDataElementComponent]
})
export class FlyPanelMetaIlaDataElementModule { }

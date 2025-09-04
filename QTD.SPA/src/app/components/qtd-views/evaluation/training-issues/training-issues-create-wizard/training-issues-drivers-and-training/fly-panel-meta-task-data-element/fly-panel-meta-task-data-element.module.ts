import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BaseModule } from 'src/app/components/base/base.module';
import { MatExpansionModule } from '@angular/material/expansion';
import { MatLegacyMenuModule as MatMenuModule } from '@angular/material/legacy-menu';
import { MatTreeModule } from '@angular/material/tree';
import { MatLegacyRadioModule as MatRadioModule } from '@angular/material/legacy-radio';
import { FlyPanelMetaTaskDataElementComponent } from './fly-panel-meta-task-data-element.component';

@NgModule({
  declarations: [
    FlyPanelMetaTaskDataElementComponent
  ],
  imports: [
    CommonModule,
    BaseModule,
    FormsModule,
    ReactiveFormsModule,
    MatExpansionModule,
    MatRadioModule,
    MatTreeModule,
    MatMenuModule,
    ],
  exports:[FlyPanelMetaTaskDataElementComponent]
})
export class FlyPanelMetaTaskDataElementModule { }

import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BaseModule } from 'src/app/components/base/base.module';
import { MatExpansionModule } from '@angular/material/expansion';
import { MatLegacyMenuModule as MatMenuModule } from '@angular/material/legacy-menu';
import { MatTreeModule } from '@angular/material/tree';
import { MatLegacyRadioModule as MatRadioModule } from '@angular/material/legacy-radio';
import { FlyPanelRegulatoryRequirementDataElementComponent } from './fly-panel-regulatory-requirement-data-element.component';

@NgModule({
  declarations: [
    FlyPanelRegulatoryRequirementDataElementComponent
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
    MatRadioModule,
  ],
  exports: [FlyPanelRegulatoryRequirementDataElementComponent]
})
export class FlyPanelRegulatoryRequirementDataElementModule { }

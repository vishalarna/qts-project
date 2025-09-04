import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BaseModule } from 'src/app/components/base/base.module';
import { MatLegacyMenuModule as MatMenuModule } from '@angular/material/legacy-menu';
import { MatLegacyRadioModule as MatRadioModule } from '@angular/material/legacy-radio';
import { FlyPanelTrainingProgramDataElementComponent } from './fly-panel-training-program-data-element.component';
import { MatLegacyFormFieldModule as MatFormFieldModule } from '@angular/material/legacy-form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatLegacySelectModule as MatSelectModule } from '@angular/material/legacy-select';

@NgModule({
  declarations: [
    FlyPanelTrainingProgramDataElementComponent
  ],
  imports: [
    CommonModule,
    BaseModule,
    FormsModule,
    ReactiveFormsModule,
    MatRadioModule,
    MatMenuModule,
    MatRadioModule,
    MatFormFieldModule,
    MatIconModule,
    MatSelectModule
  ],
  exports: [FlyPanelTrainingProgramDataElementComponent]
})
export class FlyPanelTrainingProgramDataElementModule { }

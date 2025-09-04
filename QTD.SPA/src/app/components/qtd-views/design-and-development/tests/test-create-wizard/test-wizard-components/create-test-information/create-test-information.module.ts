import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CreateTestInformationComponent } from './create-test-information.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BaseModule } from 'src/app/components/base/base.module';
import { MatLegacyFormFieldModule as MatFormFieldModule } from '@angular/material/legacy-form-field';
import { LayoutModule } from 'src/app/components/qtd-views/layout/layout.module';
import { MatLegacyRadioModule as MatRadioModule } from '@angular/material/legacy-radio';
import { MatLegacyTooltipModule as MatTooltipModule } from '@angular/material/legacy-tooltip';
import { MatLegacySelectModule as MatSelectModule } from '@angular/material/legacy-select';
import { MatLegacyProgressSpinnerModule as MatProgressSpinnerModule, MatLegacySpinner as MatSpinner } from '@angular/material/legacy-progress-spinner';



@NgModule({
  declarations: [CreateTestInformationComponent],
  imports: [
    CommonModule,
    FormsModule,
    BaseModule,
    MatFormFieldModule,
    ReactiveFormsModule,
    LayoutModule, 
    MatRadioModule,
    MatTooltipModule,
    MatFormFieldModule,
    MatSelectModule,
    MatProgressSpinnerModule
  ],
  exports: [CreateTestInformationComponent]
})
export class CreateTestInformationModule { }

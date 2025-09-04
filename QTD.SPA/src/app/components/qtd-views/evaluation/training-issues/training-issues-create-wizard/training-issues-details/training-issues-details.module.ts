import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BaseModule } from 'src/app/components/base/base.module';
import { LayoutModule } from '@angular/cdk/layout';
import { MatLegacySelectModule as MatSelectModule } from '@angular/material/legacy-select';
import { TrainingIssuesDetailsComponent } from './training-issues-details.component';

@NgModule({
  declarations: [
    TrainingIssuesDetailsComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    BaseModule,
    ReactiveFormsModule,
    LayoutModule,
    MatSelectModule,
  ],
  exports:[TrainingIssuesDetailsComponent]
})
export class TrainingIssuesDetailsModule { }

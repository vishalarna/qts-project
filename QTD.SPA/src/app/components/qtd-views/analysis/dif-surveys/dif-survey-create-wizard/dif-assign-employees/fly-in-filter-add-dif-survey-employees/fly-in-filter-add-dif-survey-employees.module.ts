import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FlypanelFilterAddDifSurveyEmpsComponent } from './fly-in-filter-add-dif-survey-employees.component';
import { BaseModule } from 'src/app/components/base/base.module';
import { MatLegacySelectModule as MatSelectModule } from '@angular/material/legacy-select';
import { ReactiveFormsModule } from '@angular/forms';



@NgModule({
  declarations: [
    FlypanelFilterAddDifSurveyEmpsComponent
  ],
  imports: [
    CommonModule,
    BaseModule,
    MatSelectModule,
    ReactiveFormsModule,
  ],
  exports : [
    FlypanelFilterAddDifSurveyEmpsComponent,
  ]
})
export class FlypanelFilterAddDifSurveyEmpsModule { }

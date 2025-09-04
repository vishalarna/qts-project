import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BaseModule } from 'src/app/components/base/base.module';
import { MatLegacyFormFieldModule as MatFormFieldModule } from '@angular/material/legacy-form-field';
import { LayoutModule } from '@angular/cdk/layout';
import { MatLegacySelectModule as MatSelectModule } from '@angular/material/legacy-select';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { MatLegacyChipsModule as MatChipsModule } from '@angular/material/legacy-chips';
import { MatLegacyTooltipModule as MatTooltipModule } from '@angular/material/legacy-tooltip';
import { MatLegacyTabsModule as MatTabsModule } from '@angular/material/legacy-tabs';
import {MatLegacyRadioModule as MatRadioModule} from '@angular/material/legacy-radio';
import { StoreModule } from '@ngrx/store';
import { DifCreateSurveyComponent } from './dif-create-survey.component';
import { CKEditorModule } from '@ckeditor/ckeditor5-angular';


@NgModule({
  declarations: [
    DifCreateSurveyComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    BaseModule,
    ReactiveFormsModule,
    MatFormFieldModule,
    ReactiveFormsModule,
    LayoutModule,
    MatSelectModule,
    MatCheckboxModule,
    MatChipsModule,
    MatTooltipModule,
    MatTabsModule,
    MatRadioModule,
    StoreModule,
    CKEditorModule
  ],
  exports:[DifCreateSurveyComponent]
})
export class DifCreateSurveyModule { }

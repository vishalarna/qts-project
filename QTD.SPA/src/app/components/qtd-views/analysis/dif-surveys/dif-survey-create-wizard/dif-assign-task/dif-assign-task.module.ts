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
import { DifAssignTaskComponent } from './dif-assign-task.component';
import { MatExpansionModule } from '@angular/material/expansion';
import { FlyPanelDifSurveyTasksModule } from './fly-panel-dif-survey-tasks/fly-panel-dif-survey-tasks.module';
import { MatLegacyTableModule as MatTableModule } from '@angular/material/legacy-table';
import { MatSortModule } from '@angular/material/sort';


@NgModule({
  declarations: [
    DifAssignTaskComponent,   
  ],
  imports: [
    CommonModule,
    FormsModule,
    BaseModule,
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
    MatExpansionModule,
    FlyPanelDifSurveyTasksModule,
    MatTableModule,
    MatSortModule
  ],
  exports:[DifAssignTaskComponent]
})
export class DifAssignTaskModule { }

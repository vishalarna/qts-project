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
import { MatLegacyRadioModule as MatRadioModule } from '@angular/material/legacy-radio';
import { StoreModule } from '@ngrx/store';
import { DifAssignEmployeesComponent } from './dif-assign-employees.component';
import { FlyPanelAddEmployeeModule } from 'src/app/components/qtd-views/implementation/procedure-review/fly-panel-add-procedure-review/fly-panel-add-employee/fly-panel-add-employee.module';
import { FlyPanelDifSurveyEmployeesModule } from './fly-panel-dif-survey-employees/fly-panel-dif-survey-employees.module';
import { MatLegacyTableModule as MatTableModule } from '@angular/material/legacy-table';

@NgModule({
  declarations: [DifAssignEmployeesComponent],
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
    FlyPanelDifSurveyEmployeesModule,
    MatTableModule
  ],
  exports: [DifAssignEmployeesComponent],
})
export class DifAssignEmployeesModule {}

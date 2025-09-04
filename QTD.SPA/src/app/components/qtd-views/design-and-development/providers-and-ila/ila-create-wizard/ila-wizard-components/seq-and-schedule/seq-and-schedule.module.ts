import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SeqAndScheduleComponent } from './seq-and-schedule.component';
import { BaseModule } from 'src/app/components/base/base.module';
import { DragDropModule } from '@angular/cdk/drag-drop';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { MatLegacyRadioModule as MatRadioModule } from '@angular/material/legacy-radio';
import { CKEditorModule } from '@ckeditor/ckeditor5-angular';
import { MatLegacySelectModule as MatSelectModule } from '@angular/material/legacy-select';
import { MatLegacyInputModule as MatInputModule } from '@angular/material/legacy-input';
import {MatButtonToggleModule} from '@angular/material/button-toggle';
import { MatStepperModule } from '@angular/material/stepper';
import { IlaEmpCbtSettingsModule } from './ila-emp-cbt-settings/ila-emp-cbt-settings.module';
import { IlaEmpEvalSettingsModule } from './ila-emp-eval-settings/ila-emp-eval-settings.module';
import { IlaEmpSchedulingModule } from './ila-emp-scheduling/ila-emp-scheduling.module';
import { IlaEmpTestSettingsModule } from './ila-emp-test-settings/ila-emp-test-settings.module';
import { IlaEmpTqSettingsModule } from './ila-emp-tq-settings/ila-emp-tq-settings.module';
import { MatLegacyTabsModule as MatTabsModule } from '@angular/material/legacy-tabs';
import { MatIconModule } from '@angular/material/icon';
import { MatLegacyTableModule as MatTableModule } from '@angular/material/legacy-table';
import { MatSortModule } from '@angular/material/sort';
import { MatLegacyPaginatorModule as MatPaginatorModule } from '@angular/material/legacy-paginator';
import { MatLegacyTooltipModule as MatTooltipModule } from '@angular/material/legacy-tooltip';
import { CbtManagerModule } from 'src/app/components/shared/cbt-manager/cbt-manager.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

@NgModule({
  declarations: [SeqAndScheduleComponent],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    BaseModule,
    DragDropModule,
    MatCheckboxModule,
    CKEditorModule,
    MatRadioModule,
    MatSelectModule,
    MatInputModule,
    MatButtonToggleModule,
    MatStepperModule,
    MatTabsModule,
    IlaEmpCbtSettingsModule,
    IlaEmpEvalSettingsModule,
    IlaEmpSchedulingModule,
    IlaEmpTestSettingsModule,
    IlaEmpTqSettingsModule,
    MatIconModule,
    MatTableModule,
    MatSortModule,
    MatPaginatorModule,
    MatTooltipModule,
    CbtManagerModule,
  ],
  exports: [SeqAndScheduleComponent],
})
export class SeqAndScheduleModule {}

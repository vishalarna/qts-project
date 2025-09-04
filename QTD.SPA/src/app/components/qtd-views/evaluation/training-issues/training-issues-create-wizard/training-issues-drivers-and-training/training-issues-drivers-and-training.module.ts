import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BaseModule } from 'src/app/components/base/base.module';
import { LayoutModule } from '@angular/cdk/layout';
import { MatLegacySelectModule as MatSelectModule } from '@angular/material/legacy-select';
import { TrainingIssuesDriversAndTrainingComponent } from './training-issues-drivers-and-training.component';
import { FlyPanelProcedureDataElementModule } from './fly-panel-procedure-data-element/fly-panel-procedure-data-element.module';
import { MatLegacyRadioModule as MatRadioModule } from '@angular/material/legacy-radio';
import { FlyPanelRegulatoryRequirementDataElementModule } from './fly-panel-regulatory-requirement-data-element/fly-panel-regulatory-requirement-data-element.module';
import { FlyPanelSafetyHazardDataElementModule } from './fly-panel-safety-hazard-data-element/fly-panel-safety-hazard-data-element.module';
import { FlyPanelToolDataElementModule } from './fly-panel-tool-data-element/fly-panel-tool-data-element.module';
import { FlyPanelIlaDataElementModule } from './fly-panel-ila-data-element/fly-panel-ila-data-element.module';
import { FlyPanelMetaIlaDataElementModule } from './fly-panel-meta-ila-data-element/fly-panel-meta-ila-data-element.module';
import { FlyPanelTestDataElementModule } from './fly-panel-test-data-element/fly-panel-test-data-element.module';
import { FlyPanelPretestDataElementModule } from './fly-panel-pretest-data-element/fly-panel-pretest-data-element.module';
import { FlyPanelCbtDataElementModule } from './fly-panel-cbt-data-element/fly-panel-cbt-data-element.module';
import { FlyPanelTrainingProgramDataElementModule } from './fly-panel-training-program-data-element/fly-panel-training-program-data-element.module';
import { FlyPanelTestItemDataElementModule } from './fly-panel-testitem-data-element/fly-panel-testitem-data-element.module';
import { FlyPanelTaskDataElementModule } from './fly-panel-task-data-element/fly-panel-task-data-element.module';
import { FlyPanelEnablingObjectiveDataElementModule } from './fly-panel-enabling-objective-data-element/fly-panel-enabling-objective-data-element.module';
import { FlyPanelMetaEnablingObjectiveDataElementModule } from './fly-panel-meta-enabling-objective-data-element/fly-panel-meta-enabling-objective-data-element.module';
import { FlyPanelMetaTaskDataElementModule } from './fly-panel-meta-task-data-element/fly-panel-meta-task-data-element.module';


@NgModule({
  declarations: [
    TrainingIssuesDriversAndTrainingComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    BaseModule,
    ReactiveFormsModule,
    LayoutModule,
    MatSelectModule,
    MatRadioModule,
    FlyPanelProcedureDataElementModule,
    FlyPanelRegulatoryRequirementDataElementModule,
    FlyPanelSafetyHazardDataElementModule,
    FlyPanelToolDataElementModule,
    FlyPanelIlaDataElementModule,
    FlyPanelMetaIlaDataElementModule,
    FlyPanelTestDataElementModule,
    FlyPanelPretestDataElementModule,
    FlyPanelTaskDataElementModule,
    FlyPanelCbtDataElementModule,
    FlyPanelTrainingProgramDataElementModule,
    FlyPanelTestItemDataElementModule,
    FlyPanelEnablingObjectiveDataElementModule,
    FlyPanelMetaEnablingObjectiveDataElementModule,
    FlyPanelMetaTaskDataElementModule
  ],
  exports: [TrainingIssuesDriversAndTrainingComponent]
})
export class TrainingIssuesDriversAndTrainingModule { }

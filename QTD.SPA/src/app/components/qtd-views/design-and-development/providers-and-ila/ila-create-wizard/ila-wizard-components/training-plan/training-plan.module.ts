import { FlyPanelLinkScenarioObjectiveModule } from './../trainee-evaluation/fly-panel-link-scenario-objective/fly-panel-link-scenario-objective.module';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TrainingPlanComponent } from './training-plan.component';
import { BaseModule } from 'src/app/components/base/base.module';
import { CKEditorModule } from '@ckeditor/ckeditor5-angular';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { MatLegacyTableModule as MatTableModule } from '@angular/material/legacy-table';
import { MatSortModule } from '@angular/material/sort';
import { MatLegacyPaginatorModule as MatPaginatorModule } from '@angular/material/legacy-paginator';
import { FlyPanelObjectivesModule } from './fly-panel-objectives/fly-panel-objectives.module';
import { MatLegacyFormFieldModule as MatFormFieldModule } from '@angular/material/legacy-form-field';
import { MatLegacySelectModule as MatSelectModule } from '@angular/material/legacy-select';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatLegacyTooltipModule as MatTooltipModule } from '@angular/material/legacy-tooltip';
import { DragDropModule } from '@angular/cdk/drag-drop';
import { MatLegacyMenuModule as MatMenuModule } from '@angular/material/legacy-menu';

@NgModule({
  declarations: [TrainingPlanComponent],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    BaseModule,
    CKEditorModule,
    MatCheckboxModule,
    MatTableModule,
    MatSortModule,
    MatPaginatorModule,
    FlyPanelObjectivesModule,
    MatFormFieldModule,
    MatSelectModule,
    MatTooltipModule,
    DragDropModule,
    FlyPanelLinkScenarioObjectiveModule,
    FormsModule,
    MatMenuModule
  ],
  exports: [TrainingPlanComponent],
})
export class TrainingPlanModule {}

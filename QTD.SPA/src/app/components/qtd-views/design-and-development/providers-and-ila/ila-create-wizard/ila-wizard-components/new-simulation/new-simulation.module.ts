import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NewSimulationComponent } from './new-simulation.component';
import { CKEditorModule } from '@ckeditor/ckeditor5-angular';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { MatLegacyTableModule as MatTableModule } from '@angular/material/legacy-table';
import { MatSortModule } from '@angular/material/sort';
import { MatLegacySelectModule as MatSelectModule } from '@angular/material/legacy-select';
import { MatLegacyChipsModule as MatChipsModule } from '@angular/material/legacy-chips';
import { BaseModule } from 'src/app/components/base/base.module';
import { LocalizeModule } from 'src/app/_Shared/modules/localize.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { FlyPanelObjectivesModule } from '../training-plan/fly-panel-objectives/fly-panel-objectives.module';
import { FlyPanelLinkTaskModule } from '../trainee-evaluation/fly-panel-link-task/fly-panel-link-task.module';
import { FlyPanelLinkScenarioObjectiveModule } from '../trainee-evaluation/fly-panel-link-scenario-objective/fly-panel-link-scenario-objective.module';

@NgModule({
  declarations: [NewSimulationComponent],
  imports: [
    CommonModule,
    CommonModule,
    BaseModule,
    LocalizeModule,
    FormsModule,
    ReactiveFormsModule,
    CKEditorModule,
    MatCheckboxModule,
    MatTableModule,
    MatSortModule,
    MatSelectModule,
    MatChipsModule,
    FlyPanelObjectivesModule,
    FlyPanelLinkTaskModule,
    FlyPanelLinkScenarioObjectiveModule
  ],
  exports: [NewSimulationComponent],
})
export class NewSimulationModule {}

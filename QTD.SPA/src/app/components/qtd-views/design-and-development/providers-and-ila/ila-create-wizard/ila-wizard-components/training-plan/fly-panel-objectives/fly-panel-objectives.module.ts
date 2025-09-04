import { FlypanelAddTaskModule } from './../../../../../../my-data/tasks/flypanel-add-task/flypanel-add-task.module';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FlyPanelObjectivesComponent } from './fly-panel-objectives.component';
import { BaseModule } from 'src/app/components/base/base.module';
import { MatExpansionModule } from '@angular/material/expansion';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { MatLegacyTabsModule as MatTabsModule } from '@angular/material/legacy-tabs';
import { MatTreeModule } from '@angular/material/tree';
import { MatLegacyMenuModule as MatMenuModule } from '@angular/material/legacy-menu';
import { MatLegacySelectModule as MatSelectModule } from '@angular/material/legacy-select';
import { ReactiveFormsModule } from '@angular/forms';
import { MatLegacyProgressSpinnerModule as MatProgressSpinnerModule } from '@angular/material/legacy-progress-spinner';
import {FormsModule} from '@angular/forms';
import {FlypanelAddEoModule} from 'src/app/components/qtd-views/my-data/enabling-objectives/flypanel-add-eo/flypanel-add-eo.module';

@NgModule({
  declarations: [FlyPanelObjectivesComponent],
  imports: [
    CommonModule,
    BaseModule,
    ReactiveFormsModule,
    MatTreeModule,
    MatCheckboxModule,
    MatTabsModule,
    MatMenuModule,
    MatSelectModule,
    MatProgressSpinnerModule,
    FormsModule,
    FlypanelAddTaskModule,
    FlypanelAddEoModule
  ],
  exports: [FlyPanelObjectivesComponent],
})
export class FlyPanelObjectivesModule {}

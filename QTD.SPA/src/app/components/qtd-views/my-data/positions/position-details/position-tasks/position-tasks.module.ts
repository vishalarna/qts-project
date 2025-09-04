import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PositionTasksComponent } from './position-tasks.component';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { MatLegacyTableModule as MatTableModule } from '@angular/material/legacy-table';
import { MatLegacyFormFieldModule as MatFormFieldModule } from '@angular/material/legacy-form-field';
import { MatLegacySelectModule as MatSelectModule } from '@angular/material/legacy-select';
import { BaseModule } from 'src/app/components/base/base.module';
import { FlyPanelLinkPositionTaskModule } from '../../fly-panel-link-position-task/fly-panel-link-position-task.module';
import { FlyPanelAddSkaModule } from 'src/app/components/qtd-views/design-and-development/skills-assessment/fly-panel-add-ska/fly-panel-add-ska.module';
import { MatSortModule } from '@angular/material/sort';
import { MatLegacyPaginatorModule as MatPaginatorModule } from '@angular/material/legacy-paginator';
import { FlyPanelLinkedPositionsModule } from '../../fly-panel-linked-positions/fly-panel-linked-positions.module';
import { FlyPanelLinkedTrainingGroupsModule } from '../../fly-panel-linked-training-groups/fly-panel-linked-training-groups.module';
import { MatLegacyTooltipModule as MatTooltipModule } from '@angular/material/legacy-tooltip';
import { FlyPanelLinkPositionR6TaskModule } from '../../fly-panel-link-position-r6-task/fly-panel-link-position-r6-task.module';
import { FlyPanelLinkPositionR5TaskModule } from '../../fly-panel-link-position-r5-task/fly-panel-link-position-r5-task.module';
import { MatLegacyMenuModule as MatMenuModule } from '@angular/material/legacy-menu';
import { DialogueUnlinkR5TasksModule } from '../../dialogue-unlink-r5-tasks/dialogue-unlink-r5-tasks.module';
import { FlyPanelPositionR6TaskInformationModule } from '../../fly-panel-position-r6-task-information/fly-panel-position-r6-task-information.module';
import { FlyPanelLinkPositionFilterModule } from '../../fly-panel-link-position-filter/fly-panel-link-position-filter.module';



@NgModule({
  declarations: [
    PositionTasksComponent
  ],
  imports: [
    CommonModule,
    BaseModule,
    MatCheckboxModule,
    MatTableModule,
    MatFormFieldModule,
    MatSelectModule,
    MatSortModule,
    MatTooltipModule,
    MatPaginatorModule,
    FlyPanelLinkPositionTaskModule,
    FlyPanelAddSkaModule,
    FlyPanelLinkedPositionsModule,
    FlyPanelLinkedTrainingGroupsModule,
    FlyPanelLinkPositionR6TaskModule,
    FlyPanelLinkPositionR5TaskModule,
    MatMenuModule,
    DialogueUnlinkR5TasksModule,
    FlyPanelPositionR6TaskInformationModule,
    FlyPanelLinkPositionFilterModule
  ],
  exports:[PositionTasksComponent],
})
export class PositionTasksModule { }

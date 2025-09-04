import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TaskSafetyHazardsComponent } from './task-safety-hazards.component';
import { BaseModule } from 'src/app/components/base/base.module';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { MatLegacyTableModule as MatTableModule } from '@angular/material/legacy-table';
import { MatLegacyPaginatorModule as MatPaginatorModule } from '@angular/material/legacy-paginator';
import { MatSortModule } from '@angular/material/sort';
import { FlyPanelTaskShLinkModule } from '../../fly-panel-task-sh-link/fly-panel-task-sh-link.module';
import { FlyPanelLinkedTasksModule } from '../../fly-panel-linked-tasks/fly-panel-linked-tasks.module';
import { MatLegacyTooltipModule as MatTooltipModule } from '@angular/material/legacy-tooltip';



@NgModule({
  declarations: [
    TaskSafetyHazardsComponent
  ],
  imports: [
    CommonModule,
    BaseModule,
    MatCheckboxModule,
    MatTableModule,
    MatPaginatorModule,
    MatSortModule,
    MatTooltipModule,
    FlyPanelTaskShLinkModule,
    FlyPanelLinkedTasksModule,
  ],
  exports : [
    TaskSafetyHazardsComponent
  ]
})
export class TaskSafetyHazardsModule { }

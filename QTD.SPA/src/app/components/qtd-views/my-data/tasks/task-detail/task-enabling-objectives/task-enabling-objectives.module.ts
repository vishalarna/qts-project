import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TaskEnablingObjectivesComponent } from './task-enabling-objectives.component';
import { BaseModule } from 'src/app/components/base/base.module';
import { MatLegacyTableModule as MatTableModule } from '@angular/material/legacy-table';
import { MatLegacyPaginatorModule as MatPaginatorModule } from '@angular/material/legacy-paginator';
import { MatSortModule } from '@angular/material/sort';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { FlyPanelTaskEosLinkModule } from '../../fly-panel-task-eos-link/fly-panel-task-eos-link.module';
import { FlyPanelLinkedTasksModule } from '../../fly-panel-linked-tasks/fly-panel-linked-tasks.module';
import { MatLegacyTooltipModule as MatTooltipModule } from '@angular/material/legacy-tooltip';



@NgModule({
  declarations: [
    TaskEnablingObjectivesComponent
  ],
  imports: [
    CommonModule,
    BaseModule,
    MatTableModule,
    MatPaginatorModule,
    MatSortModule,
    MatCheckboxModule,
    FlyPanelTaskEosLinkModule,
    FlyPanelLinkedTasksModule,
    MatTooltipModule
  ],
  exports : [
    TaskEnablingObjectivesComponent
  ]
})
export class TaskEnablingObjectivesModule { }

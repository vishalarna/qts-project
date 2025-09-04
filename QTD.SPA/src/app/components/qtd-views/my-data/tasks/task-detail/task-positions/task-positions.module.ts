import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TaskPositionsComponent } from './task-positions.component';
import { BaseModule } from 'src/app/components/base/base.module';
import { MatLegacyTableModule as MatTableModule } from '@angular/material/legacy-table';
import { MatLegacyPaginatorModule as MatPaginatorModule } from '@angular/material/legacy-paginator';
import { MatSortModule } from '@angular/material/sort';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { FlyPanelTaskPosLinkModule } from '../../fly-panel-task-pos-link/fly-panel-task-pos-link.module';
import { FlyPanelLinkedTasksModule } from '../../fly-panel-linked-tasks/fly-panel-linked-tasks.module';

@NgModule({
  declarations: [TaskPositionsComponent],
  imports: [
    CommonModule,
    BaseModule,
    MatTableModule,
    MatPaginatorModule,
    MatSortModule,
    MatCheckboxModule,
    FlyPanelTaskPosLinkModule,
    FlyPanelLinkedTasksModule,
  ],
  exports: [TaskPositionsComponent],
})
export class TaskPositionsModule {}

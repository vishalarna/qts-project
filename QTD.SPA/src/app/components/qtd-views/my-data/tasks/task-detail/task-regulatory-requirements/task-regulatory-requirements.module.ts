import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TaskRegulatoryRequirementsComponent } from './task-regulatory-requirements.component';
import { BaseModule } from 'src/app/components/base/base.module';
import { MatLegacyTableModule as MatTableModule } from '@angular/material/legacy-table';
import { MatLegacyPaginatorModule as MatPaginatorModule } from '@angular/material/legacy-paginator';
import { MatSortModule } from '@angular/material/sort';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { FlyPanelTaskRrLinkModule } from '../../fly-panel-task-rr-link/fly-panel-task-rr-link.module';
import { FlyPanelLinkedTasksModule } from '../../fly-panel-linked-tasks/fly-panel-linked-tasks.module';

@NgModule({
  declarations: [TaskRegulatoryRequirementsComponent],
  imports: [
    CommonModule,
    BaseModule,
    MatTableModule,
    MatPaginatorModule,
    MatSortModule,
    MatCheckboxModule,
    FlyPanelTaskRrLinkModule,
    FlyPanelLinkedTasksModule,
  ],
  exports: [TaskRegulatoryRequirementsComponent],
})
export class TaskRegulatoryRequirementsModule {}

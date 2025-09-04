import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TaskIlasComponent } from './task-ilas.component';
import { BaseModule } from 'src/app/components/base/base.module';
import { MatLegacyTableModule as MatTableModule } from '@angular/material/legacy-table';
import { MatLegacyPaginatorModule as MatPaginatorModule } from '@angular/material/legacy-paginator';
import { MatSortModule } from '@angular/material/sort';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { FlyPanelTaskIlaLinkModule } from '../../fly-panel-task-ila-link/fly-panel-task-ila-link.module';
import { FlyPanelLinkedTasksModule } from '../../fly-panel-linked-tasks/fly-panel-linked-tasks.module';



@NgModule({
  declarations: [
    TaskIlasComponent
  ],
  imports: [
    CommonModule,
    BaseModule,
    MatTableModule,
    MatPaginatorModule,
    MatSortModule,
    MatCheckboxModule,
    FlyPanelTaskIlaLinkModule,
    FlyPanelLinkedTasksModule,
  ],
  exports : [
    TaskIlasComponent
  ]
})
export class TaskIlasModule { }

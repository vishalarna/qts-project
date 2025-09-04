import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TaskOverviewComponent } from './task-overview.component';
import { MatLegacyPaginatorModule as MatPaginatorModule } from '@angular/material/legacy-paginator';
import { MatSortModule } from '@angular/material/sort';
import { MatLegacyTableModule as MatTableModule } from '@angular/material/legacy-table';
import { Routes, RouterModule } from '@angular/router';
import { BaseModule } from 'src/app/components/base/base.module';
import { FlyPanelTaskNotLinkedModule } from '../fly-panel-task-not-linked/fly-panel-task-not-linked.module';
import { FlyPanelTaskHistoryModule } from '../fly-panel-task-history/fly-panel-task-history.module';

const routes: Routes = [
  {
    path: '',
    component: TaskOverviewComponent,
  },
];

@NgModule({
  declarations: [TaskOverviewComponent],
  imports: [
    CommonModule,
    MatTableModule,
    MatPaginatorModule,
    MatSortModule,
    BaseModule,
    RouterModule.forChild(routes),
    FlyPanelTaskNotLinkedModule,
    FlyPanelTaskHistoryModule,
  ],
})
export class TaskOverviewModule {}

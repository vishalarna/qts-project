import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ToolOverviewComponent } from './tool-overview.component';
import { FlyPanelToolNotLinkedModule } from '../fly-panel-tool-not-linked/fly-panel-tool-not-linked.module';
import { MatLegacyPaginatorModule as MatPaginatorModule } from '@angular/material/legacy-paginator';
import { MatSortModule } from '@angular/material/sort';
import { MatLegacyTableModule as MatTableModule } from '@angular/material/legacy-table';
import { Routes, RouterModule } from '@angular/router';
import { BaseModule } from 'src/app/components/base/base.module';
import { FlyPanelToolHistoryModule } from '../fly-panel-tool-history/fly-panel-tool-history.module';

const routes: Routes = [
  {
    path: '',
    component: ToolOverviewComponent,
  },
];

@NgModule({
  declarations: [ToolOverviewComponent],
  imports: [
    CommonModule,
    MatTableModule,
    MatPaginatorModule,
    MatSortModule,
    BaseModule,
    RouterModule.forChild(routes),
    FlyPanelToolNotLinkedModule,
    FlyPanelToolHistoryModule
  ],
})
export class ToolOverviewModule {}

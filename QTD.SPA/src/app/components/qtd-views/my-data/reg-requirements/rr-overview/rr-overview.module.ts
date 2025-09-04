import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RROverviewComponent } from './rr-overview.component';
import { FlyPanelRRNotLinkedModule } from '../fly-panel-rr-not-linked/fly-panel-rr-not-linked.module';
import { MatLegacyPaginatorModule as MatPaginatorModule } from '@angular/material/legacy-paginator';
import { MatSortModule } from '@angular/material/sort';
import { MatLegacyTableModule as MatTableModule } from '@angular/material/legacy-table';
import { RouterModule, Routes } from '@angular/router';
import { BaseModule } from 'src/app/components/base/base.module';
import { FlyPanelRrHistoryModule } from '../fly-panel-rr-history/fly-panel-rr-history.module';

const routes: Routes = [
  {
    path: '',
    component: RROverviewComponent,
  },
];

@NgModule({
  declarations: [RROverviewComponent],
  imports: [
    CommonModule,
    MatTableModule,
    MatPaginatorModule,
    MatSortModule,
    BaseModule,
    RouterModule.forChild(routes),
    FlyPanelRRNotLinkedModule,
    FlyPanelRrHistoryModule
  ],
})
export class RROverviewModule {}

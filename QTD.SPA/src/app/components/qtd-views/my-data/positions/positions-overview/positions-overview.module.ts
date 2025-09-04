import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PositionsOverviewComponent } from './positions-overview.component';
import { RouterModule, Routes } from '@angular/router';
import { MatLegacyPaginatorModule as MatPaginatorModule } from '@angular/material/legacy-paginator';
import { MatSortModule } from '@angular/material/sort';
import { BaseModule } from 'src/app/components/base/base.module';
import { MatLegacyTableModule as MatTableModule } from '@angular/material/legacy-table';
import { FlyPanelPositionNotLinkedModule } from '../fly-panel-position-not-linked/fly-panel-position-not-linked.module';
import { FlyPanelPositionsHistoryModule } from '../fly-panel-positions-history/fly-panel-positions-history.module';

const routes: Routes = [
  {
    path: '',
    component: PositionsOverviewComponent,
  }
 ]


@NgModule({
  declarations: [
    PositionsOverviewComponent
  ],
  imports: [
    BaseModule,
    CommonModule,
    MatTableModule,
    MatPaginatorModule,
    MatSortModule,
    BaseModule,
    MatTableModule,
    RouterModule.forChild(routes),
    FlyPanelPositionNotLinkedModule,
    FlyPanelPositionsHistoryModule
  ],
  exports: [PositionsOverviewComponent],
})
export class PositionsOverviewModule { }

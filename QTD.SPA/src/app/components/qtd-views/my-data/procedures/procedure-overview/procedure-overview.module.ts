import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ProcedureOverviewComponent } from './procedure-overview.component';
import { MatLegacyTableModule as MatTableModule } from '@angular/material/legacy-table';
import { MatLegacyPaginatorModule as MatPaginatorModule } from '@angular/material/legacy-paginator';
import { MatSortModule } from '@angular/material/sort';
import { BaseModule } from 'src/app/components/base/base.module';
import { MatLegacyMenuModule as MatMenuModule } from '@angular/material/legacy-menu';
import { RouterModule, Routes } from '@angular/router';
import { FlyPanelIssuingAuthorityModule } from '../fly-panel-add-issuing-authority/fly-panel-add-issuing-authority.module';
import { FlyPanelProcedureNotLinkedModule } from '../fly-panel-procedure-not-linked/fly-panel-procedure-not-linked.module';
import { FlyPanelProceduresHistoryModule } from '../fly-panel-procedures-history/fly-panel-procedures-history.module';

const routes: Routes = [
  {
    path: '',
    component: ProcedureOverviewComponent,
  },
];

@NgModule({
  declarations: [ProcedureOverviewComponent],
  imports: [
    CommonModule,
    MatTableModule,
    MatPaginatorModule,
    MatSortModule,
    BaseModule,
    RouterModule.forChild(routes),
    FlyPanelProcedureNotLinkedModule,
    FlyPanelProceduresHistoryModule,
  ],
  exports: [ProcedureOverviewComponent],
})
export class ProcedureOverviewModule {}

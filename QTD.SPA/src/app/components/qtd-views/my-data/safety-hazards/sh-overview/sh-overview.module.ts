import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ShOverviewComponent } from './sh-overview.component';
import { MatLegacyPaginatorModule as MatPaginatorModule } from '@angular/material/legacy-paginator';
import { MatSortModule } from '@angular/material/sort';
import { MatLegacyTableModule as MatTableModule } from '@angular/material/legacy-table';
import { RouterModule, Routes } from '@angular/router';
import { BaseModule } from 'src/app/components/base/base.module';
import { FlyPanelShNotLinkedModule } from '../fly-panel-sh-not-linked/fly-panel-sh-not-linked.module';
import { FlypanelAddSafetyHazardsModule } from '../flypanel-add-safety-hazards/flypanel-add-safety-hazards.module';
import { FlyPanelShCategoryModule } from '../fly-panel-sh-category/fly-panel-sh-category.module';
import { FlyPanelShHistoryModule } from '../fly-panel-sh-history/fly-panel-sh-history.module';

const routes: Routes = [
  {
    path: '',
    component: ShOverviewComponent,
  },
];

@NgModule({
  declarations: [ShOverviewComponent],
  imports: [
    CommonModule,
    MatTableModule,
    MatPaginatorModule,
    MatSortModule,
    BaseModule,
    RouterModule.forChild(routes),
    FlyPanelShNotLinkedModule,
    FlyPanelShHistoryModule,
  ],
})
export class ShOverviewModule {}

import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { EnablingObjectivesOverviewComponent } from './enabling-objectives-overview.component';
import { RouterModule, Routes } from '@angular/router';
import { BaseModule } from 'src/app/components/base/base.module';
import { MatLegacyTableModule as MatTableModule } from '@angular/material/legacy-table';
import { MatLegacyPaginatorModule as MatPaginatorModule } from '@angular/material/legacy-paginator';
import { FlypanelEoHistoryModule } from '../flypanel-eo-history/flypanel-eo-history.module';
import { FlypanelEoNotLinkedModule } from '../flypanel-eo-not-linked/flypanel-eo-not-linked.module';
import { MatSortModule } from '@angular/material/sort';

const routes:Routes = [
  {
    path: '',
    component : EnablingObjectivesOverviewComponent,
  }
]

@NgModule({
  declarations: [
    EnablingObjectivesOverviewComponent
  ],
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    BaseModule,
    MatTableModule,
    MatPaginatorModule,
    FlypanelEoHistoryModule,
    FlypanelEoNotLinkedModule,
    MatSortModule,
  ],
  exports : [
    EnablingObjectivesOverviewComponent
  ]
})
export class EnablingObjectivesOverviewModule { }

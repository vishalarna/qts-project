import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TaskEOLinkComponent } from './task-eo-link.component';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule, Routes } from '@angular/router';
import { NgbDropdownModule } from '@ng-bootstrap/ng-bootstrap';
import { DataTablesModule } from 'angular-datatables';
import { LocalizeModule } from 'src/app/_Shared/modules/localize.module';

import { FormsModule } from '@angular/forms';
import { LayoutModule } from '../../../layout/layout.module';
import { MatExpansionModule } from '@angular/material/expansion';
import { MatIconModule } from '@angular/material/icon';
import { MatLegacyMenuModule as MatMenuModule } from '@angular/material/legacy-menu';
import { FlyPanelEnablingObjectiveModule } from '../../enabling-objective/fly-panel-enabling-objective/fly-panel-enabling-objective.module';
import { MatLegacyTableModule as MatTableModule } from '@angular/material/legacy-table';
import { MatSortModule } from '@angular/material/sort';
import { MatLegacyPaginatorModule as MatPaginatorModule } from '@angular/material/legacy-paginator';
import { BaseModule } from 'src/app/components/base/base.module';

const routes: Routes = [
  {
    path: '',
    component: TaskEOLinkComponent,
  },
];

@NgModule({
  declarations: [TaskEOLinkComponent],
  imports: [
    CommonModule,
    HttpClientModule,
    LocalizeModule,
    FormsModule,
    LayoutModule,
    RouterModule.forChild(routes),
    NgbDropdownModule,
    DataTablesModule,
    MatExpansionModule,
    MatMenuModule,
    MatIconModule,
    FlyPanelEnablingObjectiveModule,
    MatTableModule,
    MatSortModule,
    MatPaginatorModule,
    BaseModule,
  ],
})
export class TaskEOLinkModule {}

import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TaskSaftyHazardLinkComponent } from './task-safty-hazard-link.component';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule, Routes } from '@angular/router';
import { NgbDropdownModule } from '@ng-bootstrap/ng-bootstrap';
import { DataTablesModule } from 'angular-datatables';
import { LocalizeModule } from 'src/app/_Shared/modules/localize.module';

import { FormsModule } from '@angular/forms';
import { LayoutModule } from '../../../layout/layout.module';
import { FlyPanelSafetyHazardModule } from '../../safety-hazard/fly-panel-safety-hazard/fly-panel-safety-hazard.module';
import { MatLegacyPaginatorModule as MatPaginatorModule } from '@angular/material/legacy-paginator';
import { MatSortModule } from '@angular/material/sort';
import { MatLegacyTableModule as MatTableModule } from '@angular/material/legacy-table';
import { BaseModule } from 'src/app/components/base/base.module';

const routes: Routes = [
  {
    path: '',
    component: TaskSaftyHazardLinkComponent,
  },
];

@NgModule({
  declarations: [TaskSaftyHazardLinkComponent],
  imports: [
    CommonModule,
    HttpClientModule,
    LocalizeModule,
    FormsModule,

    RouterModule.forChild(routes),
    NgbDropdownModule,
    DataTablesModule,
    LayoutModule,
    FlyPanelSafetyHazardModule,
    MatTableModule,
    MatSortModule,
    MatPaginatorModule,
    BaseModule,
  ],
})
export class TaskSaftyHazardLinkModule {}

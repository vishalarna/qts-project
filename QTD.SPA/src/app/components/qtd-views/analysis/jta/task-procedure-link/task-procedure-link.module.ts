import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TaskProcedureLinkComponent } from './task-procedure-link.component';
import { HttpClientModule } from '@angular/common/http';
import { Routes, RouterModule } from '@angular/router';
import { NgbDropdownModule } from '@ng-bootstrap/ng-bootstrap';
import { DataTablesModule } from 'angular-datatables';
import { LocalizeModule } from 'src/app/_Shared/modules/localize.module';

import { FormsModule } from '@angular/forms';
import { LayoutModule } from '../../../layout/layout.module';
import { FlypanelProcedureModule } from '../../procedure/flypanel-procedure/flypanel-procedure.module';
import { MatLegacyPaginatorModule as MatPaginatorModule } from '@angular/material/legacy-paginator';
import { MatSortModule } from '@angular/material/sort';
import { MatLegacyTableModule as MatTableModule } from '@angular/material/legacy-table';
import { BaseModule } from 'src/app/components/base/base.module';

const routes: Routes = [
  {
    path: '',
    component: TaskProcedureLinkComponent,
  },
];

@NgModule({
  declarations: [TaskProcedureLinkComponent],
  imports: [
    CommonModule,
    FormsModule,
    HttpClientModule,
    LocalizeModule,
    RouterModule.forChild(routes),
    NgbDropdownModule,
    DataTablesModule,
    LayoutModule,
    FlypanelProcedureModule,
    MatTableModule,
    MatSortModule,
    MatPaginatorModule,
    BaseModule,
  ],
})
export class TaskProcedureLinkModule {}

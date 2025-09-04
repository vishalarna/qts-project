import { ActiveInactiveDialogueModule } from './../active-inactive-dialogue/active-inactive-dialogue.module';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ListEmpsComponent } from './list-emps.component';
import { MatLegacyTableModule as MatTableModule } from '@angular/material/legacy-table';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule, Routes } from '@angular/router';
import { NgbDropdownModule } from '@ng-bootstrap/ng-bootstrap';
import { DataTablesModule } from 'angular-datatables';
import { LocalizeModule } from 'src/app/_Shared/modules/localize.module';

import { FormsModule } from '@angular/forms';
import { LayoutModule } from '../../../layout/layout.module';
import { MatLegacyPaginatorModule as MatPaginatorModule } from '@angular/material/legacy-paginator';
import { MatSortModule } from '@angular/material/sort';
import { MatLegacySelectModule as MatSelectModule } from '@angular/material/legacy-select'
import { MatLegacyMenuModule as MatMenuModule } from '@angular/material/legacy-menu';
import { BaseModule } from 'src/app/components/base/base.module';
import { CdkColumnDef } from '@angular/cdk/table';
import { FlyPanelEmpListModule } from '../fly-panel-emp-list/fly-panel-emp-list.module';

const routes: Routes = [
  {
    path: '',
    component: ListEmpsComponent,
  },
];

@NgModule({
  declarations: [ListEmpsComponent],
  imports: [
    CommonModule,
    HttpClientModule,
    RouterModule.forChild(routes),
    LocalizeModule,
    NgbDropdownModule,
    DataTablesModule,
    FormsModule,
    LayoutModule,
    MatTableModule,
    MatPaginatorModule,
    MatSortModule,
    MatMenuModule,
    BaseModule,
    ActiveInactiveDialogueModule,
    FlyPanelEmpListModule,
    MatSelectModule,
  ],
})
export class ListEmpsModule {}

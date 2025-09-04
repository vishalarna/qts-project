import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ListClientsComponent } from './list-clients.component';
import { RouterModule, Routes } from '@angular/router';
import { LocalizeModule } from 'src/app/_Shared/modules/localize.module';
import { AddEditClientComponent } from './add-edit-client/add-edit-client.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgbDropdownModule } from '@ng-bootstrap/ng-bootstrap';
import { DataTablesModule } from 'angular-datatables';

import { LayoutModule } from '../../layout/layout.module';
import { MatTableModule } from '@angular/material/table';
import { MatSortModule } from '@angular/material/sort';
import { MatPaginatorModule } from '@angular/material/paginator';
import { BaseModule } from 'src/app/components/base/base.module';
import { MatMenuModule } from '@angular/material/menu';
import { MatDialogModule } from '@angular/material/dialog';

const routes: Routes = [
  {
    path: '',

    component: ListClientsComponent,
  },
];

@NgModule({
  declarations: [ListClientsComponent, AddEditClientComponent],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    RouterModule.forChild(routes),
    LocalizeModule,
    NgbDropdownModule,
    DataTablesModule,
    LayoutModule,
    MatTableModule,
    MatSortModule,
    MatPaginatorModule,
    BaseModule,
    MatMenuModule,
    MatDialogModule,
  ],
})
export class ListClientsModule {}

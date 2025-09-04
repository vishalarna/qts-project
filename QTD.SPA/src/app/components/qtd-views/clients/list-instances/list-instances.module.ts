import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ListInstancesComponent } from './list-instances.component';
import { AddEditInstanceComponent } from './add-edit-instance/add-edit-instance.component';
import { ReactiveFormsModule } from '@angular/forms';
import { RouterModule, Routes } from '@angular/router';
import { NgbDropdownModule } from '@ng-bootstrap/ng-bootstrap';
import { DataTablesModule } from 'angular-datatables';
import { LocalizeModule } from 'src/app/_Shared/modules/localize.module';

import { LayoutModule } from '../../layout/layout.module';
import { BaseModule } from 'src/app/components/base/base.module';
import { MatDialogModule } from '@angular/material/dialog';
import { MatMenuModule } from '@angular/material/menu';
import { MatTableModule } from '@angular/material/table';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatSortModule } from '@angular/material/sort';

const routes: Routes = [
  {
    path: '',
    component: ListInstancesComponent,
  },
];

@NgModule({
  declarations: [ListInstancesComponent, AddEditInstanceComponent],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    RouterModule.forChild(routes),
    LocalizeModule,
    NgbDropdownModule,
    DataTablesModule,
    LayoutModule,
    BaseModule,
    MatDialogModule,
    MatMenuModule,
    MatTableModule,
    MatPaginatorModule,
    MatSortModule,
  ],
})
export class ListInstancesModule {}

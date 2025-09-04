import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { TaskDetailComponent } from './task-detail.component';
import { RouterModule, Routes } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';
import { LocalizeModule } from 'src/app/_Shared/modules/localize.module';
import { NgbDropdownModule } from '@ng-bootstrap/ng-bootstrap';
import { DataTablesModule } from 'angular-datatables';
import { FormsModule } from '@angular/forms';
import { LayoutModule } from '../../../layout/layout.module';
import { MatLegacyTableModule as MatTableModule } from '@angular/material/legacy-table';
import { MatLegacyMenuModule as MatMenuModule } from '@angular/material/legacy-menu';
import { BaseModule } from 'src/app/components/base/base.module';
import { MatLegacyTabsModule as MatTabsModule } from '@angular/material/legacy-tabs';
import { MatSortModule } from '@angular/material/sort';
import { MatLegacyPaginatorModule as MatPaginatorModule } from '@angular/material/legacy-paginator';

const routes: Routes = [
  {
    path: '',
    component: TaskDetailComponent,
  },
];

@NgModule({
  declarations: [TaskDetailComponent],
  imports: [
    CommonModule,
    HttpClientModule,
    LocalizeModule,
    FormsModule,
    RouterModule.forChild(routes),
    NgbDropdownModule,
    LayoutModule,
    MatTableModule,
    MatTabsModule,
    MatMenuModule,
    MatSortModule,
    MatPaginatorModule,
    BaseModule,
  ],
})
export class TaskDetailModule {}

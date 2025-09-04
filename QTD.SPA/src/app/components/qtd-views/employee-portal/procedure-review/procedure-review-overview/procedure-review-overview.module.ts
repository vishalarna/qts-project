import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ProcedureReviewOverviewComponent } from './procedure-review-overview.component';
import { BaseModule } from 'src/app/components/base/base.module';
import { LayoutModule } from '../../../layout/layout.module';
import { MatButtonToggleModule } from '@angular/material/button-toggle';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { MatLegacyChipsModule as MatChipsModule } from '@angular/material/legacy-chips';
import { MatLegacyDialogModule as MatDialogModule } from '@angular/material/legacy-dialog';
import { MatLegacyMenuModule as MatMenuModule } from '@angular/material/legacy-menu';
import { MatLegacyPaginatorModule as MatPaginatorModule } from '@angular/material/legacy-paginator';
import { MatLegacySelectModule as MatSelectModule } from '@angular/material/legacy-select';
import { MatSortModule } from '@angular/material/sort';
import { MatLegacyTableModule as MatTableModule } from '@angular/material/legacy-table';
import { MatLegacyTabsModule as MatTabsModule } from '@angular/material/legacy-tabs';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  {
    path: '',
    component: ProcedureReviewOverviewComponent,
    
  },
];

@NgModule({
  declarations: [
    ProcedureReviewOverviewComponent
  ],
  imports: [
    CommonModule,
    MatTableModule,
    RouterModule.forChild(routes),
    MatPaginatorModule,
    MatSortModule,
    BaseModule,
    LayoutModule,
    MatMenuModule,
    MatCheckboxModule,
    MatSelectModule,
    MatButtonToggleModule,
    MatDialogModule,
    MatTabsModule,
    MatChipsModule,
  ]
})
export class ProcedureReviewOverviewModule { }

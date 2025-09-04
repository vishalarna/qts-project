import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TestOverviewComponent } from './test-overview.component';
import { MatLegacyTableModule as MatTableModule } from '@angular/material/legacy-table';
import { MatLegacyPaginatorModule as MatPaginatorModule } from '@angular/material/legacy-paginator';
import { MatSortModule } from '@angular/material/sort';
import { BaseModule } from 'src/app/components/base/base.module';
import { LayoutModule } from '../../../layout/layout.module';
import { MatButtonToggleModule } from '@angular/material/button-toggle';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { MatLegacyDialogModule as MatDialogModule } from '@angular/material/legacy-dialog';
import { MatLegacyMenuModule as MatMenuModule } from '@angular/material/legacy-menu';
import { MatLegacySelectModule as MatSelectModule } from '@angular/material/legacy-select';
import { RouterModule, Routes } from '@angular/router';
import { MatLegacyTabsModule as MatTabsModule } from '@angular/material/legacy-tabs';
import { MatLegacyChipsModule as MatChipsModule } from '@angular/material/legacy-chips';
import { MatLegacyTooltipModule as MatTooltipModule } from '@angular/material/legacy-tooltip';


const routes: Routes = [
  {
    path: '',
    component: TestOverviewComponent,

  },
];
@NgModule({
  declarations: [
    TestOverviewComponent
  ],
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    MatPaginatorModule,
    BaseModule,
    LayoutModule,
    MatMenuModule,
    MatCheckboxModule,
    MatSelectModule,
    MatButtonToggleModule,
    MatDialogModule,
    MatTabsModule,
    MatChipsModule,
    MatTableModule,
    MatSortModule,
    MatTooltipModule,

  ]
})
export class TestOverviewModule { }

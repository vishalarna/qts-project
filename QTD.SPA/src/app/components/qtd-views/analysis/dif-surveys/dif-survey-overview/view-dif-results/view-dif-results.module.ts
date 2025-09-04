import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BaseModule } from 'src/app/components/base/base.module';
import { RouterModule, Routes } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { MatLegacyTableModule as MatTableModule } from '@angular/material/legacy-table';
import { MatLegacyPaginatorModule as MatPaginatorModule } from '@angular/material/legacy-paginator';
import { MatSortModule } from '@angular/material/sort';
import { MatLegacyMenuModule as MatMenuModule } from '@angular/material/legacy-menu';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { MatIconModule } from '@angular/material/icon';
import { ViewDifResultsComponent } from './view-dif-results.component';
import { LayoutModule } from 'src/app/components/qtd-views/layout/layout.module';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatLegacySelectModule as MatSelectModule } from '@angular/material/legacy-select';

const routes: Routes = [
  {
    path: '',
    component: ViewDifResultsComponent,
  }
]

@NgModule({
  declarations: [ViewDifResultsComponent],
  imports: [
    CommonModule,
    BaseModule,
    RouterModule.forChild(routes),
    LayoutModule,
    FormsModule,
    MatTableModule,
    MatPaginatorModule,
    MatSortModule,
    MatMenuModule,
    MatCheckboxModule,
    MatIconModule,
    MatToolbarModule,
    MatSelectModule
  ],
   exports:[ViewDifResultsComponent]
})
export class ViewDifResultsModule { }

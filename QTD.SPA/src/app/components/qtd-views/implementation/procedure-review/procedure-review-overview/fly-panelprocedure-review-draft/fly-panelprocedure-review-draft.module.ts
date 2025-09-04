import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FlyPanelprocedureReviewDraftComponent } from './fly-panelprocedure-review-draft.component';
import { BaseModule } from 'src/app/components/base/base.module';
import { LayoutModule } from 'src/app/components/qtd-views/layout/layout.module';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatSortModule } from '@angular/material/sort';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { DataTablesModule } from 'angular-datatables';
import { MatLegacyTableModule as MatTableModule } from '@angular/material/legacy-table';
import { MatLegacyPaginatorModule as MatPaginatorModule } from '@angular/material/legacy-paginator';



@NgModule({
  declarations: [
    FlyPanelprocedureReviewDraftComponent
  ],
  imports: [
    CommonModule,
    BaseModule,
    MatCheckboxModule,
    LayoutModule,
    MatToolbarModule,
    MatSortModule,
    DataTablesModule,
    MatTableModule,
    MatPaginatorModule
  ],
  exports:[FlyPanelprocedureReviewDraftComponent]
})
export class FlyPanelprocedureReviewDraftModule { }

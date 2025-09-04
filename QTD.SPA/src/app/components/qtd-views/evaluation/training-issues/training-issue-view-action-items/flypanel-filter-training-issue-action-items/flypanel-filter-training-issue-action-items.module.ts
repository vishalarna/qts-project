import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BaseModule } from 'src/app/components/base/base.module';
import { RouterModule, Routes } from '@angular/router';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { MatLegacyTableModule as MatTableModule } from '@angular/material/legacy-table';
import { MatLegacyPaginatorModule as MatPaginatorModule } from '@angular/material/legacy-paginator';
import { MatSortModule } from '@angular/material/sort';
import { MatLegacyMenuModule as MatMenuModule } from '@angular/material/legacy-menu';
import { MatToolbarModule } from '@angular/material/toolbar';
import { FlypanelFilterTrainingIssueActionItemsComponent } from './flypanel-filter-training-issue-action-items.component';
import { LayoutModule } from 'src/app/components/qtd-views/layout/layout.module';
import { ReactiveFormsModule } from '@angular/forms';
import { MatLegacySelectModule as MatSelectModule } from '@angular/material/legacy-select';
import { MatIconModule } from '@angular/material/icon';


@NgModule({
  declarations: [FlypanelFilterTrainingIssueActionItemsComponent],
  imports: [
    CommonModule,
    LayoutModule,
    BaseModule,
    MatCheckboxModule,
    MatTableModule,
    MatPaginatorModule,
    MatSortModule,
    MatMenuModule,
    MatToolbarModule,
    ReactiveFormsModule,
    MatSelectModule,
    MatIconModule
  ],
  exports:[FlypanelFilterTrainingIssueActionItemsComponent]
})
export class FlypanelFilterTrainingIssueActionItemsModule { }

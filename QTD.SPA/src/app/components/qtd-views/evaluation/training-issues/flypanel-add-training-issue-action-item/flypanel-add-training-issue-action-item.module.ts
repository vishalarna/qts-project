import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LayoutModule } from '../../../layout/layout.module';
import { BaseModule } from 'src/app/components/base/base.module';
import { RouterModule, Routes } from '@angular/router';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { MatLegacyTableModule as MatTableModule } from '@angular/material/legacy-table';
import { MatLegacyPaginatorModule as MatPaginatorModule } from '@angular/material/legacy-paginator';
import { MatSortModule } from '@angular/material/sort';
import { MatLegacyMenuModule as MatMenuModule } from '@angular/material/legacy-menu';
import { FlypanelAddTrainingIssueActionItemComponent } from './flypanel-add-training-issue-action-item.component';
import { MatLegacySelectModule as MatSelectModule } from '@angular/material/legacy-select';
import { ReactiveFormsModule } from '@angular/forms';


@NgModule({
  declarations: [FlypanelAddTrainingIssueActionItemComponent],
  imports: [
    CommonModule,
    LayoutModule,
    BaseModule,
    MatCheckboxModule,
    MatTableModule,
    MatPaginatorModule,
    MatSortModule,
    MatMenuModule,
    MatSelectModule,
    ReactiveFormsModule,
  ],
  exports:[FlypanelAddTrainingIssueActionItemComponent]
})
export class FlypanelAddTrainingIssueActionItemModule { }

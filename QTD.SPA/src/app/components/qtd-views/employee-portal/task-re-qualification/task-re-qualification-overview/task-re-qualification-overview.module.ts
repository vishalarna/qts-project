import { BaseModule } from 'src/app/components/base/base.module';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { qualification } from './task-re-qualification-overview.component';
import { MatLegacyTableModule as MatTableModule } from '@angular/material/legacy-table';
import { LayoutModule } from '../../../layout/layout.module';
import { MatButtonToggleModule } from '@angular/material/button-toggle';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { MatLegacyChipsModule as MatChipsModule } from '@angular/material/legacy-chips';
import { MatLegacyDialogModule as MatDialogModule } from '@angular/material/legacy-dialog';
import { MatLegacyMenuModule as MatMenuModule } from '@angular/material/legacy-menu';
import { MatLegacyPaginatorModule as MatPaginatorModule } from '@angular/material/legacy-paginator';
import { MatLegacySelectModule as MatSelectModule } from '@angular/material/legacy-select';
import { MatSortModule } from '@angular/material/sort';
import { MatLegacyTabsModule as MatTabsModule } from '@angular/material/legacy-tabs';
import { RouterModule, Routes } from '@angular/router';
import { MatIconModule } from '@angular/material/icon';
import { FormsModule } from '@angular/forms';
import { MatLegacyTooltipModule as MatTooltipModule } from '@angular/material/legacy-tooltip';
import { TaskSortPipePipe } from 'src/app/_Pipes/task-sort-pipe.pipe';

const routes: Routes = [
  {
    path: '',
    component: qualification,
    
  },
];

@NgModule({
  declarations: [
    qualification
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
    MatIconModule,
    FormsModule,
    MatTooltipModule
  ]
})
export class TaskReQualificationOverviewModule { }

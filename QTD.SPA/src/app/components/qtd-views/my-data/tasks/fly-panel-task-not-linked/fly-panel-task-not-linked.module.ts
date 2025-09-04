import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FlyPanelTaskNotLinkedComponent } from './fly-panel-task-not-linked.component';
import { FormsModule } from '@angular/forms';
import { MatLegacyMenuModule as MatMenuModule } from '@angular/material/legacy-menu';
import { MatTreeModule } from '@angular/material/tree';
import { BaseModule } from 'src/app/components/base/base.module';
import { MatLegacyTableModule as MatTableModule } from '@angular/material/legacy-table';
import { MatLegacyPaginatorModule as MatPaginatorModule } from '@angular/material/legacy-paginator';
import { MatSortModule } from '@angular/material/sort';

@NgModule({
  declarations: [FlyPanelTaskNotLinkedComponent],
  imports: [
    CommonModule,
    MatMenuModule,
    FormsModule,
    MatTreeModule,
    BaseModule,
    MatTableModule,
    MatPaginatorModule,
    MatSortModule,
  ],
  exports: [FlyPanelTaskNotLinkedComponent],
})
export class FlyPanelTaskNotLinkedModule {}

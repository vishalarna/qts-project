import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FlyPanelProcedureNotLinkedComponent } from './fly-panel-procedure-not-linked.component';
import { BaseModule } from 'src/app/components/base/base.module';
import { FormsModule } from '@angular/forms';
import { MatTreeModule } from '@angular/material/tree';
import { MatLegacyMenuModule as MatMenuModule } from '@angular/material/legacy-menu';
import { MatLegacyPaginatorModule as MatPaginatorModule } from '@angular/material/legacy-paginator';
import { MatLegacyTableModule as MatTableModule } from '@angular/material/legacy-table';
import { MatSortModule } from '@angular/material/sort';

@NgModule({
  declarations: [FlyPanelProcedureNotLinkedComponent],
  imports: [
    CommonModule,
    BaseModule,
    FormsModule,
    MatTreeModule,
    MatMenuModule,
    MatPaginatorModule,
    MatTableModule,
    MatSortModule,
  ],
  exports: [FlyPanelProcedureNotLinkedComponent],
})
export class FlyPanelProcedureNotLinkedModule {}

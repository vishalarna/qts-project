import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FlyPanelProceduresHistoryComponent } from './fly-panel-procedures-history.component';
import { MatLegacyPaginatorModule as MatPaginatorModule } from '@angular/material/legacy-paginator';
import { MatSortModule } from '@angular/material/sort';
import { MatLegacyTableModule as MatTableModule } from '@angular/material/legacy-table';
import { BaseModule } from 'src/app/components/base/base.module';
import { FormsModule } from '@angular/forms';

@NgModule({
  declarations: [FlyPanelProceduresHistoryComponent],
  imports: [
    CommonModule,
    MatPaginatorModule,
    MatSortModule,
    MatTableModule,
    BaseModule,
    FormsModule
  ],
  exports: [FlyPanelProceduresHistoryComponent],
})
export class FlyPanelProceduresHistoryModule {}

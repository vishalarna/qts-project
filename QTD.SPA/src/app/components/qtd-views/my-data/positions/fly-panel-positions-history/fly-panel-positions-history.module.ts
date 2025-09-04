import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FlyPanelPositionsHistoryComponent } from './fly-panel-positions-history.component';
import { MatLegacyPaginatorModule as MatPaginatorModule } from '@angular/material/legacy-paginator';
import { MatSortModule } from '@angular/material/sort';
import { MatLegacyTableModule as MatTableModule } from '@angular/material/legacy-table';
import { BaseModule } from 'src/app/components/base/base.module';
import { FormsModule } from '@angular/forms';



@NgModule({
  declarations: [FlyPanelPositionsHistoryComponent],
  imports: [
    CommonModule,
    MatPaginatorModule,
    MatSortModule,
    MatTableModule,
    BaseModule,
    FormsModule
  ],
  exports: [FlyPanelPositionsHistoryComponent]
})
export class FlyPanelPositionsHistoryModule { }

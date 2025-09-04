import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FlyPanelViewLocationHistoryComponent } from './fly-panel-view-location-history.component';
import { MatLegacyPaginatorModule as MatPaginatorModule } from '@angular/material/legacy-paginator';
import { MatSortModule } from '@angular/material/sort';
import { MatLegacyTableModule as MatTableModule } from '@angular/material/legacy-table';
import { BaseModule } from 'src/app/components/base/base.module';
import { FormsModule } from '@angular/forms';




@NgModule({
  declarations: [
    FlyPanelViewLocationHistoryComponent
  ],
  imports: [
    CommonModule,
    MatPaginatorModule,
    MatSortModule,
    MatTableModule,
    BaseModule,
    FormsModule
  ],
  exports: [FlyPanelViewLocationHistoryComponent],
})
export class FlyPanelViewLocationHistoryModule { }

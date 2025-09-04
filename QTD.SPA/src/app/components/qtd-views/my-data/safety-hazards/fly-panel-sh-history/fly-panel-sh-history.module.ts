import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatLegacyTableModule as MatTableModule } from '@angular/material/legacy-table';
import { MatLegacyPaginatorModule as MatPaginatorModule } from '@angular/material/legacy-paginator';
import { FlyPanelShHistoryComponent } from './fly-panel-sh-history.component';
import { MatSortModule } from '@angular/material/sort';
import { BaseModule } from 'src/app/components/base/base.module';
import { FormsModule } from '@angular/forms';



@NgModule({
  declarations: [FlyPanelShHistoryComponent],
  imports: [
    CommonModule,
    MatPaginatorModule,
    MatSortModule,
    MatTableModule,
    BaseModule,
    FormsModule
  ],
  exports: [FlyPanelShHistoryComponent]
})
export class FlyPanelShHistoryModule { }

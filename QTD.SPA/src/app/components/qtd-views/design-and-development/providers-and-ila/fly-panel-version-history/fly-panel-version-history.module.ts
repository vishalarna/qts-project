import { ReactiveFormsModule } from '@angular/forms';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FlyPanelVersionHistoryComponent } from './fly-panel-version-history.component';
import { BaseModule } from 'src/app/components/base/base.module';
import { MatExpansionModule } from '@angular/material/expansion';
import { MatLegacyTableModule as MatTableModule } from '@angular/material/legacy-table';
import { MatLegacyPaginatorModule as MatPaginatorModule } from '@angular/material/legacy-paginator';
import { MatSortModule } from '@angular/material/sort';
import { FlyPanelVersionHistoryDetailsModule } from './fly-panel-version-history-details/fly-panel-version-history-details.module';


@NgModule({
  declarations: [
    FlyPanelVersionHistoryComponent
  ],
  imports: [
    CommonModule,
    BaseModule,
    ReactiveFormsModule,
    MatExpansionModule,
    MatTableModule,
    MatPaginatorModule,
    MatSortModule,
    FlyPanelVersionHistoryDetailsModule
  ],
  exports:[
    FlyPanelVersionHistoryComponent
  ]
})
export class FlyPanelVersionHistoryModule { }

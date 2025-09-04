import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FlyPanelActiveInactiveListComponent } from './fly-panel-active-inactive-list.component';
import { BaseModule } from 'src/app/components/base/base.module';
import { MatLegacyPaginatorModule as MatPaginatorModule } from '@angular/material/legacy-paginator';
import { MatSortModule } from '@angular/material/sort';
import { MatLegacyTableModule as MatTableModule } from '@angular/material/legacy-table';

@NgModule({
  declarations: [
    FlyPanelActiveInactiveListComponent
  ],
  imports: [
    CommonModule,
    BaseModule,
    MatPaginatorModule,
    MatSortModule,
    MatTableModule
  ],
  exports:[FlyPanelActiveInactiveListComponent]
})
export class FlyPanelActiveInactiveListModule { }

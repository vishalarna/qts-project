import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FlyPanelEmpListComponent } from './fly-panel-emp-list.component';
import { BaseModule } from 'src/app/components/base/base.module';
import { MatLegacyPaginatorModule as MatPaginatorModule } from '@angular/material/legacy-paginator';
import { MatSortModule } from '@angular/material/sort';
import { MatLegacyTableModule as MatTableModule } from '@angular/material/legacy-table';




@NgModule({
  declarations: [
    FlyPanelEmpListComponent
  ],
  imports: [
    CommonModule,
    BaseModule,
    MatPaginatorModule,
    MatSortModule,
    MatTableModule
  ],
  exports:[FlyPanelEmpListComponent]
})
export class FlyPanelEmpListModule { }

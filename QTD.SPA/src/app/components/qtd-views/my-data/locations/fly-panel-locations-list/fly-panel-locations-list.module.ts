import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FlyPanelLocationsListComponent } from './fly-panel-locations-list.component';
import { BaseModule } from 'src/app/components/base/base.module';
import { MatLegacyTableModule as MatTableModule } from '@angular/material/legacy-table';
import { MatLegacyPaginatorModule as MatPaginatorModule } from '@angular/material/legacy-paginator';
import { MatSortModule } from '@angular/material/sort';
import { MatTreeModule } from '@angular/material/tree';



@NgModule({
  declarations: [
    FlyPanelLocationsListComponent
  ],
  imports: [
    CommonModule,
    BaseModule,
    MatTableModule,
    MatPaginatorModule,
    MatSortModule,
    MatTreeModule
  ],
  exports:[FlyPanelLocationsListComponent]
})
export class FlyPanelLocationsListModule { }

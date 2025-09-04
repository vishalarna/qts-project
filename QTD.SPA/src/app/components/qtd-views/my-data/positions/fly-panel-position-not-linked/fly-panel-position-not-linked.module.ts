import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FlyPanelPositionNotLinkedComponent } from './fly-panel-position-not-linked.component';
import { FormsModule } from '@angular/forms';
import { MatLegacyMenuModule as MatMenuModule } from '@angular/material/legacy-menu';
import { MatTreeModule } from '@angular/material/tree';
import { BaseModule } from 'src/app/components/base/base.module';
import { MatLegacyTableModule as MatTableModule } from '@angular/material/legacy-table';
import { MatLegacyPaginatorModule as MatPaginatorModule } from '@angular/material/legacy-paginator';
import { MatSortModule } from '@angular/material/sort';



@NgModule({
  declarations: [
    FlyPanelPositionNotLinkedComponent
  ],
  imports: [
    CommonModule,
    BaseModule,
    FormsModule,
    MatTreeModule,
    MatMenuModule,
    MatTableModule,
    MatPaginatorModule,
    MatSortModule,
  ],

  exports:[FlyPanelPositionNotLinkedComponent]
})
export class FlyPanelPositionNotLinkedModule { }

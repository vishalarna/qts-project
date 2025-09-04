import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FlypanelEoNotLinkedComponent } from './flypanel-eo-not-linked.component';
import { BaseModule } from 'src/app/components/base/base.module';
import { MatTreeModule } from '@angular/material/tree';
import { FormsModule } from '@angular/forms';
import { MatLegacyMenuModule as MatMenuModule } from '@angular/material/legacy-menu';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { MatLegacyTableModule as MatTableModule } from '@angular/material/legacy-table';
import { MatLegacyPaginatorModule as MatPaginatorModule } from '@angular/material/legacy-paginator';
import { MatSortModule } from '@angular/material/sort';



@NgModule({
  declarations: [
    FlypanelEoNotLinkedComponent
  ],
  imports: [
    CommonModule,
    BaseModule,
    MatTreeModule,
    FormsModule,
    MatMenuModule,
    MatCheckboxModule,
    MatTableModule,
    MatPaginatorModule,
    MatSortModule,
  ],
  exports : [
    FlypanelEoNotLinkedComponent,
  ]
})
export class FlypanelEoNotLinkedModule { }

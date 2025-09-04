import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FlypanelEoHistoryComponent } from './flypanel-eo-history.component';
import { BaseModule } from 'src/app/components/base/base.module';
import { MatLegacyTableModule as MatTableModule } from '@angular/material/legacy-table';
import { MatLegacyPaginatorModule as MatPaginatorModule } from '@angular/material/legacy-paginator';
import { MatIconModule } from '@angular/material/icon';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { MatSortModule } from '@angular/material/sort';
import { FormsModule } from '@angular/forms';



@NgModule({
  declarations: [
    FlypanelEoHistoryComponent
  ],
  imports: [
    CommonModule,
    BaseModule,
    MatTableModule,
    MatPaginatorModule,
    MatIconModule,
    MatCheckboxModule,
    MatSortModule,
    FormsModule
  ],
  exports : [
    FlypanelEoHistoryComponent
  ]
})
export class FlypanelEoHistoryModule { }

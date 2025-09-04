import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TaskEmployeeComponent } from './task-employee.component';
import { MatLegacyPaginatorModule as MatPaginatorModule } from '@angular/material/legacy-paginator';
import { MatSortModule } from '@angular/material/sort';
import { MatLegacyTableModule as MatTableModule } from '@angular/material/legacy-table';
import { BaseModule } from 'src/app/components/base/base.module';

@NgModule({
  declarations: [TaskEmployeeComponent],
  imports: [
    CommonModule,
    BaseModule,
    MatTableModule,
    MatPaginatorModule,
    MatSortModule,
  ],
  exports: [TaskEmployeeComponent],
})
export class TaskEmployeeModule {}

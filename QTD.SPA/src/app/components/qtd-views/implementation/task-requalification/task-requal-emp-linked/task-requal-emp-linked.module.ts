import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TaskRequalEmpLinkedComponent } from './task-requal-emp-linked.component';
import { BaseModule } from 'src/app/components/base/base.module';
import { MatLegacyTableModule as MatTableModule } from '@angular/material/legacy-table';
import { MatLegacyMenuModule as MatMenuModule } from '@angular/material/legacy-menu';
import { MatIconModule } from '@angular/material/icon';
import { RouterModule, Routes } from '@angular/router';
import { LayoutModule } from '../../../layout/layout.module';
import { FlypanelTaskQualifiedModule } from '../flypanel-task-qualified/flypanel-task-qualified.module';
import { FlypanelAddTaskQualificationModule } from '../flypanel-add-task-qualification/flypanel-add-task-qualification.module';
import { FlypanelFilterTqEmpByModule } from '../flypanel-filter-tq-emp-by/flypanel-filter-tq-emp-by.module';
import { MatSortModule } from '@angular/material/sort';

const route:Routes = [
  {
    path:':id',
    component:TaskRequalEmpLinkedComponent,
  }
]

@NgModule({
  declarations: [
    TaskRequalEmpLinkedComponent
  ],
  imports: [
    CommonModule,
    BaseModule,
    MatTableModule,
    MatMenuModule,
    MatIconModule,
    LayoutModule,
    RouterModule.forChild(route),
    FlypanelTaskQualifiedModule,
    FlypanelAddTaskQualificationModule,
    FlypanelFilterTqEmpByModule,
    MatSortModule,
  ],
  exports: [
    TaskRequalEmpLinkedComponent,
  ]
})
export class TaskRequalEmpLinkedModule { }

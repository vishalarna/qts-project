import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { EoTasksComponent } from './eo-tasks.component';
import { BaseModule } from 'src/app/components/base/base.module';
import { MatLegacyTableModule as MatTableModule } from '@angular/material/legacy-table';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { MatLegacyPaginatorModule as MatPaginatorModule } from '@angular/material/legacy-paginator';
import { FlypanelEoTaskLinkModule } from '../../flypanel-eo-task-link/flypanel-eo-task-link.module';
import { FlypanelLinkedEosModule } from '../../flypanel-linked-eos/flypanel-linked-eos.module';
import { MatSortModule } from '@angular/material/sort';


@NgModule({
  declarations: [
    EoTasksComponent
  ],
  imports: [
    CommonModule,
    BaseModule,
    MatTableModule,
    MatCheckboxModule,
    MatPaginatorModule,
    FlypanelEoTaskLinkModule,
    FlypanelLinkedEosModule,
    MatSortModule
  ],
  exports : [EoTasksComponent]
})
export class EoTasksModule { }

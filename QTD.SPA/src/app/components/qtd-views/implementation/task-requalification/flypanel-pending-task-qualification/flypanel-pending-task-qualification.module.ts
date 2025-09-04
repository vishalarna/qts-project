import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BaseModule } from 'src/app/components/base/base.module';
import { FlypanelPendingTaskQualificationComponent } from './flypanel-pending-task-qualification.component';
import { MatLegacyTableModule as MatTableModule } from '@angular/material/legacy-table';
import { FlypanelTaskRequalNeededModule } from '../flypanel-task-requal-needed/flypanel-task-requal-needed.module';



@NgModule({
  declarations: [FlypanelPendingTaskQualificationComponent],
  imports: [
    CommonModule,
    BaseModule,
    MatTableModule,
    FlypanelTaskRequalNeededModule,
  ],
  exports: [
    FlypanelPendingTaskQualificationComponent
  ]
})
export class FlypanelPendingTaskQualificationModule { }

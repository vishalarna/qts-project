import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FlypanelWithoutTasksQualificationsComponent } from './flypanel-without-tasks-qualifications.component';
import { BaseModule } from 'src/app/components/base/base.module';
import { MatLegacyTableModule as MatTableModule } from '@angular/material/legacy-table';
import { FlypanelTaskRequalNeededModule } from '../flypanel-task-requal-needed/flypanel-task-requal-needed.module';



@NgModule({
  declarations: [FlypanelWithoutTasksQualificationsComponent],
  imports: [
    CommonModule,
    BaseModule,
    MatTableModule,
    FlypanelTaskRequalNeededModule,
  ],
  exports : [
    FlypanelWithoutTasksQualificationsComponent
  ]
})
export class FlypanelWithoutTasksQualificationsModule { }

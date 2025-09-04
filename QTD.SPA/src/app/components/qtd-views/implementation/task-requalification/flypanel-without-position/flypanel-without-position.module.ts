import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FlypanelWithoutPositionComponent } from './flypanel-without-position.component';
import { BaseModule } from 'src/app/components/base/base.module';
import { MatLegacyTableModule as MatTableModule } from '@angular/material/legacy-table';
import { FlypanelTaskRequalNeededModule } from '../flypanel-task-requal-needed/flypanel-task-requal-needed.module';



@NgModule({
  declarations: [
    FlypanelWithoutPositionComponent
  ],
  imports: [
    CommonModule,
    BaseModule,
    MatTableModule,
    FlypanelTaskRequalNeededModule,
  ],
  exports : [
    FlypanelWithoutPositionComponent
  ]
})
export class FlypanelWithoutPositionModule { }

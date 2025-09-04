import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FlypanelTaskRequalNeededComponent } from './flypanel-task-requal-needed.component';
import { BaseModule } from 'src/app/components/base/base.module';



@NgModule({
  declarations: [
    FlypanelTaskRequalNeededComponent
  ],
  imports: [
    CommonModule,
    BaseModule,
  ],
  exports: [
    FlypanelTaskRequalNeededComponent,
  ]
})
export class FlypanelTaskRequalNeededModule { }

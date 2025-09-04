import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FlyPanelLinkedTasksComponent } from './fly-panel-linked-tasks.component';
import { BaseModule } from 'src/app/components/base/base.module';



@NgModule({
  declarations: [
    FlyPanelLinkedTasksComponent
  ],
  imports: [
    CommonModule,
    BaseModule,
  ],
  exports : [
    FlyPanelLinkedTasksComponent
  ]
})
export class FlyPanelLinkedTasksModule { }

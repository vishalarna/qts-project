import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FlyPanelPositionR6TaskInformationComponent } from './fly-panel-position-r6-task-information.component';
import { BaseModule } from 'src/app/components/base/base.module';
import { FormsModule } from '@angular/forms';



@NgModule({
  declarations: [
    FlyPanelPositionR6TaskInformationComponent
  ],
  imports: [
    CommonModule,
    BaseModule,
    FormsModule
  ],
  exports: [
    FlyPanelPositionR6TaskInformationComponent
  ],
})
export class FlyPanelPositionR6TaskInformationModule { }

import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FlyPanelLinkedTrainingGroupsComponent } from './fly-panel-linked-training-groups.component';
import { BaseModule } from 'src/app/components/base/base.module';



@NgModule({
  declarations: [FlyPanelLinkedTrainingGroupsComponent],
  imports: [
    CommonModule,
    BaseModule
  ],
  exports:[FlyPanelLinkedTrainingGroupsComponent]
})
export class FlyPanelLinkedTrainingGroupsModule { }

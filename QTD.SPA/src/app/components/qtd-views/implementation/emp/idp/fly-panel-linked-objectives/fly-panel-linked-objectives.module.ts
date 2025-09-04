import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FlyPanelLinkedObjectivesComponent } from './fly-panel-linked-objectives.component';
import { FormsModule } from '@angular/forms';
import { BaseModule } from 'src/app/components/base/base.module';



@NgModule({
  declarations: [
    FlyPanelLinkedObjectivesComponent
  ],
  imports: [
    CommonModule,
    BaseModule,
    FormsModule
  ],
  exports:[FlyPanelLinkedObjectivesComponent]
})
export class FlyPanelLinkedObjectivesModule { }

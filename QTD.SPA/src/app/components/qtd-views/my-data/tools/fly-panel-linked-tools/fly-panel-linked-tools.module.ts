import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FlyPanelLinkedToolsComponent } from './fly-panel-linked-tools.component';
import { BaseModule } from 'src/app/components/base/base.module';



@NgModule({
  declarations: [
    FlyPanelLinkedToolsComponent
  ],
  imports: [
    CommonModule,
    BaseModule
  ],
  exports: [FlyPanelLinkedToolsComponent]
})
export class FlyPanelLinkedToolsModule { }

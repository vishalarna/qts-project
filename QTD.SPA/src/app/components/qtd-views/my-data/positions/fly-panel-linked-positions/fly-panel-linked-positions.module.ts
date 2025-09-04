import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FlyPanelLinkedPositionsComponent } from './fly-panel-linked-positions.component';
import { BaseModule } from 'src/app/components/base/base.module';



@NgModule({
  declarations: [FlyPanelLinkedPositionsComponent],
  imports: [
    CommonModule,
    BaseModule
  ],
  exports: [FlyPanelLinkedPositionsComponent]
})
export class FlyPanelLinkedPositionsModule { }

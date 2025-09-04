import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FlyPanelLinkedRRComponent } from './fly-panel-linked-rr.component';
import { BaseModule } from 'src/app/components/base/base.module';

@NgModule({
  declarations: [FlyPanelLinkedRRComponent],
  imports: [CommonModule, BaseModule],
  exports: [FlyPanelLinkedRRComponent],
})
export class FlyPanelLinkedRRModule {}

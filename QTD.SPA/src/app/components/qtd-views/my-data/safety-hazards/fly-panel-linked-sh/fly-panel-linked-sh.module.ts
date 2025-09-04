import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FlyPanelLinkedShComponent } from './fly-panel-linked-sh.component';
import { BaseModule } from 'src/app/components/base/base.module';

@NgModule({
  declarations: [FlyPanelLinkedShComponent],
  imports: [CommonModule, BaseModule],
  exports: [FlyPanelLinkedShComponent],
})
export class FlyPanelLinkedShModule {}

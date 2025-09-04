import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FlyPanelOJTComponent } from './fly-panel-ojt.component';
import { BaseModule } from 'src/app/components/base/base.module';

@NgModule({
  declarations: [FlyPanelOJTComponent],
  imports: [BaseModule, CommonModule],
  exports: [FlyPanelOJTComponent],
})
export class FlyPanelOJTModule {}

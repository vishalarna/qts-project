import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FlyPanelLinkedProceduresComponent } from './fly-panel-linked-procedures.component';
import { BaseModule } from 'src/app/components/base/base.module';
import { MatTreeModule } from '@angular/material/tree';

@NgModule({
  declarations: [FlyPanelLinkedProceduresComponent],
  imports: [CommonModule, BaseModule,MatTreeModule],
  exports: [FlyPanelLinkedProceduresComponent],
})
export class FlyPanelLinkedProceduresModule {}

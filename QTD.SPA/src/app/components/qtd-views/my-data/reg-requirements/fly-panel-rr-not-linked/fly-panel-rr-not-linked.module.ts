import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FlyPanelRRNotLinkedComponent } from './fly-panel-rr-not-linked.component';
import { FormsModule } from '@angular/forms';
import { MatTreeModule } from '@angular/material/tree';
import { BaseModule } from 'src/app/components/base/base.module';
import { MatLegacyMenuModule as MatMenuModule } from '@angular/material/legacy-menu';

@NgModule({
  declarations: [FlyPanelRRNotLinkedComponent],
  imports: [
    CommonModule,
    BaseModule,
    FormsModule,
    MatTreeModule,
    MatMenuModule,
  ],
  exports: [FlyPanelRRNotLinkedComponent],
})
export class FlyPanelRRNotLinkedModule {}

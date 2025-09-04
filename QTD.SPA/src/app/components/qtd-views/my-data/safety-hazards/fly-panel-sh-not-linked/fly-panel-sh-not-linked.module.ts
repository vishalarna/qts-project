import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FlyPanelShNotLinkedComponent } from './fly-panel-sh-not-linked.component';
import { FormsModule } from '@angular/forms';
import { MatLegacyMenuModule as MatMenuModule } from '@angular/material/legacy-menu';
import { MatTreeModule } from '@angular/material/tree';
import { BaseModule } from 'src/app/components/base/base.module';

@NgModule({
  declarations: [FlyPanelShNotLinkedComponent],
  imports: [
    CommonModule,
    BaseModule,
    FormsModule,
    MatTreeModule,
    MatMenuModule,
  ],
  exports: [FlyPanelShNotLinkedComponent],
})
export class FlyPanelShNotLinkedModule {}

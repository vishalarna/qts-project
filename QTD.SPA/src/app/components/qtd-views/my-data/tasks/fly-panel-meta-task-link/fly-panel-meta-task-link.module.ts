import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FlyPanelMetaTaskLinkComponent } from './fly-panel-meta-task-link.component';
import { FormsModule } from '@angular/forms';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { MatLegacyMenuModule as MatMenuModule } from '@angular/material/legacy-menu';
import { MatTreeModule } from '@angular/material/tree';
import { BaseModule } from 'src/app/components/base/base.module';

@NgModule({
  declarations: [FlyPanelMetaTaskLinkComponent],
  imports: [
    CommonModule,
    BaseModule,
    MatMenuModule,
    FormsModule,
    MatCheckboxModule,
    MatTreeModule,
  ],
  exports: [FlyPanelMetaTaskLinkComponent],
})
export class FlyPanelMetaTaskLinkModule {}

import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FlyPanelTaskRrLinkComponent } from './fly-panel-task-rr-link.component';
import { FlyPanelAddRrModule } from '../../reg-requirements/fly-panel-add-rr/fly-panel-add-rr.module';
import { FormsModule } from '@angular/forms';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { MatLegacyMenuModule as MatMenuModule } from '@angular/material/legacy-menu';
import { MatTreeModule } from '@angular/material/tree';
import { BaseModule } from 'src/app/components/base/base.module';

@NgModule({
  declarations: [FlyPanelTaskRrLinkComponent],
  imports: [
    CommonModule,
    BaseModule,
    MatMenuModule,
    FormsModule,
    MatCheckboxModule,
    MatTreeModule,
    FlyPanelAddRrModule,
  ],
  exports: [FlyPanelTaskRrLinkComponent],
})
export class FlyPanelTaskRrLinkModule {}

import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FlyPanelTaskPosLinkComponent } from './fly-panel-task-pos-link.component';
import { FormsModule } from '@angular/forms';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { MatLegacyMenuModule as MatMenuModule } from '@angular/material/legacy-menu';
import { MatTreeModule } from '@angular/material/tree';
import { BaseModule } from 'src/app/components/base/base.module';
import { FlyPanelAddPositionModule } from '../../positions/fly-panel-add-position/fly-panel-add-position.module';

@NgModule({
  declarations: [FlyPanelTaskPosLinkComponent],
  imports: [
    CommonModule,
    BaseModule,
    MatCheckboxModule,
    MatMenuModule,
    FormsModule,
    FlyPanelAddPositionModule
  ],
  exports: [FlyPanelTaskPosLinkComponent],
})
export class FlyPanelTaskPosLinkModule {}

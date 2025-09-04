import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ToolTaskComponent } from './tool-task.component';
import { BaseModule } from 'src/app/components/base/base.module';
import { MatLegacyTableModule as MatTableModule } from '@angular/material/legacy-table';
import { MatLegacyTooltipModule as MatTooltipModule } from '@angular/material/legacy-tooltip';
import { MatLegacyPaginatorModule as MatPaginatorModule } from '@angular/material/legacy-paginator';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { FlyPanelToolTaskLinkModule } from '../../fly-panel-tool-task-link/fly-panel-tool-task-link.module';
import { FlyPanelLinkedToolsModule } from '../../fly-panel-linked-tools/fly-panel-linked-tools.module';
import {  MatSortModule } from '@angular/material/sort';



@NgModule({
  declarations: [
    ToolTaskComponent
  ],
  imports: [
    CommonModule,
    BaseModule,
    MatTableModule,
    MatTooltipModule,
    MatPaginatorModule,
    MatCheckboxModule,
    FlyPanelToolTaskLinkModule,
    FlyPanelLinkedToolsModule,
    MatSortModule,
  ],
  exports: [ToolTaskComponent],
})
export class ToolTaskModule { }

import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ShTaskComponent } from './sh-task.component';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { MatLegacyPaginatorModule as MatPaginatorModule } from '@angular/material/legacy-paginator';
import { MatSortModule } from '@angular/material/sort';
import { MatLegacyTableModule as MatTableModule } from '@angular/material/legacy-table';
import { MatLegacyTooltipModule as MatTooltipModule } from '@angular/material/legacy-tooltip';
import { BaseModule } from 'src/app/components/base/base.module';
import { FlyPanelLinkedShModule } from '../../fly-panel-linked-sh/fly-panel-linked-sh.module';
import { FlyPanelShTasksLinkModule } from '../../fly-panel-sh-tasks-link/fly-panel-sh-tasks-link.module';

@NgModule({
  declarations: [ShTaskComponent],
  imports: [
    CommonModule,
    BaseModule,
    MatTableModule,
    MatSortModule,
    MatPaginatorModule,
    MatCheckboxModule,
    MatTooltipModule,
    FlyPanelShTasksLinkModule,
    FlyPanelLinkedShModule,
  ],
  exports: [ShTaskComponent],
})
export class ShTaskModule {}

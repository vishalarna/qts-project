import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ToolShComponent } from './tool-sh.component';
import { BaseModule } from 'src/app/components/base/base.module';
import { MatLegacyTableModule as MatTableModule } from '@angular/material/legacy-table';
import { MatLegacyTooltipModule as MatTooltipModule } from '@angular/material/legacy-tooltip';
import { MatLegacyPaginatorModule as MatPaginatorModule } from '@angular/material/legacy-paginator';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { FlyPanelToolShLinkModule } from '../../fly-panel-tool-sh-link/fly-panel-tool-sh-link.module';
import { FlyPanelLinkedToolsModule } from '../../fly-panel-linked-tools/fly-panel-linked-tools.module';
import { MatSortModule } from '@angular/material/sort';



@NgModule({
  declarations: [
    ToolShComponent
  ],
  imports: [
    CommonModule,
    BaseModule,
    MatTableModule,
    MatTooltipModule,
    MatPaginatorModule,
    MatCheckboxModule,
    FlyPanelToolShLinkModule,
    FlyPanelLinkedToolsModule,
    MatSortModule
  ],
  exports:[ToolShComponent],
})
export class ToolShModule { }

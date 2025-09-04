import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PositionSkasComponent } from './position-skas.component';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { MatLegacyFormFieldModule as MatFormFieldModule } from '@angular/material/legacy-form-field';
import { MatLegacySelectModule as MatSelectModule } from '@angular/material/legacy-select';
import { MatLegacyTableModule as MatTableModule } from '@angular/material/legacy-table';
import { BaseModule } from 'src/app/components/base/base.module';
import { FlyPanelAddSkaModule } from 'src/app/components/qtd-views/design-and-development/skills-assessment/fly-panel-add-ska/fly-panel-add-ska.module';
import { FlyPanelLinkPositionTaskModule } from '../../fly-panel-link-position-task/fly-panel-link-position-task.module';
import { FlyPanelLinkPositionSkasModule } from '../../fly-panel-link-position-skas/fly-panel-link-position-skas.module';
import { MatLegacyPaginatorModule as MatPaginatorModule } from '@angular/material/legacy-paginator';
import { FlyPanelLinkedPositionsModule } from '../../fly-panel-linked-positions/fly-panel-linked-positions.module';
import { MatSortModule } from '@angular/material/sort';


@NgModule({
  declarations: [
    PositionSkasComponent
  ],
  imports: [
    CommonModule,
    BaseModule,
    MatCheckboxModule,
    MatTableModule,
    MatFormFieldModule,
    MatSelectModule,
    MatPaginatorModule,
    FlyPanelLinkPositionSkasModule,
    FlyPanelAddSkaModule,
    FlyPanelLinkedPositionsModule,
    MatSortModule
  ],
  exports:[PositionSkasComponent]

})
export class PositionSkasModule { }

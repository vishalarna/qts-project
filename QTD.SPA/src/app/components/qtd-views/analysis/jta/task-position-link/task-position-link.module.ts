import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TaskPositionLinkComponent } from './task-position-link.component';
import { HttpClientModule } from '@angular/common/http';
import { Routes, RouterModule } from '@angular/router';
import { NgbDropdownModule } from '@ng-bootstrap/ng-bootstrap';
import { LocalizeModule } from 'src/app/_Shared/modules/localize.module';

import { DataTablesModule } from 'angular-datatables';
import { LayoutModule } from '../../../layout/layout.module';
import { FlyPanelPositionsModule } from '../../../implementation/positions/fly-panel-positions/fly-panel-positions.module';
import { BaseModule } from 'src/app/components/base/base.module';
const routes: Routes = [
  {
    path: '',
    component: TaskPositionLinkComponent,
  },
];

@NgModule({
  declarations: [TaskPositionLinkComponent],
  imports: [
    BaseModule,
    CommonModule,
    HttpClientModule,
    LocalizeModule,
    RouterModule.forChild(routes),
    NgbDropdownModule,
    DataTablesModule,
    LayoutModule,
    FlyPanelPositionsModule
  ],
})
export class TaskPositionLinkModule {}

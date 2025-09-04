import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TaskOJTLinkComponent } from './task-ojt-link.component';
import { HttpClientModule } from '@angular/common/http';
import { Routes, RouterModule } from '@angular/router';
import { NgbDropdownModule } from '@ng-bootstrap/ng-bootstrap';
import { DataTablesModule } from 'angular-datatables';
import { LocalizeModule } from 'src/app/_Shared/modules/localize.module';

import { LayoutModule } from '../../../layout/layout.module';
import { FlyPanelOJTModule } from '../../ojt/fly-panel-ojt/fly-panel-ojt.module';
import { BaseModule } from 'src/app/components/base/base.module';

const routes: Routes = [
  {
    path: '',
    component: TaskOJTLinkComponent,
  },
];

@NgModule({
  declarations: [TaskOJTLinkComponent],
  imports: [
    BaseModule,
    CommonModule,
    HttpClientModule,
    LocalizeModule,
    RouterModule.forChild(routes),
    NgbDropdownModule,
    DataTablesModule,
    LayoutModule,
    FlyPanelOJTModule,
  ],
})
export class TaskOJTLinkModule {}

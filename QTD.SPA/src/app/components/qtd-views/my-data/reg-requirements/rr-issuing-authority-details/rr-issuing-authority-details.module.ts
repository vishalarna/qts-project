import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RRIssuingAuthorityDetailsComponent } from './rr-issuing-authority-details.component';

import { MatLegacyMenuModule as MatMenuModule } from '@angular/material/legacy-menu';
import { RouterModule, Routes } from '@angular/router';
import { BaseModule } from 'src/app/components/base/base.module';
import { LayoutModule } from '../../../layout/layout.module';
import { FlyPanelAddRrIssuingAuthorityModule } from '../fly-panel-add-rr-issuing-authority/fly-panel-add-rr-issuing-authority.module';

const routes: Routes = [
  {
    path: ':id',
    component: RRIssuingAuthorityDetailsComponent,
  },
];

@NgModule({
  declarations: [RRIssuingAuthorityDetailsComponent],
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    MatMenuModule,
    LayoutModule,
    BaseModule,
    FlyPanelAddRrIssuingAuthorityModule,
  ],
})
export class RRIssuingAuthorityDetailsModule {}

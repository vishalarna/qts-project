import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { IssuingAuthorityDetailsComponent } from './issuing-authority-details.component';
import { RouterModule, Routes } from '@angular/router';
import { MatLegacyMenuModule as MatMenuModule } from '@angular/material/legacy-menu';
import { LayoutModule } from '../../../layout/layout.module';
import { BaseModule } from 'src/app/components/base/base.module';
import { FlyPanelIssuingAuthorityModule } from '../fly-panel-add-issuing-authority/fly-panel-add-issuing-authority.module';

const routes: Routes = [
  {
    path: ':id',
    component: IssuingAuthorityDetailsComponent,
  },
];

@NgModule({
  declarations: [IssuingAuthorityDetailsComponent],
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    MatMenuModule,
    LayoutModule,
    BaseModule,
    FlyPanelIssuingAuthorityModule,
  ],

})
export class IssuingAuthorityDetailsModule {}

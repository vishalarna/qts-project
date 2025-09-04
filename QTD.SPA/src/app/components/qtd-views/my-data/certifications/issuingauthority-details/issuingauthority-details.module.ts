import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { BaseModule } from 'src/app/components/base/base.module';
import { MatLegacyMenuModule as MatMenuModule } from '@angular/material/legacy-menu';
import { MatLegacyTabsModule as MatTabsModule } from '@angular/material/legacy-tabs';
import { FlyPanelAddIssuingAuthorityModule } from '../fly-panel-add-issuingauthority/fly-panel-add-issuingauthority.module';
import { FlyPanelAddCertificationModule } from '../fly-panel-add-certification/fly-panel-add-certification.module';
import { IssuingauthorityDetailsComponent } from './issuingauthority-details.component';



const routes: Routes = [
  {
    path: ':id',
    component: IssuingauthorityDetailsComponent,
  }
 ]


@NgModule({
  declarations: [
    IssuingauthorityDetailsComponent
  ],
  imports: [
    CommonModule,
    BaseModule,
    RouterModule.forChild(routes),
    MatMenuModule,
    MatTabsModule,
    FlyPanelAddCertificationModule,
    FlyPanelAddIssuingAuthorityModule,
  ]
})
export class IssuingauthorityDetailsModule { }

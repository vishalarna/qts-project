import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CertificationDetailsComponent } from './certification-details.component';
import { RouterModule, Routes } from '@angular/router';
import { BaseModule } from 'src/app/components/base/base.module';
import { MatLegacyMenuModule as MatMenuModule } from '@angular/material/legacy-menu';
import { MatLegacyTabsModule as MatTabsModule } from '@angular/material/legacy-tabs';


import { FlyPanelAddCertificationModule } from '../fly-panel-add-certification/fly-panel-add-certification.module';
import { FlyPanelAddIssuingAuthorityModule } from '../fly-panel-add-issuingauthority/fly-panel-add-issuingauthority.module';


const routes: Routes = [
  {
    path: ':id',
    component: CertificationDetailsComponent,
  }
 ]



@NgModule({
  declarations: [
    CertificationDetailsComponent
  ],
  imports: [
    CommonModule,
    BaseModule,
    RouterModule.forChild(routes),
    MatMenuModule,
    MatTabsModule,
    FlyPanelAddCertificationModule,
    FlyPanelAddIssuingAuthorityModule,
    RouterModule.forChild(routes),
  ]
})
export class CertificationDetailsModule { }

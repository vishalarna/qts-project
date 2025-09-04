import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CertificationOverviewComponent } from './certification-overview.component';
import { RouterModule, Routes } from '@angular/router';
import { MatLegacyTableModule as MatTableModule } from '@angular/material/legacy-table';
import { MatLegacyPaginatorModule as MatPaginatorModule } from '@angular/material/legacy-paginator';
import { MatSortModule } from '@angular/material/sort';
import { BaseModule } from 'src/app/components/base/base.module';
import { FlyPanelViewCertificationHistoryModule } from '../fly-panel-view-certification-history/fly-panel-view-certification-history.module';
import { FlyPanelCertificationsListModule } from '../fly-panel-certifications-list/fly-panel-certifications-list.module';
// import { FlyPanelViewCertificationHistoryModule } from '../fly-panel-view-location-history/fly-panel-view-location-history.module';;
const routes: Routes = [
  {
    path: '',
    component: CertificationOverviewComponent,
  }
 ]


 @NgModule({
  declarations: [
    CertificationOverviewComponent
  ],
  imports: [
    CommonModule,
    MatTableModule,
    MatPaginatorModule,
    MatSortModule,
    BaseModule,
    RouterModule.forChild(routes),
    FlyPanelViewCertificationHistoryModule,
    FlyPanelCertificationsListModule

  ],
  exports: [CertificationOverviewComponent],
})
export class CertificationOverviewModule { }

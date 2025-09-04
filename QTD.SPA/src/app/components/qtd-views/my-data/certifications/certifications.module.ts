import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CertificationsComponent } from './certifications.component';
import { CertificationNavbarComponent } from './certification-navbar/certification-navbar.component';
import { MatLegacyMenuModule as MatMenuModule } from '@angular/material/legacy-menu';
import { MatLegacyTabsModule as MatTabsModule } from '@angular/material/legacy-tabs';
import { Routes, RouterModule } from '@angular/router';
import { BaseModule } from 'src/app/components/base/base.module';
import { AuthGuard } from 'src/app/_Guards/auth.guard';
import { RouteGuard } from 'src/app/_Guards/route.guard';
import { FlyPanelAddCertificationModule } from './fly-panel-add-certification/fly-panel-add-certification.module';
import { FormsModule } from '@angular/forms';
import { FlyPanelAddCertificationComponent } from './fly-panel-add-certification/fly-panel-add-certification.component';
import { FlyPanelAddIssuingAuthorityModule } from './fly-panel-add-issuingauthority/fly-panel-add-issuingauthority.module';
import { LayoutModule } from '../../layout/layout.module';
import { MatSidenavModule } from '@angular/material/sidenav';
import { FlyPanelViewCertificationHistoryModule } from './fly-panel-view-certification-history/fly-panel-view-certification-history.module';
import { MatTreeModule } from '@angular/material/tree';

const routes: Routes = [
  {
    path: '',
    component: CertificationsComponent,
    children: 
    [
      {
        path: '',
        redirectTo: 'overview',
        pathMatch: 'full',
      },
      {
        path: 'overview',
        canActivate: [AuthGuard, RouteGuard],
        loadChildren: () =>
          import('./certification-overview/certification-overview.module').then(
            (m) => m.CertificationOverviewModule
          ),
      },
      {
        path: 'details',
        canActivate: [AuthGuard, RouteGuard],
        loadChildren: () =>
          import('./certification-details/certification-details.module').then(
            (m) => m.CertificationDetailsModule
          ),
      },
      {
        path: 'issuingauthority',
        canActivate: [AuthGuard, RouteGuard],
        loadChildren: () =>
          import(
            './issuingauthority-details/issuingauthority-details.module'
          ).then((m) => m.IssuingauthorityDetailsModule),
      },
    ]
  }
]

@NgModule({
  declarations: [CertificationsComponent, CertificationNavbarComponent ],
  imports: [
    CommonModule,
    BaseModule,
    MatMenuModule,
    MatTabsModule,
     MatSidenavModule,
    FormsModule,
    LayoutModule,
    RouterModule.forChild(routes),
    FlyPanelAddCertificationModule,
    FormsModule,
    FlyPanelAddIssuingAuthorityModule,
    FlyPanelViewCertificationHistoryModule,
    MatTreeModule
  ],
})
export class CertificationsModule { } 


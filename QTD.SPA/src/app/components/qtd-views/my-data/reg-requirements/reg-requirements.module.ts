import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RegRequirementsComponent } from './reg-requirements.component';
import { RRNavBarComponent } from './rr-nav-bar/rr-nav-bar.component';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { MatLegacyFormFieldModule as MatFormFieldModule } from '@angular/material/legacy-form-field';
import { MatLegacyMenuModule as MatMenuModule } from '@angular/material/legacy-menu';
import { MatLegacySelectModule as MatSelectModule } from '@angular/material/legacy-select';
import { MatSidenavModule } from '@angular/material/sidenav';
import { Routes, RouterModule } from '@angular/router';
import { BaseModule } from 'src/app/components/base/base.module';
import { AuthGuard } from 'src/app/_Guards/auth.guard';
import { RouteGuard } from 'src/app/_Guards/route.guard';
import { LocalizeModule } from 'src/app/_Shared/modules/localize.module';
import { ProcedureOverviewModule } from '../procedures/procedure-overview/procedure-overview.module';
import { FlyPanelAddRrIssuingAuthorityModule } from './fly-panel-add-rr-issuing-authority/fly-panel-add-rr-issuing-authority.module';
import { FlyPanelAddRrModule } from './fly-panel-add-rr/fly-panel-add-rr.module';
import { LayoutModule } from '../../layout/layout.module';
import { MatTreeModule } from '@angular/material/tree';

const routes: Routes = [
  {
    path: '',
    component: RegRequirementsComponent,
    children: [
      {
        path: '',
        redirectTo: 'overview',
        pathMatch: 'full',
      },
      {
        path: 'overview',
        canActivate: [AuthGuard, RouteGuard],
        loadChildren: () =>
          import('./rr-overview/rr-overview.module').then(
            (m) => m.RROverviewModule
          ),
      },
      {
        path: 'rr',
        canActivate: [AuthGuard, RouteGuard],
        loadChildren: () =>
          import('./rr-details/rr-details.module').then(
            (m) => m.RRDetailsModule
          ),
      },
      {
        path: 'ia',
        canActivate: [AuthGuard, RouteGuard],
        loadChildren: () =>
          import(
            './rr-issuing-authority-details/rr-issuing-authority-details.module'
          ).then((m) => m.RRIssuingAuthorityDetailsModule),
      },
    ],
  },
];

@NgModule({
  declarations: [RegRequirementsComponent, RRNavBarComponent],
  imports: [
    CommonModule,
    HttpClientModule,
    RouterModule.forChild(routes),
    LocalizeModule,
    MatSidenavModule,
    LayoutModule,
    BaseModule,
    LayoutModule,
    MatMenuModule,
    ProcedureOverviewModule,
    BaseModule,
    FormsModule,
    ReactiveFormsModule,
    MatFormFieldModule,
    MatSelectModule,
    MatCheckboxModule,
    FlyPanelAddRrIssuingAuthorityModule,
    FlyPanelAddRrModule,
    MatTreeModule
  ],
})
export class RegRequirementsModule {}

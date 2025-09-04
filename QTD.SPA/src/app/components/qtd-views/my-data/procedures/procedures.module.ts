import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ProceduresComponent } from './procedures.component';
import { RouterModule, Routes } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';
import { LocalizeModule } from 'src/app/_Shared/modules/localize.module';
import { MatSidenavModule } from '@angular/material/sidenav';
import { LayoutModule } from '../../layout/layout.module';
import { BaseModule } from 'src/app/components/base/base.module';
import { ProcedureNavBarComponent } from './procedure-nav-bar/procedure-nav-bar.component';
import { MatLegacyMenuModule as MatMenuModule } from '@angular/material/legacy-menu';
import { ProcedureOverviewModule } from './procedure-overview/procedure-overview.module';
import { AuthGuard } from 'src/app/_Guards/auth.guard';
import { RouteGuard } from 'src/app/_Guards/route.guard';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { MatLegacyFormFieldModule as MatFormFieldModule } from '@angular/material/legacy-form-field';
import { MatLegacySelectModule as MatSelectModule } from '@angular/material/legacy-select';
import { BulkEditComponent } from '../bulk-edit/bulk-edit.component';
import { FlyPanelIssuingAuthorityModule } from './fly-panel-add-issuing-authority/fly-panel-add-issuing-authority.module';
import { FlyPanelAddProcedureModule } from './fly-panel-add-procedure/fly-panel-add-procedure.module';
import { FlyPanelProcedureSafetyHazardsLinkComponent } from './fly-panel-procedure-safety-hazards-link/fly-panel-procedure-safety-hazards-link.component';
import { MatTreeModule } from '@angular/material/tree';

const routes: Routes = [
  {
    path: '',
    component: ProceduresComponent,
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
          import('./procedure-overview/procedure-overview.module').then(
            (m) => m.ProcedureOverviewModule
          ),
      },
      {
        path: 'proc',
        canActivate: [AuthGuard, RouteGuard],
        loadChildren: () =>
          import('./procedure-details/procedure-details.module').then(
            (m) => m.ProcedureDetailsModule
          ),
      },
      {
        path: 'ia',
        canActivate: [AuthGuard, RouteGuard],
        loadChildren: () =>
          import(
            './issuing-authority-details/issuing-authority-details.module'
          ).then((m) => m.IssuingAuthorityDetailsModule),
      },
    ],
  },
];

@NgModule({
  declarations: [
    ProceduresComponent,
    ProcedureNavBarComponent,
  ],
  imports: [
    CommonModule,
    HttpClientModule,
    RouterModule.forChild(routes),
    LocalizeModule,
    MatSidenavModule,
    LayoutModule,
    BaseModule,
    MatMenuModule,
    ProcedureOverviewModule,
    BaseModule,
    FormsModule,
    ReactiveFormsModule,
    MatFormFieldModule,
    MatSelectModule,
    MatCheckboxModule,
    FlyPanelIssuingAuthorityModule,
    FlyPanelAddProcedureModule,
    MatTreeModule
    // FlyPanelProcedureNotLinkedModule
  ],
})
export class ProceduresModule {}

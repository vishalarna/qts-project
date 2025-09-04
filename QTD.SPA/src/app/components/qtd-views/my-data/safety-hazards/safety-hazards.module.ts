import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SafetyHazardComponent } from '../../analysis/safety-hazard/safety-hazard.component';
import { ShNavBarComponent } from './sh-nav-bar/sh-nav-bar.component';
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
import { SafetyHazardsComponent } from './safety-hazards.component';
import { LayoutModule } from '../../layout/layout.module';
import { FlyPanelShCategoryModule } from './fly-panel-sh-category/fly-panel-sh-category.module';
import { FlypanelAddSafetyHazardsModule } from './flypanel-add-safety-hazards/flypanel-add-safety-hazards.module';
import { MatTreeModule } from '@angular/material/tree';

const routes: Routes = [
  {
    path: '',
    component: SafetyHazardsComponent,
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
          import('./sh-overview/sh-overview.module').then(
            (m) => m.ShOverviewModule
          ),
      },
      {
        path: 'sh',
        canActivate: [AuthGuard, RouteGuard],
        loadChildren: () =>
          import('./sh-details/sh-details.module').then(
            (m) => m.ShDetailsModule
          ),
      },
      {
        path: 'cat',
        canActivate: [AuthGuard, RouteGuard],
        loadChildren: () =>
          import('./sh-category-details/sh-category-details.module').then(
            (m) => m.ShCategoryDetailsModule
          ),
      },
    ],
  },
];

@NgModule({
  declarations: [SafetyHazardsComponent, ShNavBarComponent],
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
    FlypanelAddSafetyHazardsModule,
    FlyPanelShCategoryModule,
    MatTreeModule
  ],
})
export class SafetyHazardsModule {}

import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ToolsComponent } from './tools.component';
import { ToolNavbarComponent } from './tool-navbar/tool-navbar.component';
import { AuthGuard } from 'src/app/_Guards/auth.guard';
import { RouteGuard } from 'src/app/_Guards/route.guard';
import { RouterModule, Routes } from '@angular/router';
import { ToolOverviewModule } from './tool-overview/tool-overview.module';
import { FlyPanelAddToolModule } from './fly-panel-add-tool/fly-panel-add-tool.module';
import { FlyPanelAddToolCategoryModule } from './fly-panel-add-tool-category/fly-panel-add-tool-category.module';

import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { MatLegacyFormFieldModule as MatFormFieldModule } from '@angular/material/legacy-form-field';
import { MatLegacyMenuModule as MatMenuModule } from '@angular/material/legacy-menu';
import { MatLegacySelectModule as MatSelectModule } from '@angular/material/legacy-select';
import { MatSidenavModule } from '@angular/material/sidenav';
import { BaseModule } from 'src/app/components/base/base.module';
import { LocalizeModule } from 'src/app/_Shared/modules/localize.module';
import { LayoutModule } from '../../layout/layout.module';
import { MatTreeModule } from '@angular/material/tree';

const routes: Routes = [
  {
    path: '',
    component: ToolsComponent,
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
          import('./tool-overview/tool-overview.module').then(
            (m) => m.ToolOverviewModule
          ),
      },
      {
        path: 'detail',
        canActivate: [AuthGuard, RouteGuard],
        loadChildren: () =>
          import('./tool-details/tool-details.module').then(
            (m) => m.ToolDetailsModule
          ),
      },
      {
        path: 'cat-detail',
        canActivate: [AuthGuard, RouteGuard],
        loadChildren: () =>
          import('./tool-category-details/tool-category-details.module').then(
            (m) => m.ToolCategoryDetailsModule
          ),
      },
    ],
  },
];

@NgModule({
  declarations: [ToolsComponent, ToolNavbarComponent],
  imports: [
    CommonModule,
    HttpClientModule,
    RouterModule.forChild(routes),
    LocalizeModule,
    MatSidenavModule,
    LayoutModule,
    BaseModule,
    MatMenuModule,
    ToolOverviewModule,
    FormsModule,
    ReactiveFormsModule,
    MatFormFieldModule,
    MatSelectModule,
    MatCheckboxModule,
    FlyPanelAddToolModule,
    FlyPanelAddToolCategoryModule,
    MatTreeModule
  ],
})
export class ToolsModule {}

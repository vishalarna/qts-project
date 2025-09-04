import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DefinitionsComponent } from './definitions.component';
import { DefinitionsNavbarComponent } from './definitions-navbar/definitions-navbar.component';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from 'src/app/_Guards/auth.guard';
import { RouteGuard } from 'src/app/_Guards/route.guard';
import { MatSidenavModule } from '@angular/material/sidenav';
import { BaseModule } from 'src/app/components/base/base.module';
import { HttpClientModule } from '@angular/common/http';
import { LayoutModule } from '../../layout/layout.module';
import { FormsModule } from '@angular/forms';
import { MatLegacyMenuModule as MatMenuModule } from '@angular/material/legacy-menu';
import { FlyPanelAddDefinitionCategoryModule } from './fly-panel-add-definition-category/fly-panel-add-definition-category.module';
import { FlyPanelAddDefinitionModule } from './fly-panel-add-definition/fly-panel-add-definition.module';
const routes: Routes = [
  {
    path: '',
    component: DefinitionsComponent,
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
          import('./definition-overview/definition-overview.module').then(
            (m) => m.DefinitionOverviewModule
          ),
      },
      {
        path: 'details',
        canActivate: [AuthGuard, RouteGuard],
        loadChildren: () =>
          import('./definition-details/definition-details.module').then(
            (m) => m.DefinitionDetailsModule
          ),
      },
      {
        path: 'definition-category-details',
        canActivate: [AuthGuard, RouteGuard],
        loadChildren: () =>
          import(
            './definition-category-details/definition-category-details.module'
          ).then((m) => m.DefinitionCategoryDetailsModule),
      },
    ]
  }
]


@NgModule({
  declarations: [
    DefinitionsComponent,
    DefinitionsNavbarComponent
  ],
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    CommonModule,
    MatSidenavModule,
    BaseModule,
    HttpClientModule,
    LayoutModule,
    FormsModule,
    MatMenuModule,
    FlyPanelAddDefinitionCategoryModule,
    FlyPanelAddDefinitionModule
  ]
})
export class DefinitionsModule { }

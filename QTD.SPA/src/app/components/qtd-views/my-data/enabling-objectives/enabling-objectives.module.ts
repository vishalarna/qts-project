import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FlypanelAddEoComponent } from './flypanel-add-eo/flypanel-add-eo.component';
import { FlypanelEOCategoryComponent } from './flypanel-eo-category/flypanel-eo-category.component';
import { FlypanelEOSubCategoryComponent } from './flypanel-eo-sub-category/flypanel-eo-sub-category.component';
import { FlypanelEOTopicComponent } from './flypanel-eo-topic/flypanel-eo-topic.component';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from 'src/app/_Guards/auth.guard';
import { RouteGuard } from 'src/app/_Guards/route.guard';
import { EnablingObjectivesComponent } from './enabling-objectives.component';
import { EnablingObjectivesNavbarComponent } from './enabling-objectives-navbar/enabling-objectives-navbar.component';
import { BaseModule } from 'src/app/components/base/base.module';
import { FormsModule } from '@angular/forms';
import { MatLegacyMenuModule as MatMenuModule } from '@angular/material/legacy-menu';
import { MatSidenavModule } from '@angular/material/sidenav';
import { LocalizeModule } from 'src/app/_Shared/modules/localize.module';
import { LayoutModule } from '../../layout/layout.module';
import { FlypanelAddEoModule } from './flypanel-add-eo/flypanel-add-eo.module';
import { FlypanelEOCategoryModule } from './flypanel-eo-category/flypanel-eo-category.module';
import { FlypanelEOSubCategoryModule } from './flypanel-eo-sub-category/flypanel-eo-sub-category.module';
import { FlypanelEOTopicModule } from './flypanel-eo-topic/flypanel-eo-topic.module';
import { MatTreeModule } from '@angular/material/tree';
import {MatLegacyTooltipModule as MatTooltipModule} from '@angular/material/legacy-tooltip';
const routes : Routes = [
  {
    path:'',
    component : EnablingObjectivesComponent,
    children : [
      {
        path : '',
        redirectTo : 'overview',
        pathMatch : 'full',
      },
      {
        path : 'overview',
        canActivate : [AuthGuard, RouteGuard],
        loadChildren : () =>
          import('./enabling-objectives-overview/enabling-objectives-overview.module').then(
            (m)=> m.EnablingObjectivesOverviewModule
          ),
      },
      {
        path: 'category',
        canActivate:[AuthGuard,RouteGuard],
        loadChildren : () =>
          import('./eo-category-details/eo-category-details.module').then(
            (m)=> m.EoCategoryDetailsModule
          ),
      },
      {
        path: 'sub-category',
        canActivate: [AuthGuard, RouteGuard],
        loadChildren : () =>
          import('./eo-sub-category-details/eo-sub-category-details.module').then(
            (m) => m.EoSubCategoryDetailsModule
          ),
      },
      {
        path : 'topic',
        canActivate: [AuthGuard, RouteGuard],
        loadChildren: () =>
          import('./eo-topic-details/eo-topic-details.module').then(
            (m) => m.EoTopicDetailsModule
          ),
      },
      {
        path : 'eo',
        canActivate : [AuthGuard,RouteGuard],
        loadChildren : () =>
          import('./enabling-objective-details/enabling-objective-details.module').then(
            (m) => m.EnablingObjectiveDetailsModule
          )
      }
    ]
  }
]

@NgModule({
  declarations: [
    EnablingObjectivesComponent,
    EnablingObjectivesNavbarComponent,
  ],
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    BaseModule,
    FormsModule,
    MatMenuModule,
    MatSidenavModule,
    LocalizeModule,
    LayoutModule,
    MatTreeModule,
    FlypanelAddEoModule,
    FlypanelEOCategoryModule,
    FlypanelEOSubCategoryModule,
    FlypanelEOTopicModule,
    MatTooltipModule
  ],
})
export class EnablingObjectivesModule { }

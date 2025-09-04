import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { InstructorsComponent } from './instructors.component';
import { InstructorsNavbarComponent } from './instructors-navbar/instructors-navbar.component';
import { RouterModule, Routes } from '@angular/router';
import { MatSidenavModule } from '@angular/material/sidenav';
import { BaseModule } from 'src/app/components/base/base.module';
import { HttpClientModule } from '@angular/common/http';
import { AuthGuard } from 'src/app/_Guards/auth.guard';
import { RouteGuard } from 'src/app/_Guards/route.guard';
import { LayoutModule } from '../../layout/layout.module';
import { FormsModule } from '@angular/forms';
import { MatLegacyMenuModule as MatMenuModule } from '@angular/material/legacy-menu';
import { FlyPanelAddProcedureModule } from '../procedures/fly-panel-add-procedure/fly-panel-add-procedure.module';
import { FlyPanelIssuingAuthorityModule } from '../procedures/fly-panel-add-issuing-authority/fly-panel-add-issuing-authority.module';
import { FlyPanelAddInstructorCategoryModule } from './fly-panel-add-instructor-category/fly-panel-add-instructor-category.module';
import { FlyPanelAddInstructorModule } from './fly-panel-add-instructor/fly-panel-add-instructor.module';
import { MatTreeModule } from '@angular/material/tree';
const routes: Routes = [
  {
    path: '',
    component: InstructorsComponent,
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
          import('./instructors-overview/instructors-overview.module').then(
            (m) => m.InstructorsOverviewModule
          ),
      },
      {
        path: 'details',
        canActivate: [AuthGuard, RouteGuard],
        loadChildren: () =>
          import('./instructor-details/instructor-details.module').then(
            (m) => m.InstructorDetailsModule
          ),
      },
      {
        path: 'category-details',
        canActivate: [AuthGuard, RouteGuard],
        loadChildren: () =>
          import(
            './instructor-category-details/instructor-category-details.module'
          ).then((m) => m.InstructorCategoryDetailsModule),
      },
    ]
  }
]

@NgModule({
  declarations: [
    InstructorsComponent,
    InstructorsNavbarComponent
  ],
  imports: [
    CommonModule,
      RouterModule.forChild(routes),
      MatSidenavModule,
      BaseModule,
      HttpClientModule,
      LayoutModule,
      FormsModule,
      MatMenuModule,
    FlyPanelAddProcedureModule,
    FlyPanelIssuingAuthorityModule,
    FlyPanelAddInstructorCategoryModule,
    FlyPanelAddInstructorModule,
    MatTreeModule
  ]
})
export class InstructorsModule { }

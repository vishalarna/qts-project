import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from 'src/app/_Guards/auth.guard';
import { RouteGuard } from 'src/app/_Guards/route.guard';
import { LayoutModule } from '@angular/cdk/layout';
import { FormsModule } from '@angular/forms';
import { MatLegacyMenuModule as MatMenuModule } from '@angular/material/legacy-menu';
import { MatSidenavModule } from '@angular/material/sidenav';
import { BaseModule } from 'src/app/components/base/base.module';
import { DataImportComponent } from './data-import.component';
import { DataImportLandingPageComponent } from './data-import-landing-page/data-import-landing-page.component';

 const routes: Routes = [
  {
    path: '',
    component: DataImportComponent,
    children:
      [
        {
          path: '',
          canActivate: [AuthGuard, RouteGuard],
          component: DataImportLandingPageComponent,
          loadChildren: () =>
          import('./data-import-landing-page/data-import-landing-page.module').then(
            (m) => m.DataImportLandingPageModule
          ),
        },
        {
          path: 'class',
          canActivate: [AuthGuard, RouteGuard],
          loadChildren: () =>
            import('./external-class-import/external-class-import.module').then(
              (m) => m.ExternalClassImportModule
            ),
        }
      ]
  }
 ]

@NgModule({
  declarations: [DataImportComponent],
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    MatSidenavModule,
    BaseModule,
    HttpClientModule,
    LayoutModule,
    FormsModule,
    MatMenuModule,
  ],
  exports: [DataImportComponent]
})
export class DataImportModule { }

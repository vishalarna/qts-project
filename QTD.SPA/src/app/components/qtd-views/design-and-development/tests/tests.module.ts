import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TestsComponent } from './tests.component';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from 'src/app/_Guards/auth.guard';
import { RouteGuard } from 'src/app/_Guards/route.guard';
import { LayoutModule } from '@angular/cdk/layout';
import { FormsModule } from '@angular/forms';
import { MatLegacyMenuModule as MatMenuModule } from '@angular/material/legacy-menu';
import { MatSidenavModule } from '@angular/material/sidenav';
import { BaseModule } from 'src/app/components/base/base.module';

const routes: Routes = [
  {
    path: '',
    component: TestsComponent,
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
            import('./tests-overview/tests-overview.module').then(
              (m) => m.TestsOverviewModule
            ),
        },
        {
          path: 'create',
          canActivate: [AuthGuard, RouteGuard],
          loadChildren: () =>
            import('./test-create-wizard/test-create-wizard.module').then(
              (m) => m.TestCreateWizardModule
            ),
        },
        {
          path: 'edit',
          canActivate: [AuthGuard, RouteGuard],
          loadChildren: () =>
            import('./test-create-wizard/test-create-wizard.module').then(
              (m) => m.TestCreateWizardModule
            ),
        },
        {
          path: 'copy',
          canActivate: [AuthGuard, RouteGuard],
          loadChildren: () =>
            import('./test-create-wizard/test-create-wizard.module').then(
              (m) => m.TestCreateWizardModule
            ),
        },
        {
         path: 'selectQuestion',
         canActivate: [AuthGuard, RouteGuard],
         loadChildren: () =>
           import('./test-create-wizard/test-wizard-components/import-test-questions/select-questions/select-questions.module').then(
             (m) => m.SelectQuestionsModule
           ),
        },
        {
          path: 'selectUnlinkedQuestion',
          canActivate: [AuthGuard, RouteGuard],
          loadChildren: () =>
            import('./test-create-wizard/test-wizard-components/import-test-questions/select-unlinked-questions/select-unlinked-questions.module').then(
              (m) => m.SelectUnlinkedQuestionsModule
            ),
         },
        {
          path: 'publish',
          canActivate: [AuthGuard, RouteGuard],
          loadChildren: () =>
            import('./test-create-wizard/test-wizard-components/preview-and-publish/preview-and-publish.module').then(
              (m) => m.PreviewAndPublishModule
            )
        },
        {
          path: 'publish/:id/:publish',
          canActivate: [AuthGuard, RouteGuard],
          loadChildren: () =>
            import('./test-create-wizard/test-wizard-components/preview-and-publish/preview-and-publish.module').then(
              (m) => m.PreviewAndPublishModule
            )
        }
      ]
  }
]

@NgModule({
  declarations: [TestsComponent],
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
  exports: [TestsComponent]
})
export class TestsModule { }

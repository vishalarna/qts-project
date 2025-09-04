import {NgModule} from '@angular/core';
import {RouterModule, Routes} from '@angular/router';
import {AuthGuard} from './_Guards/auth.guard';
import {RouteGuard} from './_Guards/route.guard';
import {CbtManagerComponent} from './components/shared/cbt-manager/cbt-manager.component';
import { PublicRouteGuard } from './_Guards/public-route.guard';
import { PublicRequestGuard } from './_Guards/public-request.guard';
 
const routes: Routes = [
  {
    path: '',
    redirectTo: 'auth',
    pathMatch: 'full',
  },
  {
    path: 'auth',
    canLoad: [RouteGuard],
    loadChildren: () =>
      import('./components/qtd-views/auth/auth.module').then(
        (m) => m.AuthModule
      ),
  },
  {
    path: 'home',
    canLoad: [AuthGuard,RouteGuard],
    loadChildren: () =>
      import('./components/qtd-views/home/home.module').then(
        (m) => m.HomeModule
      ),
  },
  {
    path: 'error',
    loadChildren: () =>
      import('./components/qtd-views/error-pages/error-pages.module').then(
        (m) => m.ErrorPagesModule
      ),
  },
  {
    path: 'clients',
    canLoad: [AuthGuard],
    loadChildren: () =>
      import('./components/qtd-views/clients/clients.module').then(
        (m) => m.ClientsModule
      ),
  },
  {
    path: 'implementation',
    canLoad: [AuthGuard,RouteGuard],
    loadChildren: () =>
      import(
        './components/qtd-views/implementation/implementation.module'
        ).then((m) => m.ImplementationModule),
  },
  {
    path: 'analysis',
    canLoad: [AuthGuard,RouteGuard],
    loadChildren: () =>
      import('./components/qtd-views/analysis/analysis.module').then(
        (m) => m.AnalysisModule
      ),
  },
  // { path: '**', redirectTo: 'error', data: ['404'] },
  {
    path: 'templates',
    canLoad: [AuthGuard,RouteGuard],
    loadChildren: () =>
      import('./components/qtd-views/templates-and-forms/templates-and-forms.module').then(
        (m) => m.TemplatesAndFormsModule
      ),
  },
  {
    path: 'dnd',
    canLoad: [AuthGuard,RouteGuard],
    loadChildren: () =>
      import(
        './components/qtd-views/design-and-development/design-and-development.module'
        ).then((m) => m.DesignAndDevelopmentModule),
  },
  {
    path: 'my-data',
    canLoad: [AuthGuard,RouteGuard],
    loadChildren: () =>
      import('./components/qtd-views/my-data/my-data.module').then(
        (m) => m.MyDataModule
      ),
  },
  {
    path: 'ila',
    loadChildren: () =>
      import('./components/qtd-views/implementation/ila/ila.module').then(
        (m) => m.ILAModule
      ),
  },
  {
    path: 'objective',
    loadChildren: () =>
      import(
        './components/qtd-views/implementation/ila/objectives/objectives.module'
        ).then((m) => m.ObjectivesModule),
  },
  {
    path: 'data-exchange',
    canLoad: [AuthGuard,RouteGuard],
    loadChildren: () =>
      import('./components/qtd-views/data-exchange/data-exchange.module').then(
        (m) => m.DataExchangeModule
      ),
  },
  {
    path: 'evaluation',
    canLoad: [AuthGuard,RouteGuard],
    loadChildren: () =>
      import(
        './components/qtd-views/evaluation/evaluation.module'
        ).then((m) => m.EvaluationModule),
  },
  {
    path: 'emp',
    loadChildren: () =>
      import('./components/qtd-views/employee-portal/employee-portal.module').then(
        (m) => m.EmployeePortalModule
      )
  },
  {
    path: 'reports',
    canLoad: [AuthGuard,RouteGuard],
    loadChildren: () =>
      import('./components/qtd-views/reports/reports.module').then(
        (m) => m.ReportsModule
      ),
  },
  {
    path: 'procedure',
    canLoad: [AuthGuard,RouteGuard],
    loadChildren: () =>
      import('./components/qtd-views/implementation/procedure-review/procedure-review.module').then(
        (m) => m.ProcedureReviewModule
      ),
  },
  {
    path: 'nerc',
    canLoad: [AuthGuard,RouteGuard],
    loadChildren: () =>
      import('./components/qtd-views/nerc/nerc.module').then(
        (m) => m.NercModule
      ),
  },
  {
    path: 'document-storage',
    canLoad: [AuthGuard,RouteGuard],
    loadChildren: () =>
      import('./components/qtd-views/document-storage/document-storage.module').then(
        (m) => m.DocumentStorageModule
      ),
  },
  {
    path: 'disabledFeature',
    canLoad: [AuthGuard],
    loadChildren: () =>
      import('./components/shared/disabled-feature/disabled-feature.module').then(
        (m) => m.DisabledFeatureModule
      ),
  },
  {
    path: 'admin/instance-setup',
    canLoad: [AuthGuard,RouteGuard],
    loadChildren: () =>
      import('./components/qtd-views/admin/instance-setup-wizard/instance-setup-wizard.module').then(
        (m) => m.InstanceSetupWizardModule
      ),
  },
  {
    path: 'admin/admin-messages',
    canLoad: [AuthGuard,RouteGuard],
    loadChildren: () =>
      import('./components/qtd-views/admin/auth-admin-messages/auth-admin-messages.module').then(
        (m) => m.AuthAdminMessagesModule
      ),
  },
  {
    path: 'expired-license',
    canLoad: [AuthGuard],
    loadChildren: () =>
      import('./components/qtd-views/expired-license/expired-license.module').then(
        (m) => m.ExpiredLicenseModule
      ),
  },
  {
    path: ':instanceName/:publicUrl',
    canActivate: [PublicRouteGuard,PublicRequestGuard],
    loadChildren: () =>
      import('./components/qtd-views/public-portal/public-portal.module').then(
        (m) => m.PublicPortalModule
      ),
  }
 
];
 
@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {
}
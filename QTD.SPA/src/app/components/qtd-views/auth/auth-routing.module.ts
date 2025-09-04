import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthComponent } from './auth.component';
import { AuthGuard } from 'src/app/_Guards/auth.guard';

const routes: Routes = [
  {
    path: '',
    component: AuthComponent,

    children: [
      {
        path: '',
        redirectTo: 'login',
        pathMatch: 'full',
      },
      {
        path: 'login-password',
        loadChildren: () =>
          import('./login/login.module').then((m) => m.LoginModule),
      },
      {
        path: 'login',
        loadChildren: () =>
            import('./login-sso/login-sso.module').then((m) => m.LoginSsoModule),
      },
      // {
      //   path: '2FA',
      //   canLoad: [TwoFAGuard],
      //   loadChildren: () =>
      //     import('./twofactorauth/twofactorauth.module').then(
      //       (m) => m.TwofactorauthModule
      //     ),
      // },
      {
        path: '2fa',
        canLoad: [AuthGuard],
        loadChildren: () =>
          import('./twofactorauth/twofactorauth.module').then(
            (m) => m.TwofactorauthModule
          ),
      },
      {
        path: 'help',
        loadChildren: () =>
          import('./auth-help/auth-help.module').then((m) => m.AuthHelpModule),
      },
      {
        path: 'forgot',
        loadChildren: () =>
          import('./forgot-password/forgot-password.module').then(
            (m) => m.ForgotPasswordModule
          ),
      },
      {
        path: 'create',
        loadChildren: () =>
          import('./create-password/create-password.module').then(
            (m) => m.CreatePasswordModule
          ),
      },
    ],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class AuthRoutingModule {}

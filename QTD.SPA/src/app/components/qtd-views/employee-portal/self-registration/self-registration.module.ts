import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SelfRegistrationComponent } from './self-registration.component';
import { Routes } from '@angular/router';
import { AuthGuard } from 'src/app/_Guards/auth.guard';
import { RouteGuard } from 'src/app/_Guards/route.guard';

const routes: Routes = [
  {
    path: '',
    component: SelfRegistrationComponent,
    children: [
      {

        path: 'overview',
        canActivate: [AuthGuard, RouteGuard],
        loadChildren: () =>
          import('./self-registration-overview/self-registration-overview.module').then(
            (m) => m.SelfRegistrationOverviewModule
          ),
      },
      {

        path: '',
        redirectTo:'overview',
        pathMatch:'full'  
      },

    ],
  },
];

@NgModule({
  declarations: [
    SelfRegistrationComponent
  ],
  imports: [
    CommonModule
  ]
})
export class SelfRegistrationModule { }

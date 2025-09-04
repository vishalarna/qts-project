import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from 'src/app/_Guards/auth.guard';
import { RouteGuard } from 'src/app/_Guards/route.guard';
import { EmailNotificationsComponent } from './email-notifications/email-notifications.component';
import { TemplatesAndFormsComponent } from './templates-and-forms.component';

const routes: Routes = [
  {
    path: '',
    component: TemplatesAndFormsComponent,
    children: [
      {
        path: 'notifications/email',
        canActivate: [AuthGuard, RouteGuard],
        component: EmailNotificationsComponent
      },
    ],
  },
];

@NgModule({ imports: [RouterModule.forChild(routes)], exports: [RouterModule] })
export class TemplatesAndFormsRoutingModule {}

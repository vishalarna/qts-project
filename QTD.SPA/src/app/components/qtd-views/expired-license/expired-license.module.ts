import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ExpiredLicenseComponent } from './expired-license.component';
import { BaseModule } from '../../base/base.module';
import { FormsModule } from '@angular/forms';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from 'src/app/_Guards/auth.guard';
import { RouteGuard } from 'src/app/_Guards/route.guard';
import { LayoutModule } from '../layout/layout.module';

const routes: Routes = [
  {
    path: '',
    canActivate: [AuthGuard, RouteGuard],
    component: ExpiredLicenseComponent,
  },
];

@NgModule({
  declarations: [
    ExpiredLicenseComponent
  ],
  imports: [
    CommonModule,
    BaseModule,
    FormsModule,
    RouterModule.forChild(routes),
    LayoutModule
  ],
  exports:[ExpiredLicenseComponent]
})
export class ExpiredLicenseModule { }

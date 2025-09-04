import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router, RouterModule, Routes } from '@angular/router';
import { ErrorPagesComponent } from './error-pages.component';
import { HttpClientModule } from '@angular/common/http';
import { NotFound404Component } from './not-found404/not-found404.component';
import { LocalizeModule } from 'src/app/_Shared/modules/localize.module';

const route: Routes = [
  {
    path: '',
    component: ErrorPagesComponent,
    children: [
      {
        path: '404',
        component: NotFound404Component,
      },
    ],
  },
];

@NgModule({
  declarations: [ErrorPagesComponent, NotFound404Component],
  imports: [
    CommonModule,
    HttpClientModule,
    RouterModule.forChild(route),
    LocalizeModule,
  ],
})
export class ErrorPagesModule {}

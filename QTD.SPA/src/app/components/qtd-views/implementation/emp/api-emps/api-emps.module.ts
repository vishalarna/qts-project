import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ApiEmpsComponent } from './api-emps.component';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule, Routes } from '@angular/router';
import { LocalizeModule } from 'src/app/_Shared/modules/localize.module';

import { LayoutModule } from '../../../layout/layout.module';

const routes: Routes = [
  {
    path: '',
    component: ApiEmpsComponent,
  },
];

@NgModule({
  declarations: [ApiEmpsComponent],
  imports: [
    CommonModule,
    HttpClientModule,
    RouterModule.forChild(routes),
    LocalizeModule,
    LayoutModule,
  ],
})
export class ApiEmpsModule {}

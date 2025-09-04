import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { Routes, RouterModule } from '@angular/router';
import { LocalizeModule } from 'src/app/_Shared/modules/localize.module';

import { UploadEmpsComponent } from './upload-emps.component';
import { NgbDropdownModule, NgbModalModule } from '@ng-bootstrap/ng-bootstrap';
import { LayoutModule } from '../../../layout/layout.module';
import { BaseModule } from 'src/app/components/base/base.module';

const routes: Routes = [
  {
    path: '',
    component: UploadEmpsComponent,
  },
];

@NgModule({
  declarations: [UploadEmpsComponent],
  imports: [
    BaseModule,
    CommonModule,
    HttpClientModule,
    RouterModule.forChild(routes),
    LocalizeModule,
    NgbModalModule,
    NgbDropdownModule,
    LayoutModule,
    BaseModule
  ],
})
export class UploadEmpsModule {}

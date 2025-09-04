import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { IDPComponent } from '../idp/idp.component';
import { Routes, RouterModule } from '@angular/router';
import { LayoutModule } from '../../../layout/layout.module';
import { BaseModule } from 'src/app/components/base/base.module';
import { MatLegacyMenuModule as MatMenuModule } from '@angular/material/legacy-menu';
import { MatLegacyTabsModule as MatTabsModule } from '@angular/material/legacy-tabs';
import { IdpDetailModule } from './idp-detail/idp-detail.module';
import { IdpReviewModule } from './idp-review/idp-review.module';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MetaIdpDetailModule } from './meta-idp-detail/meta-idp-detail.module';

const routes: Routes = [
  {
    path: ':id',
    component: IDPComponent,
  }]

@NgModule({
  declarations: [
    IDPComponent,
    
  ],
  imports: [
    CommonModule,
    LayoutModule,
    BaseModule,
    MatMenuModule,
    MatTabsModule,
    RouterModule.forChild(routes),
    IdpDetailModule,
    IdpReviewModule,
    MatToolbarModule,
    MetaIdpDetailModule
  ],
  exports:[IDPComponent]
})
export class IDPModule { }

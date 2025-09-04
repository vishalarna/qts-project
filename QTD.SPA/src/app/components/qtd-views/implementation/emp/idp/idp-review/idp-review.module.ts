import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { IdpReviewComponent } from './idp-review.component';
import { BaseModule } from 'src/app/components/base/base.module';
import { LayoutModule } from '@angular/cdk/layout';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatExpansionModule } from '@angular/material/expansion';
import { MatLegacyInputModule as MatInputModule } from '@angular/material/legacy-input';
import { MatLegacyMenuModule as MatMenuModule } from '@angular/material/legacy-menu';
import { MatLegacyPaginatorModule as MatPaginatorModule } from '@angular/material/legacy-paginator';
import { MatLegacySelectModule as MatSelectModule } from '@angular/material/legacy-select';
import { MatLegacyTableModule as MatTableModule } from '@angular/material/legacy-table';
import { FlyPanelReleaseIdpModule } from '../fly-panel-release-idp/fly-panel-release-idp.module';



@NgModule({
  declarations: [
    IdpReviewComponent
  ],
  imports: [
    CommonModule,
    BaseModule,
    LayoutModule,
    MatTableModule,
    MatPaginatorModule,
    MatMenuModule,
    FormsModule,
    ReactiveFormsModule,
    MatInputModule,
    MatSelectModule,
    MatExpansionModule,
    FlyPanelReleaseIdpModule,
  ],
  exports:[IdpReviewComponent]
})
export class IdpReviewModule { }

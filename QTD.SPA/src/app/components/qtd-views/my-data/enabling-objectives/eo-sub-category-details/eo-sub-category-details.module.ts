import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { EoSubCategoryDetailsComponent } from './eo-sub-category-details.component';
import { BaseModule } from 'src/app/components/base/base.module';
import { MatLegacyMenuModule as MatMenuModule } from '@angular/material/legacy-menu';
import { FlypanelEOSubCategoryModule } from '../flypanel-eo-sub-category/flypanel-eo-sub-category.module';
import { MatLegacyTooltipModule as MatTooltipModule } from '@angular/material/legacy-tooltip';

const routes : Routes = [
  {
    path: ':id',
    component : EoSubCategoryDetailsComponent,
  }
]

@NgModule({
  declarations: [EoSubCategoryDetailsComponent],
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    BaseModule,
    MatMenuModule,
    FlypanelEOSubCategoryModule,
    MatTooltipModule,
  ]
})
export class EoSubCategoryDetailsModule { }

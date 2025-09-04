import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { EoCategoryDetailsComponent } from './eo-category-details.component';
import { Router, RouterModule, Routes } from '@angular/router';
import { BaseModule } from 'src/app/components/base/base.module';
import { MatLegacyMenuModule as MatMenuModule } from '@angular/material/legacy-menu';
import { FlypanelEOCategoryModule } from '../flypanel-eo-category/flypanel-eo-category.module';
import { MatLegacyTooltipModule as MatTooltipModule } from '@angular/material/legacy-tooltip';

const routes : Routes = [
  {
    path: ':id',
    component: EoCategoryDetailsComponent,
  }
]

@NgModule({
  declarations: [
    EoCategoryDetailsComponent
  ],
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    BaseModule,
    MatMenuModule,
    FlypanelEOCategoryModule,
    MatTooltipModule,
  ]
})
export class EoCategoryDetailsModule { }

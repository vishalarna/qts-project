import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ShCategoryDetailsComponent } from './sh-category-details.component';
import { RouterModule, Routes } from '@angular/router';
import { BaseModule } from 'src/app/components/base/base.module';
import { MatLegacyMenuModule as MatMenuModule } from '@angular/material/legacy-menu';
import { FlyPanelShCategoryModule } from '../fly-panel-sh-category/fly-panel-sh-category.module';

const routes: Routes = [
  {
    path: ':id',
    component: ShCategoryDetailsComponent,
  },
];

@NgModule({
  declarations: [
    ShCategoryDetailsComponent
  ],
  imports: [
    CommonModule,
    BaseModule,
    RouterModule.forChild(routes),
    MatMenuModule,
    FlyPanelShCategoryModule,
  ]
})
export class ShCategoryDetailsModule { }

import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ToolCategoryDetailsComponent } from './tool-category-details.component';
import { BaseModule } from 'src/app/components/base/base.module';
import { RouterModule, Routes } from '@angular/router';
import { MatLegacyMenuModule as MatMenuModule } from '@angular/material/legacy-menu';
import { FlyPanelAddToolCategoryModule } from '../fly-panel-add-tool-category/fly-panel-add-tool-category.module';
import { MatLegacyTooltipModule as MatTooltipModule } from '@angular/material/legacy-tooltip';

const routes: Routes = [
  {
    path: ':id',
    component: ToolCategoryDetailsComponent,
  },
];



@NgModule({
  declarations: [
    ToolCategoryDetailsComponent
  ],
  imports: [
    CommonModule,
    BaseModule,
    RouterModule.forChild(routes),
    MatMenuModule,
    FlyPanelAddToolCategoryModule,
    MatTooltipModule
  ]
})
export class ToolCategoryDetailsModule { }

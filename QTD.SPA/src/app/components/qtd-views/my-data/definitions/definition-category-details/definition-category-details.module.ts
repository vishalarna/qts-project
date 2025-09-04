import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DefinitionCategoryDetailsComponent } from './definition-category-details.component';
import { RouterModule, Routes } from '@angular/router';
import { BaseModule } from 'src/app/components/base/base.module';
import { MatLegacyMenuModule as MatMenuModule } from '@angular/material/legacy-menu';
import { MatLegacyTabsModule as MatTabsModule } from '@angular/material/legacy-tabs';
import { FlyPanelAddDefinitionCategoryModule } from '../fly-panel-add-definition-category/fly-panel-add-definition-category.module';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';

const routes: Routes = [
  {
    path: ':id',
    component: DefinitionCategoryDetailsComponent,
  }
 ]


@NgModule({
  declarations: [
    DefinitionCategoryDetailsComponent
  ],
  imports: [
    CommonModule,
    BaseModule,
    RouterModule.forChild(routes),
    MatMenuModule,
    MatTabsModule,
    MatCheckboxModule,
    FlyPanelAddDefinitionCategoryModule
  ]
})
export class DefinitionCategoryDetailsModule { }

import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DefinitionDetailsComponent } from './definition-details.component';
import { RouterModule, Routes } from '@angular/router';
import { BaseModule } from 'src/app/components/base/base.module';
import { MatLegacyMenuModule as MatMenuModule } from '@angular/material/legacy-menu';
import { MatLegacyTabsModule as MatTabsModule } from '@angular/material/legacy-tabs';
import { FlyPanelAddDefinitionModule } from '../fly-panel-add-definition/fly-panel-add-definition.module';

const routes: Routes = [
  {
    path: ':id',
    component: DefinitionDetailsComponent,
  }
 ]



@NgModule({
  declarations: [
    DefinitionDetailsComponent
  ],
  imports: [
    CommonModule,
    BaseModule,
    MatMenuModule,
    MatTabsModule,
    RouterModule.forChild(routes),
    FlyPanelAddDefinitionModule
  ]
})
export class DefinitionDetailsModule { }

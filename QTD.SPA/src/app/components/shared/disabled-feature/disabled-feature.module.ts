import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BaseModule } from 'src/app/components/base/base.module';
import {LocalizeModule} from 'src/app/_Shared/modules/localize.module';
import { HttpClientModule } from '@angular/common/http';
import { LayoutModule } from '../../qtd-views/layout/layout.module';
import { DisabledFeatureComponent } from './disabled-feature.component';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
    {
      path: '',
      component: DisabledFeatureComponent,
      children: []
    }];

@NgModule({
  declarations: [
    DisabledFeatureComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    LayoutModule,
    ReactiveFormsModule,
    RouterModule.forChild(routes),
    BaseModule,
    ReactiveFormsModule,
    HttpClientModule,
    LocalizeModule,
  ],
  exports: [
    DisabledFeatureComponent
  ]
})

export class DisabledFeatureModule { }

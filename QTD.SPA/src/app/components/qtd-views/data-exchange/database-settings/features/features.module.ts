import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import {BaseModule} from "../../../../base/base.module";
import { FeaturesComponent } from './features.component';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { RouterModule } from '@angular/router';

@NgModule({
  declarations: [
    FeaturesComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    BaseModule,
    MatCheckboxModule,
    RouterModule
  ], 
  exports: [
    FeaturesComponent
  ]
})
export class FeaturesModule { }

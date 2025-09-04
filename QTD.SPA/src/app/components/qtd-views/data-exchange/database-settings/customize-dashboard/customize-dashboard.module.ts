import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CustomizeDashboardComponent } from './customize-dashboard.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatExpansionModule } from '@angular/material/expansion';
import {BaseModule} from "../../../../base/base.module";

@NgModule({
  declarations: [
    CustomizeDashboardComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    MatExpansionModule,
    BaseModule
  ],
  exports: [
    CustomizeDashboardComponent
  ]
})
export class CustomizeDashboardModule { }

import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ProductOverviewComponent } from './product-overview.component';
import { FormsModule, ReactiveFormsModule  } from '@angular/forms';
import {  MatLegacyTableModule as MatTableModule } from '@angular/material/legacy-table';
import {BaseModule} from "../../../../base/base.module";

@NgModule({
  declarations: [
    ProductOverviewComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    MatTableModule,
    BaseModule,
    ReactiveFormsModule
  ],
  exports: [
    ProductOverviewComponent
  ]
})
export class ProductOverviewModule { }

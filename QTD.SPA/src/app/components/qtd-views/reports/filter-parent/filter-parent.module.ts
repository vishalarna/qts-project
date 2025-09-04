import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BaseModule } from 'src/app/components/base/base.module';
import { FilterParentComponent } from './filter-parent.component';
import { MatLegacyPaginatorModule as MatPaginatorModule } from '@angular/material/legacy-paginator';
import { FilterListModule } from '../filter-list/filter-list.module';
import { FilterSingleModule } from '../filter-single/filter-single.module';
import { FilterRangeModule } from '../filter-range/filter-range.module';


@NgModule({
  declarations: [
    FilterParentComponent
  ],
  imports: [
    CommonModule,
    BaseModule,
    MatPaginatorModule,
    FilterListModule,
    FilterSingleModule,
    FilterRangeModule
  ],
  exports: [
    FilterParentComponent
  ]
})
export class FilterParentModule { }

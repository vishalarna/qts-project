import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FilterRangeComponent } from './filter-range.component';
import { BaseModule } from 'src/app/components/base/base.module';
import { FormsModule } from '@angular/forms';

@NgModule({
  declarations: [
    FilterRangeComponent
  ],
  imports: [
    CommonModule,
    BaseModule,
    FormsModule
  ],
  exports: [
    FilterRangeComponent
  ]
})
export class FilterRangeModule { }

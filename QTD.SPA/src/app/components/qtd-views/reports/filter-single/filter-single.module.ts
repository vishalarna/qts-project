import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FilterSingleComponent } from './filter-single.component';
import { BaseModule } from 'src/app/components/base/base.module';
import { MatLegacySelectModule as MatSelectModule } from '@angular/material/legacy-select';

@NgModule({
  declarations: [
    FilterSingleComponent
  ],
  imports: [
    CommonModule,
    BaseModule,
    MatSelectModule
  ],
  exports:[
    FilterSingleComponent
  ]
  
})
export class FilterSingleModule { }

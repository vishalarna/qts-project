import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FlypanelFilterTqEmpByComponent } from './flypanel-filter-tq-emp-by.component';
import { BaseModule } from 'src/app/components/base/base.module';
import { MatLegacySelectModule as MatSelectModule } from '@angular/material/legacy-select';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';



@NgModule({
  declarations: [
    FlypanelFilterTqEmpByComponent
  ],
  imports: [
    CommonModule,
    BaseModule,
    MatSelectModule,
    FormsModule,
    ReactiveFormsModule,
  ],
  exports : [
    FlypanelFilterTqEmpByComponent,
  ]
})
export class FlypanelFilterTqEmpByModule { }

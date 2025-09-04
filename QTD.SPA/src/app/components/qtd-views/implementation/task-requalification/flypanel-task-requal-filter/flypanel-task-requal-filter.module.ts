import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FlypanelTaskRequalFilterComponent } from './flypanel-task-requal-filter.component';
import { BaseModule } from 'src/app/components/base/base.module';
import { MatLegacySelectModule as MatSelectModule } from '@angular/material/legacy-select';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';



@NgModule({
  declarations: [
    FlypanelTaskRequalFilterComponent
  ],
  imports: [
    CommonModule,
    BaseModule,
    MatSelectModule,
    MatCheckboxModule,
    ReactiveFormsModule,
    FormsModule,
  ],
  exports : [
    FlypanelTaskRequalFilterComponent,
  ]
})
export class FlypanelTaskRequalFilterModule { }

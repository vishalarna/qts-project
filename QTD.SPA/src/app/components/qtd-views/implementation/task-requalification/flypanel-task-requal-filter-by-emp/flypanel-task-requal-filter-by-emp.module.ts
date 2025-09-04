import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FlypanelTaskRequalFilterByEmpComponent } from './flypanel-task-requal-filter-by-emp.component';
import { BaseModule } from 'src/app/components/base/base.module';
import { MatLegacySelectModule as MatSelectModule } from '@angular/material/legacy-select';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatLegacyInputModule as MatInputModule } from '@angular/material/legacy-input';
import { MatIconModule } from '@angular/material/icon';



@NgModule({
  declarations: [
    FlypanelTaskRequalFilterByEmpComponent
  ],
  imports: [
    CommonModule,
    BaseModule,
    MatSelectModule,
    ReactiveFormsModule,
    FormsModule,
    MatInputModule,
    MatIconModule
  ],
  exports : [
    FlypanelTaskRequalFilterByEmpComponent,
  ]
})
export class FlypanelTaskRequalFilterByEmpModule { }

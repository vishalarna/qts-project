import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatLegacySelectModule as MatSelectModule } from '@angular/material/legacy-select';
import { ReactiveFormsModule } from '@angular/forms';
import { BaseModule } from 'src/app/components/base/base.module';
import { LayoutModule } from '../../../layout/layout.module';
import { MatLegacyFormFieldModule as MatFormFieldModule } from '@angular/material/legacy-form-field';
import { MatIconModule } from '@angular/material/icon';
import { FlypanelFilterEmployeeTaskQualificationComponent } from './flypanel-filter-employee-task-qualification.component';

@NgModule({
  declarations: [FlypanelFilterEmployeeTaskQualificationComponent],
  imports: [
    CommonModule,
    MatSelectModule,
    ReactiveFormsModule,
    BaseModule,
    LayoutModule,
    ReactiveFormsModule,
    MatSelectModule,
    MatFormFieldModule,
    MatIconModule
  ],
  exports:[FlypanelFilterEmployeeTaskQualificationComponent]
})
export class FlypanelFilterEmployeeTaskQualificationModule { }

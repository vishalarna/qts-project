import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BaseModule } from 'src/app/components/base/base.module';
import { LayoutModule } from '@angular/cdk/layout';
import { TrainingDepartmentSignOffComponent } from './training-department-sign-off.component';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';


@NgModule({
  declarations: [
    TrainingDepartmentSignOffComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    BaseModule,
    ReactiveFormsModule,
    LayoutModule,
    MatCheckboxModule
  ],
  exports:[TrainingDepartmentSignOffComponent]
})
export class TrainingDepartmentSignOffModule { }

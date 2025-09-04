import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { IlaEmpTestSettingsComponent } from './ila-emp-test-settings.component';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { MatLegacySelectModule as MatSelectModule } from '@angular/material/legacy-select';
import { BaseModule } from 'src/app/components/base/base.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';



@NgModule({
  declarations: [
    IlaEmpTestSettingsComponent
  ],
  imports: [
    CommonModule,
    BaseModule,
    MatSelectModule,
    MatCheckboxModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule
  ],
  exports: [
    IlaEmpTestSettingsComponent,
  ]
})
export class IlaEmpTestSettingsModule { }

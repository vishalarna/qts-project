import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FlypanelEmployeesWaitlistComponent } from './flypanel-employees-waitlist.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { MatLegacyFormFieldModule as MatFormFieldModule } from '@angular/material/legacy-form-field';
import { BaseModule } from 'src/app/components/base/base.module';
import { LayoutModule } from 'src/app/components/qtd-views/layout/layout.module';



@NgModule({
  declarations: [
    FlypanelEmployeesWaitlistComponent
  ],
  imports: [
    CommonModule,
    BaseModule,
    FormsModule,
    ReactiveFormsModule,
    MatCheckboxModule,
    MatFormFieldModule,
    LayoutModule,
  ],
  exports:[FlypanelEmployeesWaitlistComponent]
})
export class FlypanelEmployeesWaitlistModule { }

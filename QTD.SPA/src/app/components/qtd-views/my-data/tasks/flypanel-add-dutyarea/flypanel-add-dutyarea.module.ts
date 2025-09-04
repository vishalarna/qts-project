import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FlypanelAddDutyareaComponent } from './flypanel-add-dutyarea.component';
import { BaseModule } from 'src/app/components/base/base.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';

@NgModule({
  declarations: [FlypanelAddDutyareaComponent],
  imports: [
    CommonModule,
    BaseModule,
    FormsModule,
    ReactiveFormsModule,
    MatCheckboxModule,
  ],
  exports: [FlypanelAddDutyareaComponent],
})
export class FlypanelAddDutyareaModule {}

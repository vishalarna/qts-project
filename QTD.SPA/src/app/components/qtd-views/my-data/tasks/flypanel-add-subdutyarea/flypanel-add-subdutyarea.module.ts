import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FlypanelAddSubdutyareaComponent } from './flypanel-add-subdutyarea.component';
import { BaseModule } from 'src/app/components/base/base.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { MatLegacySelectModule as MatSelectModule } from '@angular/material/legacy-select';

@NgModule({
  declarations: [FlypanelAddSubdutyareaComponent],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    BaseModule,
    MatCheckboxModule,
    MatSelectModule,
  ],
  exports: [FlypanelAddSubdutyareaComponent],
})
export class FlypanelAddSubdutyareaModule {}

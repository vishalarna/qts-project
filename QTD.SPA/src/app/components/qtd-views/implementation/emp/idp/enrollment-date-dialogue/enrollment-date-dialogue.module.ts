
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { EnrollmentDateDialogueComponent } from './enrollment-date-dialogue.component';

import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatLegacyDialogModule as MatDialogModule } from '@angular/material/legacy-dialog';
import { BaseModule } from 'src/app/components/base/base.module';
import { MatLegacySelectModule as MatSelectModule } from '@angular/material/legacy-select';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';



@NgModule({
  declarations: [
    EnrollmentDateDialogueComponent
  ],
  imports: [
    CommonModule,
    MatDialogModule,
    BaseModule,
    FormsModule,
    MatSelectModule,
    MatCheckboxModule,
    FormsModule,
    ReactiveFormsModule
  ],
  exports:[EnrollmentDateDialogueComponent]

})
export class EnrollmentDateDialogueModule { }


import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DisclaimerDialogComponent } from './disclaimer-dialog.component';
import { MatLegacyDialogModule as MatDialogModule } from '@angular/material/legacy-dialog';
import { BaseModule } from 'src/app/components/base/base.module';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { ReactiveFormsModule } from '@angular/forms';



@NgModule({
  declarations: [
    DisclaimerDialogComponent
  ],
  imports: [
    CommonModule,
    MatDialogModule,
    BaseModule,
    MatCheckboxModule,
    ReactiveFormsModule,
  ],
  exports: [
    DisclaimerDialogComponent
  ]
})
export class DisclaimerDialogModule { }

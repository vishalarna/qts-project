import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TaskRequalUpdateDateDialogComponent } from './task-requal-update-date-dialog.component';
import { BaseModule } from 'src/app/components/base/base.module';
import { MatLegacyDialogModule as MatDialogModule } from '@angular/material/legacy-dialog';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatLegacyFormFieldModule as MatFormFieldModule } from '@angular/material/legacy-form-field';
import { MatLegacyInputModule as MatInputModule } from '@angular/material/legacy-input';



@NgModule({
  declarations: [
    TaskRequalUpdateDateDialogComponent
  ],
  imports: [
    CommonModule,
    BaseModule,
    MatDialogModule,
    FormsModule,
    MatFormFieldModule,
    ReactiveFormsModule,
    MatInputModule
  ],
  exports : [
    TaskRequalUpdateDateDialogComponent,
  ]
})
export class TaskRequalUpdateDateDialogModule { }

import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TaskRequalReassignEvalDialogComponent } from './task-requal-reassign-eval-dialog.component';
import { BaseModule } from 'src/app/components/base/base.module';
import { MatLegacySelectModule as MatSelectModule } from '@angular/material/legacy-select';
import { MatLegacyChipsModule as MatChipsModule } from '@angular/material/legacy-chips';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatLegacyDialogModule as MatDialogModule } from '@angular/material/legacy-dialog';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';



@NgModule({
  declarations: [
    TaskRequalReassignEvalDialogComponent
  ],
  imports: [
    CommonModule,
    BaseModule,
    MatSelectModule,
    MatChipsModule,
    ReactiveFormsModule,
    FormsModule,
    MatDialogModule,
    MatCheckboxModule,
  ],
  exports : [
    TaskRequalReassignEvalDialogComponent,
  ]
})
export class TaskRequalReassignEvalDialogModule { }

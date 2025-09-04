import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BaseModule } from 'src/app/components/base/base.module';
import { MatLegacyDialogModule as MatDialogModule } from '@angular/material/legacy-dialog';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { TaskRequalReassignTaskRequalComponent } from './task-requal-reassign-task-requal.component';
import { MatLegacySelectModule as MatSelectModule } from '@angular/material/legacy-select';
import { MatLegacyChipsModule as MatChipsModule } from '@angular/material/legacy-chips';
import { MatLegacyRadioModule as MatRadioModule } from '@angular/material/legacy-radio';
import { MatLegacyFormFieldModule as MatFormFieldModule } from '@angular/material/legacy-form-field';
import { MatLegacyInputModule as MatInputModule } from '@angular/material/legacy-input';



@NgModule({
  declarations: [TaskRequalReassignTaskRequalComponent],
  imports: [
    CommonModule,
    BaseModule,
    MatDialogModule,
    FormsModule,
    MatSelectModule,
    MatChipsModule,
    ReactiveFormsModule,
    MatRadioModule,
    MatFormFieldModule,
    MatInputModule
  ],
  exports:[
    TaskRequalReassignTaskRequalComponent,
  ]
})
export class TaskRequalReassignTaskRequalModule { }

import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FlypanelAssignedTaskQualificationComponent } from './flypanel-assigned-task-qualification.component';
import { BaseModule } from 'src/app/components/base/base.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatLegacyTableModule as MatTableModule } from '@angular/material/legacy-table';
import { MatExpansionModule } from '@angular/material/expansion';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { MatDividerModule } from '@angular/material/divider';
import { MatLegacyListModule as MatListModule } from '@angular/material/legacy-list'
import { TaskRequalReassignEvalDialogModule } from '../task-requal-reassign-eval-dialog/task-requal-reassign-eval-dialog.module';



@NgModule({
  declarations: [
    FlypanelAssignedTaskQualificationComponent
  ],
  imports: [
    CommonModule,
    BaseModule,
    FormsModule,
    ReactiveFormsModule,
    MatTableModule,
    MatExpansionModule,
    MatCheckboxModule,
    MatDividerModule,
    MatListModule,
    TaskRequalReassignEvalDialogModule,
  ],
  exports : [
    FlypanelAssignedTaskQualificationComponent,
  ]
})
export class FlypanelAssignedTaskQualificationModule { }

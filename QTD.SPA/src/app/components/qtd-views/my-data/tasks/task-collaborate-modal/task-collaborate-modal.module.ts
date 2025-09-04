import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TaskCollaborateModalComponent } from './task-collaborate-modal.component';
import { MatLegacyDialogModule as MatDialogModule } from '@angular/material/legacy-dialog';
import { MatLegacyTableModule as MatTableModule } from '@angular/material/legacy-table';
import { CKEditorModule } from '@ckeditor/ckeditor5-angular';
import { BaseModule } from 'src/app/components/base/base.module';
import { MatLegacySelectModule as MatSelectModule } from '@angular/material/legacy-select';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';



@NgModule({
  declarations: [
    TaskCollaborateModalComponent
  ],
  imports: [
    CommonModule,
    BaseModule,
    MatDialogModule,
    MatTableModule,
    CKEditorModule,
    MatSelectModule,
    FormsModule,
    ReactiveFormsModule,
  ],
  exports : [
    TaskCollaborateModalComponent
  ]
})
export class TaskCollaborateModalModule { }

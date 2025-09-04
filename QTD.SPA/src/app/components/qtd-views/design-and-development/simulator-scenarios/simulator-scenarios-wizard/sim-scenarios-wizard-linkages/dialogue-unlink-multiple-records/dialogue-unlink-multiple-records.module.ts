import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BaseModule } from 'src/app/components/base/base.module';
import { MatLegacyDialogModule as MatDialogModule } from '@angular/material/legacy-dialog';
import { FormsModule } from '@angular/forms';
import { DialogueUnlinkMultipleRecordsComponent } from './dialogue-unlink-multiple-records.component';
import { MatLegacyTableModule as MatTableModule } from '@angular/material/legacy-table';



@NgModule({
  declarations: [
    DialogueUnlinkMultipleRecordsComponent
  ],
  imports: [
    CommonModule,
    BaseModule,
    MatDialogModule,
    FormsModule,
    MatTableModule
  ],
  exports:[DialogueUnlinkMultipleRecordsComponent]
})
export class DialogueUnlinkMultipleRecordsModule { }
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BaseModule } from 'src/app/components/base/base.module';
import { MatLegacyDialogModule as MatDialogModule } from '@angular/material/legacy-dialog';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { FormsModule } from '@angular/forms';
import { DialogueUnlinkR5TasksComponent } from './dialogue-unlink-r5-tasks.component';
import { MatLegacyTableModule as MatTableModule } from '@angular/material/legacy-table';



@NgModule({
  declarations: [
    DialogueUnlinkR5TasksComponent
  ],
  imports: [
    CommonModule,
    BaseModule,
    MatDialogModule,
    MatCheckboxModule,
    FormsModule,
    MatTableModule

  ],
  exports:[DialogueUnlinkR5TasksComponent]
})
export class DialogueUnlinkR5TasksModule { }
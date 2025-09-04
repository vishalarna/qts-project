import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DeleteEmpDialogueComponent } from './delete-emp-dialogue.component';
import { BaseModule } from 'src/app/components/base/base.module';
import { MatLegacyDialog as MatDialog, MatLegacyDialogModule as MatDialogModule } from '@angular/material/legacy-dialog';
import { FormsModule } from '@angular/forms';
import { MatLegacyChipsModule as MatChipsModule } from '@angular/material/legacy-chips';



@NgModule({
  declarations: [
    DeleteEmpDialogueComponent
  ],
  imports: [
    CommonModule,
    BaseModule,
    MatDialogModule,
    FormsModule,
    MatChipsModule
  ],
  exports:[DeleteEmpDialogueComponent]
})

export class DeleteEmpDialogueModule { }

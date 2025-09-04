import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UpdateProcedureDialogueComponent } from './update-procedure-dialogue.component';
import { MatLegacyDialogModule as MatDialogModule } from '@angular/material/legacy-dialog';
import { BaseModule } from 'src/app/components/base/base.module';
import { MatLegacyTableModule as MatTableModule } from '@angular/material/legacy-table';
import { MatLegacyChipsModule as MatChipsModule } from '@angular/material/legacy-chips';



@NgModule({
  declarations: [
    UpdateProcedureDialogueComponent
  ],
  imports: [
    CommonModule,
    BaseModule,
    MatDialogModule,
    MatTableModule,
    MatChipsModule
  ],
  exports:[
    UpdateProcedureDialogueComponent
  ]
})
export class UpdateProcedureDialogueModule { }

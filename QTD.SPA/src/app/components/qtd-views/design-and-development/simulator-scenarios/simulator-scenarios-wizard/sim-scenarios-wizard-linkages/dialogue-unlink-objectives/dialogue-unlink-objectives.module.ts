import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BaseModule } from 'src/app/components/base/base.module';
import { MatLegacyDialogModule as MatDialogModule } from '@angular/material/legacy-dialog';
import { FormsModule } from '@angular/forms';
import { DialogueUnlinkObjectivesComponent } from './dialogue-unlink-objectives.component';
import { MatLegacyTableModule as MatTableModule } from '@angular/material/legacy-table';



@NgModule({
  declarations: [
    DialogueUnlinkObjectivesComponent
  ],
  imports: [
    CommonModule,
    BaseModule,
    MatDialogModule,
    FormsModule,
    MatTableModule
  ],
  exports:[DialogueUnlinkObjectivesComponent]
})
export class DialogueUnlinkObjectivesModule { }
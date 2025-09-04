import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PlannedDateDialogueComponent } from './planned-date-dialogue.component';
import { FormsModule } from '@angular/forms';
import { MatLegacyDialogModule as MatDialogModule } from '@angular/material/legacy-dialog';
import { BaseModule } from 'src/app/components/base/base.module';



@NgModule({
  declarations: [
    PlannedDateDialogueComponent
  ],
  imports: [
    CommonModule,
    MatDialogModule,
    BaseModule,
    FormsModule,

  ],
  exports:[PlannedDateDialogueComponent]

})
export class PlannedDateDialogueModule { }

import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActiveInactiveDialogueComponent } from './active-inactive-dialogue.component';
import { BaseModule } from 'src/app/components/base/base.module';
import { MatLegacyDialog as MatDialog, MatLegacyDialogModule as MatDialogModule } from '@angular/material/legacy-dialog';
import { FormsModule } from '@angular/forms';

@NgModule({
  declarations: [ActiveInactiveDialogueComponent],
  imports: [
    CommonModule,
    BaseModule,
    MatDialogModule,
    FormsModule,
  ],
  exports:[ActiveInactiveDialogueComponent]
})
export class ActiveInactiveDialogueModule { }

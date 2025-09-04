import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SubmitTestDialogueComponent } from './submit-test-dialogue.component';
import { BaseModule } from 'src/app/components/base/base.module';
import { MatLegacyDialogModule as MatDialogModule } from '@angular/material/legacy-dialog';



@NgModule({
  declarations: [
    SubmitTestDialogueComponent
  ],
  imports: [
    CommonModule,
    BaseModule,
    MatDialogModule
  ],
  exports:[SubmitTestDialogueComponent]
})
export class SubmitTestDialogueModule { }

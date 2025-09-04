import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RemoveEmployeesDialogueComponent } from './remove-employees-dialogue.component';
import { FormsModule } from '@angular/forms';
import { MatLegacyChipsModule as MatChipsModule } from '@angular/material/legacy-chips';
import { MatLegacyDialogModule as MatDialogModule, MatLegacyDialogRef as MatDialogRef, MAT_LEGACY_DIALOG_DATA as MAT_DIALOG_DATA } from '@angular/material/legacy-dialog';
import { BaseModule } from 'src/app/components/base/base.module';



@NgModule({
  declarations: [
    RemoveEmployeesDialogueComponent
  ],
  imports: [
    CommonModule,
    BaseModule,
    MatDialogModule,
    FormsModule,
    MatChipsModule
  ],

  exports:[RemoveEmployeesDialogueComponent]
})
export class RemoveEmployeesDialogueModule { }

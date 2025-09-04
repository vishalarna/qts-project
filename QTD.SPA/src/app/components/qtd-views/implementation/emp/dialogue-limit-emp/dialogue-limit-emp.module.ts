import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DialogueLimitEmpComponent } from './dialogue-limit-emp.component';
import { BaseModule } from 'src/app/components/base/base.module';
import { MatLegacyDialogModule as MatDialogModule } from '@angular/material/legacy-dialog';



@NgModule({
  declarations: [
    DialogueLimitEmpComponent
  ],
  imports: [
    CommonModule,
    BaseModule,
  ],
  exports:[DialogueLimitEmpComponent]
})
export class DialogueLimitEmpModule { }

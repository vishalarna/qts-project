import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FillEvalFormDialogComponent } from './fill-eval-form-dialog.component';
import { BaseModule } from 'src/app/components/base/base.module';
import { MatLegacyDialogModule as MatDialogModule } from '@angular/material/legacy-dialog';
import { MatLegacyRadioModule as MatRadioModule } from '@angular/material/legacy-radio';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';



@NgModule({
  declarations: [
    FillEvalFormDialogComponent
  ],
  imports: [
    CommonModule,
    BaseModule,
    MatDialogModule,
    MatRadioModule,
    ReactiveFormsModule,
    FormsModule,
  ],
  exports:[
    FillEvalFormDialogComponent,
  ]
})
export class FillEvalFormDialogModule { }

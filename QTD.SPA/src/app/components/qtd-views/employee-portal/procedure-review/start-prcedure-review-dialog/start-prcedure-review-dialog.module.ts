import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { StartPrcedureReviewDialogComponent } from './start-prcedure-review-dialog.component';
import { BaseModule } from 'src/app/components/base/base.module';
import { LayoutModule } from '../../../layout/layout.module';
import { FormsModule } from '@angular/forms';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { MatLegacyDialogModule as MatDialogModule } from '@angular/material/legacy-dialog';



@NgModule({
  declarations: [
    StartPrcedureReviewDialogComponent
  ],
  imports: [
    CommonModule,
    BaseModule,
    LayoutModule,
    MatDialogModule,
    MatCheckboxModule,
    FormsModule
  ]
})
export class StartPrcedureReviewDialogModule { }

import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RecallTaskQualificationDialogComponent } from './recall-task-qualification-dialog.component';
import { BaseModule } from 'src/app/components/base/base.module';
import { MatLegacyDialogModule as MatDialogModule } from '@angular/material/legacy-dialog';
import { MatLegacyTableModule as MatTableModule } from '@angular/material/legacy-table';
import { MatLegacyPaginatorModule as MatPaginatorModule } from '@angular/material/legacy-paginator';



@NgModule({
  declarations: [
    RecallTaskQualificationDialogComponent
  ],
  imports: [
    CommonModule,
    BaseModule,
    MatDialogModule,
    MatTableModule,
    MatPaginatorModule,
  ],
  exports: [
    RecallTaskQualificationDialogComponent,
  ]
})
export class RecallTaskQualificationDialogModule { }

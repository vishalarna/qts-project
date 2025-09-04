import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BaseModule } from 'src/app/components/base/base.module';
import { MatLegacyDialogModule as MatDialogModule } from '@angular/material/legacy-dialog';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RosterBulkUpdateDialogComponent } from './roster-bulk-update-dialog.component';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { MatLegacyTooltipModule as MatTooltipModule } from '@angular/material/legacy-tooltip';


@NgModule({
  declarations: [
    RosterBulkUpdateDialogComponent
  ],
  imports: [
    ReactiveFormsModule,
    CommonModule,
    BaseModule,
    MatDialogModule,
    FormsModule,
    MatCheckboxModule,
    MatTooltipModule
  ],
  exports : [
    RosterBulkUpdateDialogComponent,
  ]
})
export class  RosterBulkUpdateDialogModule { }
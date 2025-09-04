import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FlypanelBulkEditTestItemsComponent } from './flypanel-bulk-edit-test-items.component';
import { BaseModule } from 'src/app/components/base/base.module';
import { MatLegacyTabsModule as MatTabsModule } from '@angular/material/legacy-tabs';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { MatLegacySelectModule as MatSelectModule } from '@angular/material/legacy-select';
import { MatStepperModule } from '@angular/material/stepper';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatLegacyTableModule as MatTableModule } from '@angular/material/legacy-table';



@NgModule({
  declarations: [
    FlypanelBulkEditTestItemsComponent
  ],
  imports: [
    CommonModule,
    BaseModule,
    MatStepperModule,
    MatCheckboxModule,
    MatSelectModule,
    FormsModule,
    ReactiveFormsModule,
    MatTableModule,
  ],
  exports : [
    FlypanelBulkEditTestItemsComponent
  ]
})
export class FlypanelBulkEditTestItemsModule { }

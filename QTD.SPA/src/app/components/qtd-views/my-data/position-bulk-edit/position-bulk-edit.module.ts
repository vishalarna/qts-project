import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PositionBulkEditComponent } from './position-bulk-edit.component';
import { RouterModule, Routes } from '@angular/router';
import { BaseModule } from 'src/app/components/base/base.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatStepperModule } from '@angular/material/stepper';
import { MatLegacySelectModule as MatSelectModule } from '@angular/material/legacy-select';
import { MatLegacyChipsModule as MatChipsModule } from '@angular/material/legacy-chips';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { MatTreeModule } from '@angular/material/tree';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatLegacyTableModule as MatTableModule } from '@angular/material/legacy-table';
import { MatSortModule } from '@angular/material/sort';
import { MatLegacyMenuModule as MatMenuModule } from '@angular/material/legacy-menu';

const routes: Routes = [
  {
    path: '',
    component: PositionBulkEditComponent,
  }
 ]

@NgModule({
  declarations: [PositionBulkEditComponent],
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    BaseModule,
    FormsModule,
    ReactiveFormsModule,
    MatStepperModule,
    MatSelectModule,
    MatChipsModule,
    MatCheckboxModule,
    MatTreeModule,
    MatToolbarModule,
    MatTableModule,
    MatSortModule,
    MatMenuModule,
  ],
  exports: [PositionBulkEditComponent]
})
export class PositionBulkEditModule { }

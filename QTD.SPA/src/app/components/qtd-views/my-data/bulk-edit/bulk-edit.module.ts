import { NgModule } from '@angular/core';
import { CommonModule, TitleCasePipe } from '@angular/common';
import { BulkEditComponent } from './bulk-edit.component';
import { RouterModule, Routes } from '@angular/router';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { MatLegacyChipsModule as MatChipsModule } from '@angular/material/legacy-chips';
import { MatLegacySelectModule as MatSelectModule } from '@angular/material/legacy-select';
import { MatStepperModule } from '@angular/material/stepper';
import { BaseModule } from 'src/app/components/base/base.module';
import { MatTreeModule } from '@angular/material/tree';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatLegacyTableModule as MatTableModule } from '@angular/material/legacy-table';
import { MatSortModule } from '@angular/material/sort';
import { MatLegacyMenuModule as MatMenuModule } from '@angular/material/legacy-menu';

const routes: Routes = [
  {
    path: ':name',
    component: BulkEditComponent,
  },
];

@NgModule({
  declarations: [BulkEditComponent],
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    CommonModule,
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
  exports: [BulkEditComponent],
})
export class BulkEditModule {}

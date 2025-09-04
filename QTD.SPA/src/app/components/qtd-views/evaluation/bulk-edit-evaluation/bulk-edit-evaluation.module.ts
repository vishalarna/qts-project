import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BulkEditEvaluationComponent } from './bulk-edit-evaluation.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { MatLegacyChipsModule as MatChipsModule } from '@angular/material/legacy-chips';
import { MatLegacyMenuModule as MatMenuModule } from '@angular/material/legacy-menu';
import { MatLegacySelectModule as MatSelectModule } from '@angular/material/legacy-select';
import { MatSortModule } from '@angular/material/sort';
import { MatStepperModule } from '@angular/material/stepper';
import { MatLegacyTableModule as MatTableModule } from '@angular/material/legacy-table';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatTreeModule } from '@angular/material/tree';
import { Routes, RouterModule } from '@angular/router';
import { BaseModule } from 'src/app/components/base/base.module';
const routes: Routes = [
  {
    path: ':name',
    component: BulkEditEvaluationComponent,
  },
];



@NgModule({
  declarations: [
    BulkEditEvaluationComponent
  ],
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
  exports: [BulkEditEvaluationComponent],
})
export class BulkEditEvaluationModule { }

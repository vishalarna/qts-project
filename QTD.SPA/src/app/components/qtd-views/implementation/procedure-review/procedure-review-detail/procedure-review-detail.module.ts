import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ProcedureReviewDetailComponent } from './procedure-review-detail.component';
import { BaseModule } from 'src/app/components/base/base.module';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { MatIconModule } from '@angular/material/icon';
import { MatLegacyMenuModule as MatMenuModule } from '@angular/material/legacy-menu';
import { MatLegacySelectModule as MatSelectModule } from '@angular/material/legacy-select';
import { MatLegacyTableModule as MatTableModule } from '@angular/material/legacy-table';
import { MatLegacyTooltipModule as MatTooltipModule } from '@angular/material/legacy-tooltip';
import { NgChartsModule } from 'ng2-charts';
import { RouterModule, Routes } from '@angular/router';
import { LayoutModule } from '../../../layout/layout.module';
import { FlyPanelAddEmployeeModule } from '../fly-panel-add-procedure-review/fly-panel-add-employee/fly-panel-add-employee.module';
import { MatLegacyChipsModule as MatChipsModule } from '@angular/material/legacy-chips';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatLegacyDialogModule as MatDialogModule } from '@angular/material/legacy-dialog';

const routes: Routes = [
  {
    path: '',
    component: ProcedureReviewDetailComponent,
    
  },
  {
    path: ':id',
    component: ProcedureReviewDetailComponent,
    
  },
];

@NgModule({
  declarations: [
    ProcedureReviewDetailComponent
  ],
  imports: [
    CommonModule,
    BaseModule,
    MatTableModule,
    RouterModule.forChild(routes),
    NgChartsModule,
    MatSelectModule,
    MatMenuModule,
    ReactiveFormsModule,
    MatIconModule,
    FormsModule,
    MatTooltipModule,
    LayoutModule,
    FlyPanelAddEmployeeModule,
    MatChipsModule,
    MatCheckboxModule,
    MatToolbarModule,
    MatDialogModule,
  ],
})
export class ProcedureReviewDetailModule { }

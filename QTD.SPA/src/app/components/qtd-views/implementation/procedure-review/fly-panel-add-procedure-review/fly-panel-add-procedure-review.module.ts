import { BaseModule } from './../../../../base/base.module';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FlyPanelAddProcedureReviewComponent } from './fly-panel-add-procedure-review.component';
import { MatButtonToggleModule } from '@angular/material/button-toggle';
import { MatLegacyInputModule as MatInputModule } from '@angular/material/legacy-input';
import { MatLegacyRadioModule as MatRadioModule } from '@angular/material/legacy-radio';
import { MatLegacyTabsModule as MatTabsModule } from '@angular/material/legacy-tabs';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { MatExpansionModule } from '@angular/material/expansion';
import { MatLegacyMenuModule as MatMenuModule } from '@angular/material/legacy-menu';
import { LayoutModule } from '../../../layout/layout.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatLegacyPaginatorModule as MatPaginatorModule } from '@angular/material/legacy-paginator';
import { MatLegacySelectModule as MatSelectModule } from '@angular/material/legacy-select';
import { MatStepperModule } from '@angular/material/stepper';
import { MatLegacyTableModule as MatTableModule } from '@angular/material/legacy-table';
import { MatToolbarModule } from '@angular/material/toolbar';
import { CKEditorModule } from '@ckeditor/ckeditor5-angular';
import { RouterModule, Routes } from '@angular/router';
import { FlyPanelAddEmployeeModule } from './fly-panel-add-employee/fly-panel-add-employee.module';
import { MatLegacyDialogModule as MatDialogModule } from '@angular/material/legacy-dialog';
import { MatLegacyChipsModule as MatChipsModule } from '@angular/material/legacy-chips';
import { MatSortModule } from '@angular/material/sort';

const routes: Routes = [
  {
    path: '',
    component: FlyPanelAddProcedureReviewComponent,
  },
  {
    path: ':id',
    component: FlyPanelAddProcedureReviewComponent,
  },

];

@NgModule({
  declarations: [
    FlyPanelAddProcedureReviewComponent
  ],
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    MatRadioModule,
    MatInputModule,
    MatButtonToggleModule,
    MatTabsModule,
    MatCheckboxModule,
    MatExpansionModule,
    MatMenuModule,
    MatPaginatorModule,
    BaseModule,
    LayoutModule,
    MatTableModule,
    FormsModule,
    ReactiveFormsModule,
    CKEditorModule,
    MatStepperModule,
    MatSelectModule,
    MatToolbarModule,
    FlyPanelAddEmployeeModule,
    MatDialogModule,
    MatChipsModule,
    MatSortModule
  ]
})
export class FlyPanelAddProcedureReviewModule { }

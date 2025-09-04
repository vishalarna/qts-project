import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FlyPanelEnrollEmployeesComponent } from './fly-panel-enroll-employees.component';
import { LayoutModule } from '@angular/cdk/layout';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { MatLegacyFormFieldModule as MatFormFieldModule } from '@angular/material/legacy-form-field';
import { BaseModule } from 'src/app/components/base/base.module';
import { MatLegacyMenuModule as MatMenuModule } from '@angular/material/legacy-menu';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatLegacyPaginatorModule as MatPaginatorModule } from '@angular/material/legacy-paginator';
import { MatSortModule } from '@angular/material/sort';
import { MatLegacyTableModule as MatTableModule } from '@angular/material/legacy-table';
import { FlyPanelAssignScoreModule } from '../view-edit-grades/fly-panel-edit-roaster/fly-panel-assign-score/fly-panel-assign-score.module';
import { FlypanelFilterEmpByOrgModule } from '../../../task-requalification/flypanel-filter-emp-by-org/flypanel-filter-emp-by-org.module';
import { FlypanelFilterTqEmpByModule } from '../../../task-requalification/flypanel-filter-tq-emp-by/flypanel-filter-tq-emp-by.module';



@NgModule({
  declarations: [
    FlyPanelEnrollEmployeesComponent
  ],
  imports: [
    CommonModule,
    BaseModule,
    FormsModule,
    ReactiveFormsModule,
    MatCheckboxModule,
    MatFormFieldModule,
    LayoutModule,
    MatMenuModule,
    MatToolbarModule,
    MatTableModule,
    MatPaginatorModule,
    MatSortModule,
    FlyPanelAssignScoreModule,
    FlypanelFilterEmpByOrgModule,
    FlypanelFilterTqEmpByModule,
  ],
  exports:[FlyPanelEnrollEmployeesComponent]
})
export class FlyPanelEnrollEmployeesModule { }

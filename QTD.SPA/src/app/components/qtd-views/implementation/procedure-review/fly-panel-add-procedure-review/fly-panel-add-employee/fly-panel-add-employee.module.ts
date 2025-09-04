import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FlyPanelAddEmployeeComponent } from './fly-panel-add-employee.component';
import { LayoutModule } from 'src/app/components/qtd-views/layout/layout.module';
import { BaseModule } from 'src/app/components/base/base.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { FlyPanelAssignScoreModule } from '../../../schedulingclasses/scheduling-classes-overview/view-edit-grades/fly-panel-edit-roaster/fly-panel-assign-score/fly-panel-assign-score.module';
import { MatSortModule } from '@angular/material/sort';
import { MatLegacyPaginatorModule as MatPaginatorModule } from '@angular/material/legacy-paginator';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { MatLegacyFormFieldModule as MatFormFieldModule } from '@angular/material/legacy-form-field';
import { MatLegacyMenuModule as MatMenuModule } from '@angular/material/legacy-menu';
import { MatLegacyTableModule as MatTableModule } from '@angular/material/legacy-table';
import { MatToolbarModule } from '@angular/material/toolbar';
import { FlypanelFilterEmpByOrgModule } from '../../../task-requalification/flypanel-filter-emp-by-org/flypanel-filter-emp-by-org.module';
import { FlypanelFilterTqEmpByModule } from '../../../task-requalification/flypanel-filter-tq-emp-by/flypanel-filter-tq-emp-by.module';



@NgModule({
  declarations: [
    FlyPanelAddEmployeeComponent
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
  exports:[FlyPanelAddEmployeeComponent]
})
export class FlyPanelAddEmployeeModule { }

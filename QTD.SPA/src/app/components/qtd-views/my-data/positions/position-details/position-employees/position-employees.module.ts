import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PositionEmployeesComponent } from './position-employees.component';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { MatLegacyFormFieldModule as MatFormFieldModule } from '@angular/material/legacy-form-field';
import { MatLegacySelectModule as MatSelectModule } from '@angular/material/legacy-select';
import { MatLegacyTableModule as MatTableModule } from '@angular/material/legacy-table';
import { BaseModule } from 'src/app/components/base/base.module';
import { FlyPanelLinkPositionEmployeeModule } from '../../fly-panel-link-position-employee/fly-panel-link-position-employee.module';
import { MatLegacyPaginatorModule as MatPaginatorModule } from '@angular/material/legacy-paginator';
import { FlyPanelLinkedPositionsModule } from '../../fly-panel-linked-positions/fly-panel-linked-positions.module';
import { MatSortModule } from '@angular/material/sort';



@NgModule({
  declarations: [
    PositionEmployeesComponent
  ],
  imports: [
    CommonModule,
    BaseModule,
    MatCheckboxModule,
    MatTableModule,
    MatSortModule,
    MatFormFieldModule,
    MatSelectModule,
    FlyPanelLinkPositionEmployeeModule,
    MatPaginatorModule,
    FlyPanelLinkedPositionsModule,
  ],
  exports:[PositionEmployeesComponent]
})
export class PositionEmployeesModule { }

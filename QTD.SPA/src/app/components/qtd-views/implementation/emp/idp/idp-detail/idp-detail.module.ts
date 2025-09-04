import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { IdpDetailComponent } from './idp-detail.component';
import { BaseModule } from 'src/app/components/base/base.module';
import { LayoutModule } from 'src/app/components/qtd-views/layout/layout.module';
import { MatLegacyTableModule as MatTableModule } from '@angular/material/legacy-table';
import {MatLegacyPaginatorModule as MatPaginatorModule } from '@angular/material/legacy-paginator';
import { MatLegacyMenuModule as MatMenuModule } from '@angular/material/legacy-menu';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatLegacyInputModule as MatInputModule } from '@angular/material/legacy-input';
import { MatLegacySelectModule as MatSelectModule } from '@angular/material/legacy-select';
import { MatExpansionModule } from '@angular/material/expansion';
import { FlyPanelLinkedObjectivesModule } from '../fly-panel-linked-objectives/fly-panel-linked-objectives.module';
import { FlyPanelAddEditGradeModule } from '../fly-panel-add-edit-grade/fly-panel-add-edit-grade.module';
import { FlyPanelEnrollEmployeeModule } from '../fly-panel-enroll-employee/fly-panel-enroll-employee.module';
import { FlyPanelFilterIdpModule } from '../fly-panel-filter-idp/fly-panel-filter-idp.module';
import { FlyPanelDownlodIlasModule } from '../fly-panel-downlod-ilas/fly-panel-downlod-ilas.module';
import { MatIconModule } from '@angular/material/icon';
import { MatSortModule } from '@angular/material/sort';



@NgModule({
  declarations: [
    IdpDetailComponent
  ],
  imports: [
    CommonModule,
    BaseModule,
    LayoutModule,
    MatTableModule,
    MatPaginatorModule,
    MatMenuModule,
    FormsModule,
    ReactiveFormsModule,
    MatInputModule,
    MatSelectModule,
    MatExpansionModule,
    FlyPanelLinkedObjectivesModule,
    FlyPanelAddEditGradeModule,
    FlyPanelEnrollEmployeeModule,
    FlyPanelFilterIdpModule,
    FlyPanelDownlodIlasModule,
    MatIconModule,
    MatSortModule
  ],
  exports:[IdpDetailComponent]
})
export class IdpDetailModule { }

import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { EoEmployeesComponent } from './eo-employees.component';
import { BaseModule } from 'src/app/components/base/base.module';
import { MatLegacyTableModule as MatTableModule } from '@angular/material/legacy-table';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { MatLegacyPaginatorModule as MatPaginatorModule } from '@angular/material/legacy-paginator';
import { MatSortModule } from '@angular/material/sort';
import { FlypanelEoEmployeeLinkModule } from '../../flypanel-eo-employee-link/flypanel-eo-employee-link.module';
import { FlypanelLinkedEosModule } from '../../flypanel-linked-eos/flypanel-linked-eos.module';



@NgModule({
  declarations: [
    EoEmployeesComponent
  ],
  imports: [
    CommonModule,
    BaseModule,
    MatTableModule,
    MatCheckboxModule,
    MatPaginatorModule,
    MatSortModule,
    FlypanelEoEmployeeLinkModule,
    FlypanelLinkedEosModule,
  ],
  exports : [
    EoEmployeesComponent,
  ]
})
export class EoEmployeesModule { }

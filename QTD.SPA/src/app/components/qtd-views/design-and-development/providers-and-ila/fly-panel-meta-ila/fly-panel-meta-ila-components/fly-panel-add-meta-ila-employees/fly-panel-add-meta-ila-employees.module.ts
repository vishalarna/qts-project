import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FlyPanelAddMetaILAEmployeesComponent } from './fly-panel-add-meta-ila-employees.component';
import { FormsModule } from '@angular/forms';
import { BaseModule } from 'src/app/components/base/base.module';
import { MatLegacyMenuModule as MatMenuModule } from '@angular/material/legacy-menu';
import { MatLegacyTableModule as MatTableModule } from '@angular/material/legacy-table';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { MatSortModule } from '@angular/material/sort';
import { FlypanelFilterEmpByOrgModule } from 'src/app/components/qtd-views/implementation/task-requalification/flypanel-filter-emp-by-org/flypanel-filter-emp-by-org.module';
import { FlypanelFilterTqEmpByModule } from 'src/app/components/qtd-views/implementation/task-requalification/flypanel-filter-tq-emp-by/flypanel-filter-tq-emp-by.module';
import { MatToolbarModule } from '@angular/material/toolbar';


@NgModule({
  declarations: [
    FlyPanelAddMetaILAEmployeesComponent
  ],
  imports: [
    CommonModule,
    BaseModule,
    FormsModule,
    MatMenuModule,
    MatTableModule,
    MatCheckboxModule,
    MatSortModule,
    FlypanelFilterEmpByOrgModule,
    FlypanelFilterTqEmpByModule,
    MatToolbarModule
  ],
  exports:[FlyPanelAddMetaILAEmployeesComponent]
})
export class FlyPanelAddMetaILAEmployeesModule { }

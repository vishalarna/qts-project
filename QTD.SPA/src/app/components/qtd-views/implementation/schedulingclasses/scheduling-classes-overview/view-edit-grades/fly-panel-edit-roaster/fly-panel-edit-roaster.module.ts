import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FlyPanelEditRoasterComponent } from './fly-panel-edit-roaster.component';
import { LayoutModule } from '@angular/cdk/layout';
import { MatLegacySelectModule as MatSelectModule } from '@angular/material/legacy-select';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BaseModule } from 'src/app/components/base/base.module';
import { MatSortModule } from '@angular/material/sort';
import { MatLegacyPaginatorModule as MatPaginatorModule } from '@angular/material/legacy-paginator';
import { MatLegacyTableModule as MatTableModule } from '@angular/material/legacy-table';
import { MatLegacyMenuModule as MatMenuModule } from '@angular/material/legacy-menu';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { MatToolbarModule } from '@angular/material/toolbar';
import { FlyPanelAssignScoreComponent } from './fly-panel-assign-score/fly-panel-assign-score.component';
import { FlyPanelAssignScoreModule } from './fly-panel-assign-score/fly-panel-assign-score.module';
import { FlypanelFilterEmpByOrgModule } from '../../../../task-requalification/flypanel-filter-emp-by-org/flypanel-filter-emp-by-org.module';
import { FlypanelFilterTqEmpByModule } from '../../../../task-requalification/flypanel-filter-tq-emp-by/flypanel-filter-tq-emp-by.module';



@NgModule({
  declarations: [FlyPanelEditRoasterComponent],
  imports: [
    FormsModule,
    ReactiveFormsModule,
    CommonModule,
    BaseModule,
    LayoutModule,
    MatSelectModule,
    MatTableModule,
    MatPaginatorModule,
    MatSortModule,
    MatMenuModule,
    MatCheckboxModule,
    MatToolbarModule,
    FlyPanelAssignScoreModule,
    FlypanelFilterEmpByOrgModule,
    FlypanelFilterTqEmpByModule,
  ],
  exports:[FlyPanelEditRoasterComponent]
})
export class FlyPanelEditRoasterModule { }

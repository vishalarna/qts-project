import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FlypanelReleaseTaskQualByEmpComponent } from './flypanel-release-task-qual-by-emp.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { MatIconModule } from '@angular/material/icon';
import { MatLegacyInputModule as MatInputModule } from '@angular/material/legacy-input';
import { MatLegacyMenuModule as MatMenuModule } from '@angular/material/legacy-menu';
import { MatLegacySelectModule as MatSelectModule } from '@angular/material/legacy-select';
import { MatStepperModule } from '@angular/material/stepper';
import { MatLegacyTableModule as MatTableModule } from '@angular/material/legacy-table';
import { MatLegacyTooltipModule as MatTooltipModule } from '@angular/material/legacy-tooltip';
import { MatTreeModule } from '@angular/material/tree';
import { BaseModule } from 'src/app/components/base/base.module';
import { DragDropModule } from '@angular/cdk/drag-drop';
import { FlypanelFilterTqEmpByModule } from '../flypanel-filter-tq-emp-by/flypanel-filter-tq-emp-by.module';
import { FlypanelFilterEmpByOrgModule } from '../flypanel-filter-emp-by-org/flypanel-filter-emp-by-org.module';
import { MatTabsModule } from '@angular/material/tabs';


@NgModule({
  declarations: [
    FlypanelReleaseTaskQualByEmpComponent
  ],
  imports: [
    CommonModule,
    BaseModule,
    MatTreeModule,
    MatCheckboxModule,
    MatSelectModule,
    MatStepperModule,
    MatMenuModule,
    FormsModule,
    ReactiveFormsModule,
    MatTableModule,
    MatInputModule,
    MatIconModule,
    MatTooltipModule,
    DragDropModule,
    FlypanelFilterTqEmpByModule,
    FlypanelFilterEmpByOrgModule,
    MatTabsModule
  ],
  exports : [
    FlypanelReleaseTaskQualByEmpComponent,
  ]
})
export class FlypanelReleaseTaskQualByEmpModule { }

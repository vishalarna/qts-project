import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FlypanelReleaseTaskQualificationComponent } from './flypanel-release-task-qualification.component';
import { BaseModule } from 'src/app/components/base/base.module';
import { MatTreeModule } from '@angular/material/tree';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { MatLegacySelectModule as MatSelectModule } from '@angular/material/legacy-select';
import { MatStepperModule } from '@angular/material/stepper';
import { MatLegacyMenuModule as MatMenuModule } from '@angular/material/legacy-menu';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatLegacyTableModule as MatTableModule } from '@angular/material/legacy-table';
import { MatLegacyInputModule as MatInputModule } from '@angular/material/legacy-input';
import { MatIconModule } from '@angular/material/icon';
import { MatLegacyTooltipModule as MatTooltipModule } from '@angular/material/legacy-tooltip';
import { FlypanelFilterTqEmpByModule } from '../flypanel-filter-tq-emp-by/flypanel-filter-tq-emp-by.module';
import { FlypanelFilterEmpByOrgModule } from '../flypanel-filter-emp-by-org/flypanel-filter-emp-by-org.module';
import { DragDropModule } from '@angular/cdk/drag-drop';
import { MatLegacyFormFieldModule as MatFormFieldModule } from '@angular/material/legacy-form-field';
import { MatLegacyTabsModule as MatTabsModule } from '@angular/material/legacy-tabs';



@NgModule({
  declarations: [
    FlypanelReleaseTaskQualificationComponent
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
    FlypanelFilterTqEmpByModule,
    FlypanelFilterEmpByOrgModule,
    DragDropModule,
    MatFormFieldModule,
    MatTabsModule
  ],
  exports : [
    FlypanelReleaseTaskQualificationComponent,
  ]
})
export class FlypanelReleaseTaskQualificationModule { }

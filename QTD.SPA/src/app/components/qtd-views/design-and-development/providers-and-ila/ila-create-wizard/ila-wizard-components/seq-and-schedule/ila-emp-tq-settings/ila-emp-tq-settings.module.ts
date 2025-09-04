import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { IlaEmpTqSettingsComponent } from './ila-emp-tq-settings.component';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { MatLegacySelectModule as MatSelectModule } from '@angular/material/legacy-select';
import { BaseModule } from 'src/app/components/base/base.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatLegacyTableModule as MatTableModule } from '@angular/material/legacy-table';
import { DragDropModule } from '@angular/cdk/drag-drop';
import { FlypanelLinkTaskQualificationEvaluatorsModule } from 'src/app/components/qtd-views/implementation/schedulingclasses/scheduling-classes-overview/flypanel-link-task-qualification-evaluators/flypanel-link-task-qualification-evaluators.module';
import { MatIconModule } from '@angular/material/icon';



@NgModule({
  declarations: [
    IlaEmpTqSettingsComponent
  ],
  imports: [
    CommonModule,
    BaseModule,
    MatSelectModule,
    MatCheckboxModule,
    FormsModule,
    MatTableModule,
    DragDropModule,
    ReactiveFormsModule,
    FlypanelLinkTaskQualificationEvaluatorsModule,
    MatIconModule,
  ],
  exports: [
    IlaEmpTqSettingsComponent,
  ]
})
export class IlaEmpTqSettingsModule { }

import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { IlaEmpEvalSettingsComponent } from './ila-emp-eval-settings.component';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { MatLegacySelectModule as MatSelectModule } from '@angular/material/legacy-select';
import { BaseModule } from 'src/app/components/base/base.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { FlyPanelAddTqEvaluaterModule } from "../../../../../../implementation/schedulingclasses/scheduling-classes-overview/fly-panel-add-tq-evaluater/fly-panel-add-tq-evaluater.module";
import { MatLegacyTableModule as MatTableModule } from '@angular/material/legacy-table';
import { FlyPanelClassesListModule } from "../../../../../../implementation/schedulingclasses/scheduling-classes-overview/fly-panel-classes-list/fly-panel-classes-list.module";
import { MatAccordion, MatExpansionModule } from '@angular/material/expansion';



@NgModule({
    declarations: [
        IlaEmpEvalSettingsComponent
    ],
    imports: [
        CommonModule,
        BaseModule,
        MatSelectModule,
        MatCheckboxModule,
        FormsModule,
        ReactiveFormsModule,
        FlyPanelAddTqEvaluaterModule,
        MatTableModule,
        FlyPanelClassesListModule,
    MatExpansionModule,

  ],
  exports: [
    IlaEmpEvalSettingsComponent,
  ]
})
export class IlaEmpEvalSettingsModule { }

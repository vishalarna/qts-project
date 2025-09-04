import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AddNewTrainingComponent } from './add-new-training.component';
import { RouterModule, Routes } from '@angular/router';
import { LayoutModule } from 'src/app/components/qtd-views/layout/layout.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { MatLegacyPaginatorModule as MatPaginatorModule } from '@angular/material/legacy-paginator';
import { MatLegacySelectModule as MatSelectModule } from '@angular/material/legacy-select';
import { MatStepperModule } from '@angular/material/stepper';
import { MatLegacyTableModule as MatTableModule } from '@angular/material/legacy-table';
import { MatToolbarModule } from '@angular/material/toolbar';
import { CKEditorModule } from '@ckeditor/ckeditor5-angular';
import { BaseModule } from 'src/app/components/base/base.module';
import { MatExpansionModule } from '@angular/material/expansion';
import { MatLegacyMenuModule as MatMenuModule } from '@angular/material/legacy-menu';
import { FlyPanelAddRecurrenceEventModule } from '../fly-panel-add-recurrence-event/fly-panel-add-recurrence-event.module';
import { DragDropModule } from '@angular/cdk/drag-drop';
import { MatButtonToggleModule } from '@angular/material/button-toggle';
import { MatLegacyInputModule as MatInputModule } from '@angular/material/legacy-input';
import { MatLegacyRadioModule as MatRadioModule } from '@angular/material/legacy-radio';
import { MatLegacyTabsModule as MatTabsModule } from '@angular/material/legacy-tabs';
import { FlyPanelChangeEnrollmentSettingsModule } from '../fly-panel-change-enrollment-settings/fly-panel-change-enrollment-settings.module';
import { RemoveEmployeesDialogueModule } from '../remove-employees-dialogue/remove-employees-dialogue.module';
import { FlypanelEmployeesWaitlistModule } from '../flypanel-employees-waitlist/flypanel-employees-waitlist.module';
import { FlypanelLinkTaskQualificationEvaluatorsModule } from '../flypanel-link-task-qualification-evaluators/flypanel-link-task-qualification-evaluators.module';
import { FlyPanelEnrollEmployeesModule } from '../fly-panel-enroll-employees/fly-panel-enroll-employees.module';
import { FlyPanelAddTqEvaluaterModule } from '../fly-panel-add-tq-evaluater/fly-panel-add-tq-evaluater.module';
import { FlyPanelClassesListModule } from '../fly-panel-classes-list/fly-panel-classes-list.module';
import { AddEmpRecurrDialogModule } from '../add-emp-recurr-dialog/add-emp-recurr-dialog.module';
import { IlaEmpTestSettingsModule } from 'src/app/components/qtd-views/design-and-development/providers-and-ila/ila-create-wizard/ila-wizard-components/seq-and-schedule/ila-emp-test-settings/ila-emp-test-settings.module';
import { IlaEmpTqSettingsModule } from 'src/app/components/qtd-views/design-and-development/providers-and-ila/ila-create-wizard/ila-wizard-components/seq-and-schedule/ila-emp-tq-settings/ila-emp-tq-settings.module';
import { CbtManagerModule } from 'src/app/components/shared/cbt-manager/cbt-manager.module';
import { MatLegacyTooltipModule as MatTooltipModule } from '@angular/material/legacy-tooltip';
import { MatIconModule } from '@angular/material/icon';

const routes: Routes = [
  {
    path: '',
    component: AddNewTrainingComponent,
  },
  {
    path: ':id',
    component: AddNewTrainingComponent,
  },
  {
    path: ':id/:index',
    component: AddNewTrainingComponent,
  },


];

@NgModule({
  declarations: [
    AddNewTrainingComponent
  ],
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    CommonModule,
    MatTableModule,
    MatPaginatorModule,
    BaseModule,
    LayoutModule,
    MatTableModule,
    FormsModule,
    ReactiveFormsModule,
    CKEditorModule,
    MatStepperModule,
    MatSelectModule,
    MatToolbarModule,
    MatCheckboxModule,
    MatCheckboxModule,
    MatExpansionModule,
    MatMenuModule,
    FlyPanelAddRecurrenceEventModule,
    DragDropModule,
    MatRadioModule,
    MatInputModule,
    MatButtonToggleModule,
    MatTabsModule,
    FlyPanelChangeEnrollmentSettingsModule,
    RemoveEmployeesDialogueModule,
    FlypanelEmployeesWaitlistModule,
    MatInputModule,
    FlypanelLinkTaskQualificationEvaluatorsModule,
    FlyPanelEnrollEmployeesModule,
    FlyPanelAddTqEvaluaterModule,
    FlyPanelClassesListModule,
    AddEmpRecurrDialogModule,
    IlaEmpTestSettingsModule,
    IlaEmpTqSettingsModule,
    CbtManagerModule,
    MatTooltipModule,
    MatIconModule
  ]
})
export class AddNewTrainingModule { }

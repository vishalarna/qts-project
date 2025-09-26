import { BaseModule } from 'src/app/components/base/base.module';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FlyPanelTaskReQualificationCompFeedbackComponent } from './fly-panel-task-re-qualification-comp-feedback.component';
import { RouterModule, Routes } from '@angular/router';
import { LayoutModule } from '../../../layout/layout.module';
import { MatButtonToggleModule } from '@angular/material/button-toggle';
import { MatLegacyChipsModule as MatChipsModule } from '@angular/material/legacy-chips';
import { MatLegacyMenuModule as MatMenuModule } from '@angular/material/legacy-menu';
import { MatLegacySelectModule as MatSelectModule } from '@angular/material/legacy-select';
import { MatLegacyTabsModule as MatTabsModule } from '@angular/material/legacy-tabs';
import { MatToolbarModule } from '@angular/material/toolbar';
import { TaskEmployeeModule } from '../../../my-data/tasks/task-detail/task-employee/task-employee.module';
import { TaskEnablingObjectivesModule } from '../../../my-data/tasks/task-detail/task-enabling-objectives/task-enabling-objectives.module';
import { TaskIlasModule } from '../../../my-data/tasks/task-detail/task-ilas/task-ilas.module';
import { TaskPositionsModule } from '../../../my-data/tasks/task-detail/task-positions/task-positions.module';
import { TaskProceduresModule } from '../../../my-data/tasks/task-detail/task-procedures/task-procedures.module';
import { TaskRegulatoryRequirementsModule } from '../../../my-data/tasks/task-detail/task-regulatory-requirements/task-regulatory-requirements.module';
import { TaskSafetyHazardsModule } from '../../../my-data/tasks/task-detail/task-safety-hazards/task-safety-hazards.module';

const routes: Routes = [
  {
    path: '',
    component: FlyPanelTaskReQualificationCompFeedbackComponent,
  },
  {
    path: ':id',
    component: FlyPanelTaskReQualificationCompFeedbackComponent,
  },
];

@NgModule({
  declarations: [
    FlyPanelTaskReQualificationCompFeedbackComponent
  ],
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    CommonModule,
    BaseModule,
    LayoutModule,
    MatToolbarModule,
    MatSelectModule,
    MatButtonToggleModule,
    MatMenuModule,
    MatTabsModule,
    MatChipsModule,
    TaskEmployeeModule,
    TaskPositionsModule,
    TaskIlasModule,
    TaskRegulatoryRequirementsModule,
    TaskProceduresModule,
    TaskEnablingObjectivesModule,
    TaskSafetyHazardsModule,
  ]
})
export class FlyPanelTaskReQualificationCompFeedbackModule { }

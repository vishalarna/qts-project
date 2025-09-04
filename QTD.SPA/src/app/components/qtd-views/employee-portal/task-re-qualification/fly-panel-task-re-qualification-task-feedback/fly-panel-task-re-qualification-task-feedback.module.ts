import { BaseModule } from 'src/app/components/base/base.module';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FlyPanelTaskReQualificationTaskFeedbackComponent } from './fly-panel-task-re-qualification-task-feedback.component';
import { LayoutModule } from '../../../layout/layout.module';
import { RouterModule, Routes } from '@angular/router';
import { MatLegacySelectModule as MatSelectModule } from '@angular/material/legacy-select';
import { MatButtonToggleModule } from '@angular/material/button-toggle';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatLegacyMenuModule as MatMenuModule } from '@angular/material/legacy-menu';
import { MatLegacyTabsModule as MatTabsModule } from '@angular/material/legacy-tabs';
import { MatLegacyChipsModule as MatChipsModule } from '@angular/material/legacy-chips';
import { TaskEmployeeModule } from '../../../my-data/tasks/task-detail/task-employee/task-employee.module';
import { TaskEnablingObjectivesModule } from '../../../my-data/tasks/task-detail/task-enabling-objectives/task-enabling-objectives.module';
import { TaskIlasModule } from '../../../my-data/tasks/task-detail/task-ilas/task-ilas.module';
import { TaskPositionsModule } from '../../../my-data/tasks/task-detail/task-positions/task-positions.module';
import { TaskProceduresModule } from '../../../my-data/tasks/task-detail/task-procedures/task-procedures.module';
import { TaskRegulatoryRequirementsModule } from '../../../my-data/tasks/task-detail/task-regulatory-requirements/task-regulatory-requirements.module';
import { TaskSafetyHazardsModule } from '../../../my-data/tasks/task-detail/task-safety-hazards/task-safety-hazards.module';
import { TaskOjtGuideModule } from '../../../my-data/tasks/task-detail/task-ojt-guide/task-ojt-guide.module';
import { TaskMetaOjtGuideModule } from '../../../my-data/tasks/task-detail/task-meta-ojt-guide/task-meta-ojt-guide.module';


const routes: Routes = [
  { path: '', component: FlyPanelTaskReQualificationTaskFeedbackComponent },
  { path: ':id', component: FlyPanelTaskReQualificationTaskFeedbackComponent }
];

@NgModule({
  declarations: [
    FlyPanelTaskReQualificationTaskFeedbackComponent
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
    TaskOjtGuideModule,
    TaskMetaOjtGuideModule,
  ],
  exports:[FlyPanelTaskReQualificationTaskFeedbackComponent]
  
})
export class FlyPanelTaskReQualificationTaskFeedbackModule { }

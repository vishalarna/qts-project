import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TaskDetailComponent } from './task-detail.component';
import { RouterModule, Routes } from '@angular/router';
import { BaseModule } from 'src/app/components/base/base.module';
import { MatLegacyTabsModule as MatTabsModule } from '@angular/material/legacy-tabs';
import { MatLegacyMenuModule as MatMenuModule } from '@angular/material/legacy-menu';
import { TaskOjtGuideModule } from './task-ojt-guide/task-ojt-guide.module';
import { TaskEmployeeModule } from './task-employee/task-employee.module';
import { TaskPositionsModule } from './task-positions/task-positions.module';
import { TaskIlasModule } from './task-ilas/task-ilas.module';
import { TaskRegulatoryRequirementsModule } from './task-regulatory-requirements/task-regulatory-requirements.module';
import { TaskProceduresModule } from './task-procedures/task-procedures.module';
import { TaskEnablingObjectivesModule } from './task-enabling-objectives/task-enabling-objectives.module';
import { TaskSafetyHazardsModule } from './task-safety-hazards/task-safety-hazards.module';
import { FlypanelAddTaskModule } from '../flypanel-add-task/flypanel-add-task.module';
import { MatLegacyTableModule as MatTableModule } from '@angular/material/legacy-table';
import { MatSortModule } from '@angular/material/sort';
import { MatLegacyPaginatorModule as MatPaginatorModule } from '@angular/material/legacy-paginator';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { FlyPanelLinkTaskModule } from '../../../design-and-development/providers-and-ila/ila-create-wizard/ila-wizard-components/trainee-evaluation/fly-panel-link-task/fly-panel-link-task.module';
import { TaskCollaborateModalModule } from '../task-collaborate-modal/task-collaborate-modal.module';
import { FlyPanelTaskHistoryModule } from '../fly-panel-task-history/fly-panel-task-history.module';
import { MatLegacyTooltipModule as MatTooltipModule } from '@angular/material/legacy-tooltip';
import { FlyPanelMetaTaskLinkModule } from '../fly-panel-meta-task-link/fly-panel-meta-task-link.module';
import { DragDropModule } from '@angular/cdk/drag-drop';
import { TaskMetaOjtGuideModule } from './task-meta-ojt-guide/task-meta-ojt-guide.module';

const routes: Routes = [
  {
    path: ':id',
    component: TaskDetailComponent,
  },
];

@NgModule({
  declarations: [TaskDetailComponent],
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    BaseModule,
    MatTooltipModule,
    MatTabsModule,
    MatMenuModule,
    MatTableModule,
    MatSortModule,
    MatPaginatorModule,
    MatCheckboxModule,
    TaskOjtGuideModule,
    TaskEmployeeModule,
    TaskPositionsModule,
    TaskIlasModule,
    TaskRegulatoryRequirementsModule,
    TaskProceduresModule,
    TaskEnablingObjectivesModule,
    TaskSafetyHazardsModule,
    FlypanelAddTaskModule,
    FlyPanelMetaTaskLinkModule,
    TaskCollaborateModalModule,
    FlyPanelTaskHistoryModule,
    DragDropModule,
    TaskMetaOjtGuideModule,
  ],
})
export class TaskDetailModule {}

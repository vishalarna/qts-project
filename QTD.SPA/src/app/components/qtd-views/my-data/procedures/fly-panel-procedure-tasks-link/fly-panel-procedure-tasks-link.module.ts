import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FlyPanelProcedureTasksLinkComponent } from './fly-panel-procedure-tasks-link.component';
import { BaseModule } from 'src/app/components/base/base.module';
import { FlypanelAddDutyareaModule } from '../../tasks/flypanel-add-dutyarea/flypanel-add-dutyarea.module';
import { FlypanelAddSubdutyareaModule } from '../../tasks/flypanel-add-subdutyarea/flypanel-add-subdutyarea.module';
import { FlypanelAddTaskModule } from '../../tasks/flypanel-add-task/flypanel-add-task.module';
import { MatLegacyMenuModule as MatMenuModule } from '@angular/material/legacy-menu';
import { FormsModule } from '@angular/forms';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { MatTreeModule } from '@angular/material/tree';

@NgModule({
  declarations: [FlyPanelProcedureTasksLinkComponent],
  imports: [
    CommonModule,
    BaseModule,
    FlypanelAddTaskModule,
    FlypanelAddDutyareaModule,
    FlypanelAddSubdutyareaModule,
    MatMenuModule,
    FormsModule,
    MatCheckboxModule,
    MatTreeModule,
  ],
  exports: [FlyPanelProcedureTasksLinkComponent],
})
export class FlyPanelProcedureTasksLinkModule {}

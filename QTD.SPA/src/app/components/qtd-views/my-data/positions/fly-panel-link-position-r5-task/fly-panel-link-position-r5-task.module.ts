import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FlyPanelLinkPositionR5TaskComponent } from './fly-panel-link-position-r5-task.component';
import { FormsModule } from '@angular/forms';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { MatLegacyMenuModule as MatMenuModule } from '@angular/material/legacy-menu';
import { MatTreeModule } from '@angular/material/tree';
import { BaseModule } from 'src/app/components/base/base.module';
import { FlypanelAddDutyareaModule } from '../../tasks/flypanel-add-dutyarea/flypanel-add-dutyarea.module';
import { FlypanelAddSubdutyareaModule } from '../../tasks/flypanel-add-subdutyarea/flypanel-add-subdutyarea.module';
import { FlypanelAddTaskModule } from '../../tasks/flypanel-add-task/flypanel-add-task.module';
import { MatLegacyTableModule as MatTableModule } from '@angular/material/legacy-table';
import { MatLegacyDialogModule as MatDialogModule } from '@angular/material/legacy-dialog';
import { DialogueUnlinkR5TasksModule } from '../dialogue-unlink-r5-tasks/dialogue-unlink-r5-tasks.module';
import { FlyPanelLinkPositionFilterModule } from '../fly-panel-link-position-filter/fly-panel-link-position-filter.module';



@NgModule({
  declarations: [
    FlyPanelLinkPositionR5TaskComponent
  ],
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
    MatTableModule,
    MatDialogModule,
    DialogueUnlinkR5TasksModule,
    FlyPanelLinkPositionFilterModule
  ],
  exports: [FlyPanelLinkPositionR5TaskComponent],
})
export class FlyPanelLinkPositionR5TaskModule { }

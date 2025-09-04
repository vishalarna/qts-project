import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FlyPanelToolTaskLinkComponent } from './fly-panel-tool-task-link.component';
import { BaseModule } from 'src/app/components/base/base.module';
import { FlypanelAddTaskModule } from '../../tasks/flypanel-add-task/flypanel-add-task.module';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { MatTreeModule } from '@angular/material/tree';
import { FormsModule } from '@angular/forms';


@NgModule({
  declarations: [
    FlyPanelToolTaskLinkComponent
  ],
  imports: [
    CommonModule,
    BaseModule,
    FlypanelAddTaskModule,
    MatCheckboxModule,
    MatTreeModule,
    FormsModule
  ],
  exports:[FlyPanelToolTaskLinkComponent]
})
export class FlyPanelToolTaskLinkModule { }

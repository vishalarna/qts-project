import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FlyPanelEditTaskTrainingGroupComponent } from './fly-panel-edit-task-training-group.component';
import { BaseModule } from 'src/app/components/base/base.module';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { MatLegacyTableModule as MatTableModule } from '@angular/material/legacy-table';
import { FlyPanelTaskTrainingGroupLinkModule } from '../fly-panel-task-training-group-link/fly-panel-task-training-group-link.module';



@NgModule({
  declarations: [
    FlyPanelEditTaskTrainingGroupComponent
  ],
  imports: [
    CommonModule,
    BaseModule,
    MatCheckboxModule,
    MatTableModule,
    FlyPanelTaskTrainingGroupLinkModule,
  ],
  exports: [
    FlyPanelEditTaskTrainingGroupComponent
  ]
})
export class FlyPanelEditTaskTrainingGroupModule { }

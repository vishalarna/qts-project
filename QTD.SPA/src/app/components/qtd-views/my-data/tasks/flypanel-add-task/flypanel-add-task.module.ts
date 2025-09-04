import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FlypanelAddTaskComponent } from './flypanel-add-task.component';
import { BaseModule } from 'src/app/components/base/base.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatStepperModule } from '@angular/material/stepper';
import { FlypanelAddDutyareaModule } from '../flypanel-add-dutyarea/flypanel-add-dutyarea.module';
import { FlypanelAddSubdutyareaModule } from '../flypanel-add-subdutyarea/flypanel-add-subdutyarea.module';
import { MatLegacySelectModule as MatSelectModule } from '@angular/material/legacy-select';
import { MatLegacyChipsModule as MatChipsModule } from '@angular/material/legacy-chips';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { FlyPanelAddPositionModule } from '../../../my-data/positions/fly-panel-add-position/fly-panel-add-position.module';

@NgModule({
  declarations: [FlypanelAddTaskComponent],
  imports: [
    CommonModule,
    BaseModule,
    FormsModule,
    ReactiveFormsModule,
    MatStepperModule,
    FlypanelAddDutyareaModule,
    FlypanelAddSubdutyareaModule,
    MatSelectModule,
    MatChipsModule,
    MatCheckboxModule,
    FlyPanelAddPositionModule
  ],
  exports: [FlypanelAddTaskComponent],
})
export class FlypanelAddTaskModule {}

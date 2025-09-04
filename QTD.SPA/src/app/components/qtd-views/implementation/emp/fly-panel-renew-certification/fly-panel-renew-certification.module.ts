import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FlyPanelRenewCertificationComponent } from './fly-panel-renew-certification.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { MatLegacyChipsModule as MatChipsModule } from '@angular/material/legacy-chips';
import { MatLegacySelectModule as MatSelectModule } from '@angular/material/legacy-select';
import { MatStepperModule } from '@angular/material/stepper';
import { BaseModule } from 'src/app/components/base/base.module';
import { FlypanelAddDutyareaModule } from '../../../my-data/tasks/flypanel-add-dutyarea/flypanel-add-dutyarea.module';
import { FlypanelAddSubdutyareaModule } from '../../../my-data/tasks/flypanel-add-subdutyarea/flypanel-add-subdutyarea.module';



@NgModule({
  declarations: [
    FlyPanelRenewCertificationComponent
  ],
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
  ],
  exports:[FlyPanelRenewCertificationComponent]
})
export class FlyPanelRenewCertificationModule { }

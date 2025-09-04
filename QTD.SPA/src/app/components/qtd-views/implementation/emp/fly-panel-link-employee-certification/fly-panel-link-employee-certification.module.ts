import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FlyPanelLinkEmployeeCertificationComponent } from './fly-panel-link-employee-certification.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { MatLegacyFormFieldModule as MatFormFieldModule } from '@angular/material/legacy-form-field';
import { MatLegacySelectModule as MatSelectModule } from '@angular/material/legacy-select';
import { MatStepperModule } from '@angular/material/stepper';
import { BaseModule } from 'src/app/components/base/base.module';
import { FlyPanelAddCertificationModule } from '../../../my-data/certifications/fly-panel-add-certification/fly-panel-add-certification.module';



@NgModule({
  declarations: [
    FlyPanelLinkEmployeeCertificationComponent
  ],
  imports: [
    CommonModule,
    BaseModule,
    FormsModule,
    ReactiveFormsModule,
    MatCheckboxModule,
    MatFormFieldModule,
    MatSelectModule,
    MatStepperModule,
    FlyPanelAddCertificationModule
  ],
  exports:[FlyPanelLinkEmployeeCertificationComponent]
})
export class FlyPanelLinkEmployeeCertificationModule { }

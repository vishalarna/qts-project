import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { MatLegacyFormFieldModule as MatFormFieldModule } from '@angular/material/legacy-form-field';
import { MatLegacySelectModule as MatSelectModule } from '@angular/material/legacy-select';
import { MatStepperModule } from '@angular/material/stepper';
import { BaseModule } from 'src/app/components/base/base.module';
import { FlyPanelIssuingAuthorityModule } from '../../procedures/fly-panel-add-issuing-authority/fly-panel-add-issuing-authority.module';
import { FlyPanelAddCertificationComponent } from './fly-panel-add-certification.component';
import { MatDividerModule } from '@angular/material/divider';
import {FlyPanelAddIssuingAuthorityModule} from '../fly-panel-add-issuingauthority/fly-panel-add-issuingauthority.module'
import {MatLegacyTooltipModule as MatTooltipModule} from '@angular/material/legacy-tooltip';


@NgModule({
  declarations: [
    FlyPanelAddCertificationComponent
  ],
  imports: [
    CommonModule,
    BaseModule,
    FormsModule,
    ReactiveFormsModule,
    MatCheckboxModule,
    MatFormFieldModule,
    MatDividerModule,
    MatSelectModule,
    FlyPanelIssuingAuthorityModule,
    MatStepperModule,
    FlyPanelAddIssuingAuthorityModule,
    MatTooltipModule
  ],
  exports: [FlyPanelAddCertificationComponent]
})
export class FlyPanelAddCertificationModule { }
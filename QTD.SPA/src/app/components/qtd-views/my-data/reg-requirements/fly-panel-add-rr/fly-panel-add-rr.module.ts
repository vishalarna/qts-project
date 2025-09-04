import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FlyPanelAddRRComponent } from './fly-panel-add-rr.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { MatLegacyChipsModule as MatChipsModule } from '@angular/material/legacy-chips';
import { MatLegacySelectModule as MatSelectModule } from '@angular/material/legacy-select';
import { MatStepperModule } from '@angular/material/stepper';
import { BaseModule } from 'src/app/components/base/base.module';
import { FlyPanelAddRrIssuingAuthorityModule } from '../fly-panel-add-rr-issuing-authority/fly-panel-add-rr-issuing-authority.module';

@NgModule({
  declarations: [FlyPanelAddRRComponent],
  imports: [
    CommonModule,
    BaseModule,
    FormsModule,
    ReactiveFormsModule,
    MatStepperModule,
    MatSelectModule,
    MatChipsModule,
    MatCheckboxModule,
    FlyPanelAddRrIssuingAuthorityModule,
  ],
  exports: [FlyPanelAddRRComponent],
})
export class FlyPanelAddRrModule {}

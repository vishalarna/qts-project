import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LayoutModule } from '../../../layout/layout.module';
import { BaseModule } from 'src/app/components/base/base.module';
import { FlyPanelAddProcedureComponent } from './fly-panel-add-procedure.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { FlyPanelIssuingAuthorityModule } from '../fly-panel-add-issuing-authority/fly-panel-add-issuing-authority.module';
import { MatLegacyFormFieldModule as MatFormFieldModule } from '@angular/material/legacy-form-field';
import { MatLegacySelectModule as MatSelectModule } from '@angular/material/legacy-select';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { MatStepperModule } from '@angular/material/stepper';

@NgModule({
  declarations: [FlyPanelAddProcedureComponent],
  imports: [
    CommonModule,
    BaseModule,
    FormsModule,
    ReactiveFormsModule,
    MatFormFieldModule,
    MatSelectModule,
    MatCheckboxModule,
    FlyPanelIssuingAuthorityModule,
    MatStepperModule
  ],
  exports: [FlyPanelAddProcedureComponent],
})
export class FlyPanelAddProcedureModule {}

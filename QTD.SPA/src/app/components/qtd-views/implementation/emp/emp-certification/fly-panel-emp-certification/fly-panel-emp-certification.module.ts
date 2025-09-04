import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FlyPanelEmpCertificationComponent } from './fly-panel-emp-certification.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { FlyPanelCertifyingBodyModule } from '../../../certifying-body/fly-panel-certifying-body/fly-panel-certifying-body.module';
import { BaseModule } from 'src/app/components/base/base.module';
import { MatLegacyFormFieldModule as MatFormFieldModule } from '@angular/material/legacy-form-field';
import { MatLegacySelectModule as MatSelectModule } from '@angular/material/legacy-select';

@NgModule({
  declarations: [FlyPanelEmpCertificationComponent],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    FlyPanelCertifyingBodyModule,
    MatFormFieldModule,
    MatSelectModule,
    BaseModule,
  ],
  exports: [FlyPanelEmpCertificationComponent],
})
export class FlyPanelEmpCertificationModule {}

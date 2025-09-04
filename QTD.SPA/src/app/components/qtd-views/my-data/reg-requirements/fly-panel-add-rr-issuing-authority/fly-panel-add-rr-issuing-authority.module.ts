import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FlyPanelAddRRIssuingAuthorityComponent } from './fly-panel-add-rr-issuing-authority.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { BaseModule } from 'src/app/components/base/base.module';

@NgModule({
  declarations: [FlyPanelAddRRIssuingAuthorityComponent],
  imports: [
    CommonModule,
    BaseModule,
    FormsModule,
    ReactiveFormsModule,
    MatCheckboxModule,
  ],
  exports: [FlyPanelAddRRIssuingAuthorityComponent],
})
export class FlyPanelAddRrIssuingAuthorityModule {}

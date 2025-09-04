import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BaseModule } from 'src/app/components/base/base.module';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { FlyPanelAddIssuingAuthorityComponent } from './fly-panel-add-issuingauthority.component';

@NgModule({
  declarations: [
    FlyPanelAddIssuingAuthorityComponent
  ],
  imports: [
    CommonModule,
    BaseModule,
    FormsModule,
    ReactiveFormsModule,
    MatCheckboxModule
  ],
  exports: [FlyPanelAddIssuingAuthorityComponent]
})
export class FlyPanelAddIssuingAuthorityModule  { }

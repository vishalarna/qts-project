import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FlyPanelAddIssuingAuthorityComponent } from './fly-panel-add-issuing-authority.component';
import { BaseModule } from 'src/app/components/base/base.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';



@NgModule({
  declarations: [FlyPanelAddIssuingAuthorityComponent],
  imports: [
    CommonModule,
    BaseModule,
    FormsModule,
    ReactiveFormsModule,
    MatCheckboxModule
  ],
  exports: [FlyPanelAddIssuingAuthorityComponent]
})
export class FlyPanelIssuingAuthorityModule { }

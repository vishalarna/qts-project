import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FlyPanelFilterIdpComponent } from './fly-panel-filter-idp.component';
import { BaseModule } from 'src/app/components/base/base.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatLegacySelectModule as MatSelectModule } from '@angular/material/legacy-select';


@NgModule({
  declarations: [
    FlyPanelFilterIdpComponent
  ],
  imports: [
    CommonModule,
    BaseModule,
    FormsModule,
    MatSelectModule,
    ReactiveFormsModule
  ],
  exports:[FlyPanelFilterIdpComponent]
})
export class FlyPanelFilterIdpModule { }

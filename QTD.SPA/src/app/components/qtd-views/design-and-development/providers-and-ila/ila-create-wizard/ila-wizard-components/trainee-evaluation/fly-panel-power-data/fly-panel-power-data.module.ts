import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FlyPanelPowerDataComponent } from './fly-panel-power-data.component';
import { BaseModule } from 'src/app/components/base/base.module';
import { LocalizeModule } from 'src/app/_Shared/modules/localize.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';


@NgModule({
  declarations: [
    FlyPanelPowerDataComponent
  ],
  imports: [
    CommonModule,
    LocalizeModule,
    BaseModule,
    FormsModule,
    ReactiveFormsModule,
    MatCheckboxModule
  ],
  exports:[FlyPanelPowerDataComponent]
})
export class FlyPanelPowerDataModule { }

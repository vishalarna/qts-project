import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FlyPanelCoversheetsComponent } from './fly-panel-coversheets.component';
import { LayoutModule } from '@angular/cdk/layout';
import { BaseModule } from 'src/app/components/base/base.module';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { ReactiveFormsModule } from '@angular/forms';
import { MatLegacyRadioModule as MatRadioModule } from '@angular/material/legacy-radio';



@NgModule({
  declarations: [FlyPanelCoversheetsComponent],
  imports: [
    CommonModule,
    BaseModule,
    LayoutModule,
    MatCheckboxModule,
    ReactiveFormsModule,
    MatRadioModule,
  ],
  exports: [FlyPanelCoversheetsComponent],
})
export class FlyPanelCoversheetsModule { }

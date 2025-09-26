import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FlyPanelCertificationHistoryComponent } from './fly-panel-certification-history.component';
import { BaseModule } from 'src/app/components/base/base.module';
import { ReactiveFormsModule } from '@angular/forms';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';

@NgModule({
  declarations: [FlyPanelCertificationHistoryComponent],
  imports: [
    CommonModule,
    BaseModule,
    ReactiveFormsModule,
    MatCheckboxModule,
  ],
    exports:[FlyPanelCertificationHistoryComponent]
})
export class FlyPanelCertificationHistoryModule { }

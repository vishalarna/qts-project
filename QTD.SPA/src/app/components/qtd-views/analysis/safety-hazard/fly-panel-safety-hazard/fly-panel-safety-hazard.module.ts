import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { MatLegacyMenuModule as MatMenuModule } from '@angular/material/legacy-menu';
import { MatLegacyProgressSpinnerModule as MatProgressSpinnerModule } from '@angular/material/legacy-progress-spinner';
import { BaseModule } from 'src/app/components/base/base.module';
import { FlyPanelSafetyHazardComponent } from './fly-panel-safety-hazard.component';
import { MatExpansionModule } from '@angular/material/expansion';

@NgModule({
  declarations: [FlyPanelSafetyHazardComponent],
  imports: [
    CommonModule,

    BaseModule,
    FormsModule,
    MatProgressSpinnerModule,
    MatMenuModule,
    MatExpansionModule,
  ],
  exports: [FlyPanelSafetyHazardComponent],
})
export class FlyPanelSafetyHazardModule {}

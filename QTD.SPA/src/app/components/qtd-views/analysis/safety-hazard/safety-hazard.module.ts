import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FlyPanelSafetyHazardComponent } from './fly-panel-safety-hazard/fly-panel-safety-hazard.component';
import { SafetyHazardComponent } from './safety-hazard.component';
import { BaseModule } from 'src/app/components/base/base.module';
import { FormsModule } from '@angular/forms';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatMenuModule } from '@angular/material/menu';

@NgModule({
  declarations: [SafetyHazardComponent],
  imports: [CommonModule],
})
export class SafetyHazardModule {}

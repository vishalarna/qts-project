import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FlyPanelEnablingObjectiveComponent } from './fly-panel-enabling-objective.component';
import { FormsModule } from '@angular/forms';
import { MatExpansionModule } from '@angular/material/expansion';
import { MatLegacyMenuModule as MatMenuModule } from '@angular/material/legacy-menu';
import { BaseModule } from 'src/app/components/base/base.module';
import { MatLegacyProgressSpinnerModule as MatProgressSpinnerModule } from '@angular/material/legacy-progress-spinner';

@NgModule({
  declarations: [FlyPanelEnablingObjectiveComponent],
  imports: [
    CommonModule,
    FormsModule,
    MatExpansionModule,
    MatMenuModule,
    BaseModule,
    MatProgressSpinnerModule,
  ],
  exports: [FlyPanelEnablingObjectiveComponent],
})
export class FlyPanelEnablingObjectiveModule {}

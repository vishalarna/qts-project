import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FlyPanelEditScoreComponent } from './fly-panel-edit-score.component';
import { LayoutModule } from '@angular/cdk/layout';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatLegacySelectModule as MatSelectModule } from '@angular/material/legacy-select';
import { BaseModule } from 'src/app/components/base/base.module';



@NgModule({
  declarations: [FlyPanelEditScoreComponent],
  imports: [
    FormsModule,
    ReactiveFormsModule,
    CommonModule,
    BaseModule,
    LayoutModule,
    MatSelectModule,
  ],
  exports:[FlyPanelEditScoreComponent]
})
export class FlyPanelEditScoreModule { }

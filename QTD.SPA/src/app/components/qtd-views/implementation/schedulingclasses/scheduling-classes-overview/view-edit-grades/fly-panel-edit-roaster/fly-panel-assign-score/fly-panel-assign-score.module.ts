import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FlyPanelAssignScoreComponent } from './fly-panel-assign-score.component';
import { LayoutModule } from '@angular/cdk/layout';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatLegacySelectModule as MatSelectModule } from '@angular/material/legacy-select';
import { BaseModule } from 'src/app/components/base/base.module';



@NgModule({
  declarations: [FlyPanelAssignScoreComponent],
  imports: [
    FormsModule,
    ReactiveFormsModule,
    CommonModule,
    BaseModule,
    LayoutModule,
    MatSelectModule,
  ]
  ,exports:[FlyPanelAssignScoreComponent]
})
export class FlyPanelAssignScoreModule { }

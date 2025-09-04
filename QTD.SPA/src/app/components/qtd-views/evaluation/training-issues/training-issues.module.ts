import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TrainingIssuesRoutingModule } from './training-issues-routing.module';
import { TrainingIssuesComponent } from './training-issues.component';

@NgModule({
  declarations: [ TrainingIssuesComponent],
  imports: [
    CommonModule,
    TrainingIssuesRoutingModule
  ],
  exports:[TrainingIssuesComponent]
})
export class TrainingIssuesModule { }

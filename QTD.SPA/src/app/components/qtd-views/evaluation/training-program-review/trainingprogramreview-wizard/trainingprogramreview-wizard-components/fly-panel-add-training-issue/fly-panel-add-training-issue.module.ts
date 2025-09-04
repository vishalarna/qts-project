import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FlyPanelAddTrainingIssueComponent } from './fly-panel-add-training-issue.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BaseModule } from 'src/app/components/base/base.module';
import { MatLegacySelectModule as MatSelectModule } from '@angular/material/legacy-select';
import { MatLegacyInputModule as MatInputModule } from '@angular/material/legacy-input';


@NgModule({
  declarations: [
    FlyPanelAddTrainingIssueComponent
  ],
  imports: [
    CommonModule,
    BaseModule,
    FormsModule,
    ReactiveFormsModule,
    MatSelectModule,
    MatInputModule
  ],
  exports:[FlyPanelAddTrainingIssueComponent]
})
export class FlyPanelAddTrainingIssueModule { }

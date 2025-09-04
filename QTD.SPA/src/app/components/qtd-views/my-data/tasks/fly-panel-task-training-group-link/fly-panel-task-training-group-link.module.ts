import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FlyPanelTaskTrainingGroupLinkComponent } from './fly-panel-task-training-group-link.component';
import { BaseModule } from 'src/app/components/base/base.module';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { MatTreeModule } from '@angular/material/tree';
import { MatLegacyMenuModule as MatMenuModule } from '@angular/material/legacy-menu';
import { FormsModule } from '@angular/forms';



@NgModule({
  declarations: [
    FlyPanelTaskTrainingGroupLinkComponent
  ],
  imports: [
    CommonModule,
    BaseModule,
    MatCheckboxModule,
    MatTreeModule,
    MatMenuModule,
    FormsModule,
  ],
  exports : [
    FlyPanelTaskTrainingGroupLinkComponent
  ]
})
export class FlyPanelTaskTrainingGroupLinkModule { }

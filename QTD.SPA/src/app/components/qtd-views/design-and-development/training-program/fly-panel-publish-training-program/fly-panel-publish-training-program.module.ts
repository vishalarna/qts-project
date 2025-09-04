import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FlyPanelPublishTrainingProgramComponent } from './fly-panel-publish-training-program.component';
import { MatLegacyDialog as MatDialog, MatLegacyDialogModule as MatDialogModule } from '@angular/material/legacy-dialog';
import { BaseModule } from 'src/app/components/base/base.module';
import { FormsModule } from '@angular/forms';



@NgModule({
  declarations: [
    FlyPanelPublishTrainingProgramComponent
  ],
  imports: [
    CommonModule,
    MatDialogModule,
    BaseModule,
    FormsModule,

  ],
  exports:[FlyPanelPublishTrainingProgramComponent]
})
export class FlyPanelPublishTrainingProgramModule { }

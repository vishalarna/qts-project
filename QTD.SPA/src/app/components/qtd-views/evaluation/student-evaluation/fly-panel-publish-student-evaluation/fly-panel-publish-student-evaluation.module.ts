import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FlyPanelPublishStudentEvaluationComponent } from './fly-panel-publish-student-evaluation.component';
import { FormsModule } from '@angular/forms';
import { MatLegacyDialogModule as MatDialogModule } from '@angular/material/legacy-dialog';
import { BaseModule } from 'src/app/components/base/base.module';



@NgModule({
  declarations: [
    FlyPanelPublishStudentEvaluationComponent
  ],
  imports: [
    CommonModule,
    MatDialogModule,
    BaseModule,
    FormsModule,

  ],
  exports:[FlyPanelPublishStudentEvaluationComponent]

})
export class FlyPanelPublishStudentEvaluationModule { }

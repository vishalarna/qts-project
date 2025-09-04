import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { InProgressComponent } from './in-progress.component';
import { BaseModule } from 'src/app/components/base/base.module';
import { FlypanelViewCourseInfoModule } from '../flypanel-view-course-info/flypanel-view-course-info.module';
import { MatExpansionModule } from '@angular/material/expansion';
import { DisclaimerDialogModule } from '../../evaluation/disclaimer-dialog/disclaimer-dialog.module';
import { MatLegacyTooltipModule as MatTooltipModule } from '@angular/material/legacy-tooltip';



@NgModule({
  declarations: [
    InProgressComponent
  ],
  imports: [
    CommonModule,
    BaseModule,
    FlypanelViewCourseInfoModule,
    MatExpansionModule,
    DisclaimerDialogModule,
    MatTooltipModule,
  ],
  exports:[InProgressComponent]
})
export class InProgressModule { }

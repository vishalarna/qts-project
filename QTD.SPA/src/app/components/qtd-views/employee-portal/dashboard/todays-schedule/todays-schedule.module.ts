import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TodaysScheduleComponent } from './todays-schedule.component';
import { BaseModule } from 'src/app/components/base/base.module';
import { FlypanelViewCourseInfoModule } from '../flypanel-view-course-info/flypanel-view-course-info.module';
import { DisclaimerDialogModule } from '../../evaluation/disclaimer-dialog/disclaimer-dialog.module';
import { MatExpansionModule } from '@angular/material/expansion';
import { MatLegacyTooltipModule as MatTooltipModule } from '@angular/material/legacy-tooltip';



@NgModule({
  declarations: [
    TodaysScheduleComponent
  ],
  imports: [
    CommonModule,
    BaseModule,
    FlypanelViewCourseInfoModule,
    DisclaimerDialogModule,
    MatExpansionModule,
    MatTooltipModule,
  ],
  exports:[TodaysScheduleComponent]
})
export class TodaysScheduleModule { }

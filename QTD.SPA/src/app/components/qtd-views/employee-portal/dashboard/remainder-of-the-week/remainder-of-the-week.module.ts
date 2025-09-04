import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RemainderOfTheWeekComponent } from './remainder-of-the-week.component';
import { BaseModule } from 'src/app/components/base/base.module';
import { MatExpansionModule } from '@angular/material/expansion';
import { DisclaimerDialogModule } from '../../evaluation/disclaimer-dialog/disclaimer-dialog.module';
import { FlypanelViewCourseInfoModule } from '../flypanel-view-course-info/flypanel-view-course-info.module';
import { MatLegacyTooltipModule as MatTooltipModule } from '@angular/material/legacy-tooltip';



@NgModule({
  declarations: [
    RemainderOfTheWeekComponent
  ],
  imports: [
    CommonModule,
    BaseModule,
    MatExpansionModule,
    DisclaimerDialogModule,
    FlypanelViewCourseInfoModule,
    MatTooltipModule,
  ],
  exports: [RemainderOfTheWeekComponent]
})
export class RemainderOfTheWeekModule { }

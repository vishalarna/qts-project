import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FinalRemainderComponent } from './final-remainder.component';
import { MatExpansionModule } from '@angular/material/expansion';
import { BaseModule } from 'src/app/components/base/base.module';
import { MatIconModule } from '@angular/material/icon';
import { DisclaimerDialogModule } from '../../evaluation/disclaimer-dialog/disclaimer-dialog.module';
import { FlypanelViewCourseInfoModule } from '../flypanel-view-course-info/flypanel-view-course-info.module';
import { MatLegacyTooltipModule as MatTooltipModule } from '@angular/material/legacy-tooltip';

@NgModule({
  declarations: [
    FinalRemainderComponent
  ],
  imports: [
    CommonModule,
    MatExpansionModule,
    BaseModule,
    MatIconModule,
    DisclaimerDialogModule,
    FlypanelViewCourseInfoModule,
    MatTooltipModule,
  ],
  exports: [FinalRemainderComponent]
})
export class FinalRemainderModule { }

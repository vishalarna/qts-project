import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DashboardComponent } from './dashboard.component';
import { RouterModule, Routes } from '@angular/router';
import { LayoutModule } from '../../layout/layout.module';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatLegacyTabsModule as MatTabsModule } from '@angular/material/legacy-tabs';
import { FinalRemainderModule } from './final-remainder/final-remainder.module';
import { InProgressModule } from './in-progress/in-progress.module';
import { RemainderOfTheWeekModule } from './remainder-of-the-week/remainder-of-the-week.module';
import { TodaysScheduleModule } from './todays-schedule/todays-schedule.module';
import { CalendarModule, DateAdapter } from 'angular-calendar';
import { adapterFactory } from 'angular-calendar/date-adapters/date-fns';
import { BaseModule } from 'src/app/components/base/base.module';
import { MatLegacyTooltipModule as MatTooltipModule } from '@angular/material/legacy-tooltip';
import { DisclaimerDialogModule } from '../evaluation/disclaimer-dialog/disclaimer-dialog.module';
import { MatLegacyMenuModule as MatMenuModule } from '@angular/material/legacy-menu';
import { MatLegacySelectModule as MatSelectModule } from '@angular/material/legacy-select';

const routes: Routes = [
  {
    path:'',
    component:DashboardComponent,
  },
]


@NgModule({
  declarations: [
    DashboardComponent,
  ],
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    LayoutModule,
    MatSidenavModule,
    MatTabsModule,
    MatSelectModule,
    FinalRemainderModule,
    InProgressModule,
    RemainderOfTheWeekModule,
    TodaysScheduleModule,
    DisclaimerDialogModule,
    CalendarModule.forRoot({
      provide: DateAdapter,
      useFactory: adapterFactory,
    }),
    BaseModule,
    MatTooltipModule,
    MatMenuModule,
  ],
  exports:[DashboardComponent]
})
export class DashboardModule { }

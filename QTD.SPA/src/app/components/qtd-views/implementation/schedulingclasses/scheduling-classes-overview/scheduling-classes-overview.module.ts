import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SchedulingClassesOverviewComponent } from './scheduling-classes-overview.component';
import { RouterModule, Routes } from '@angular/router';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { MatLegacyMenuModule as MatMenuModule } from '@angular/material/legacy-menu';
import { MatLegacyPaginatorModule as MatPaginatorModule } from '@angular/material/legacy-paginator';
import { MatSortModule } from '@angular/material/sort';
import { MatLegacyTableModule as MatTableModule } from '@angular/material/legacy-table';
import { BaseModule } from 'src/app/components/base/base.module';
import { LayoutModule } from '../../../layout/layout.module';
import { MatLegacySelectModule as MatSelectModule } from '@angular/material/legacy-select';
import { CalendarModule, DateAdapter } from 'angular-calendar';
import { adapterFactory } from 'angular-calendar/date-adapters/date-fns';
import { MatButtonToggleModule } from '@angular/material/button-toggle';
import { MatLegacyDialogModule as MatDialogModule } from '@angular/material/legacy-dialog';
import { FlyPanelFilterLocationModule } from './fly-panel-filter-location/fly-panel-filter-location.module';
import { FlyPanelFilterTopicModule } from './fly-panel-filter-topic/fly-panel-filter-topic.module';
import { FlyPanelFilterInstructorModule } from './fly-panel-filter-instructor/fly-panel-filter-instructor.module';
import { FlypanelSelectIlaModule } from '../../../design-and-development/tests/test-question-bank/flypanel-select-ila/flypanel-select-ila.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatLegacyTooltipModule as MatTooltipModule } from '@angular/material/legacy-tooltip';
import { MatLegacyInputModule as MatInputModule } from '@angular/material/legacy-input';
import { MatIconModule } from '@angular/material/icon';
const routes: Routes = [
  {
    path: '',
    component: SchedulingClassesOverviewComponent,

  },
];


@NgModule({
  declarations: [
    SchedulingClassesOverviewComponent,
  ],
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    MatTableModule,
    MatPaginatorModule,
    MatSortModule,
    BaseModule,
    LayoutModule,
    MatMenuModule,
    MatCheckboxModule,
    MatSelectModule,
    MatButtonToggleModule,
    MatDialogModule,
    CalendarModule.forRoot({
      provide: DateAdapter,
      useFactory: adapterFactory,
    }),
    FlypanelSelectIlaModule,
    FlyPanelFilterLocationModule,
    FlyPanelFilterTopicModule,
    FlyPanelFilterInstructorModule,
    FormsModule,
    ReactiveFormsModule,
    MatTooltipModule,
    MatInputModule,
    MatIconModule
  ]
})
export class SchedulingClassesOverviewModule { }

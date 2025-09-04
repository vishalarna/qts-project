import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { InstructorsOverviewComponent } from './instructors-overview.component';
import { RouterModule, Routes } from '@angular/router';
import { MatLegacyTableModule as MatTableModule } from '@angular/material/legacy-table';
import { MatLegacyPaginatorModule as MatPaginatorModule } from '@angular/material/legacy-paginator';
import { MatSortModule } from '@angular/material/sort';
import { BaseModule } from 'src/app/components/base/base.module';
import { FlyPanelViewInstructorHistoryModule } from '../fly-panel-view-instructor-history/fly-panel-view-instructor-history.module';
import { FlyPanelInstructorsListModule } from '../fly-panel-instructors-list/fly-panel-instructors-list.module';
const routes: Routes = [
  {
    path: '',
    component: InstructorsOverviewComponent,
  }
 ]



@NgModule({
  declarations: [
    InstructorsOverviewComponent
  ],
  imports: [
    CommonModule,
    MatTableModule,
    MatPaginatorModule,
    MatSortModule,
    BaseModule,
    MatTableModule,
    RouterModule.forChild(routes),
    FlyPanelViewInstructorHistoryModule,
    FlyPanelInstructorsListModule

  ],
  exports: [InstructorsOverviewComponent],
})
export class InstructorsOverviewModule { }

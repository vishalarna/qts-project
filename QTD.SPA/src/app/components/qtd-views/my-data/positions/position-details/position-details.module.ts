import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PositionDetailsComponent } from './position-details.component';
import { RouterModule, Routes } from '@angular/router';
import { BaseModule } from 'src/app/components/base/base.module';
import { MatLegacyMenuModule as MatMenuModule } from '@angular/material/legacy-menu';
import { MatLegacyTabsModule as MatTabsModule } from '@angular/material/legacy-tabs';
import { FlyPanelAddPositionModule } from '../fly-panel-add-position/fly-panel-add-position.module';
import { PositionTasksModule } from './position-tasks/position-tasks.module';
import { PositionSkasModule } from './position-skas/position-skas.module';
import { PositionEmployeesModule } from './position-employees/position-employees.module';
import { MatLegacyTooltipModule as MatTooltipModule } from '@angular/material/legacy-tooltip';
import { TaskSortPipePipe } from 'src/app/_Pipes/task-sort-pipe.pipe';

const routes: Routes = [
  {
    path: ':id',
    component: PositionDetailsComponent,
  }
 ]

@NgModule({
  declarations: [
    PositionDetailsComponent,TaskSortPipePipe
  ],
  imports: [
    CommonModule,
    BaseModule,
    MatMenuModule,
    MatTabsModule,
    FlyPanelAddPositionModule,
    PositionTasksModule,
    PositionSkasModule,
    PositionEmployeesModule,
    RouterModule.forChild(routes),
    MatTooltipModule
  ]
})
export class PositionDetailsModule { }

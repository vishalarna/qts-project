import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BaseModule } from 'src/app/components/base/base.module';
import { LayoutModule } from '../../../layout/layout.module';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { SimulatorScenariosOverviewComponent } from './simulator-scenarios-overview.component';
import { RouterModule, Routes } from '@angular/router';
import { MatLegacyTableModule as MatTableModule } from '@angular/material/legacy-table';
import { MatLegacyPaginatorModule as MatPaginatorModule } from '@angular/material/legacy-paginator';
import { MatSortModule } from '@angular/material/sort';
import { MatLegacyMenuModule as MatMenuModule } from '@angular/material/legacy-menu';
import { FlyPanelFilterSimulatorScenariosModule } from '../fly-panel-filter-simulator-scenarios/fly-panel-filter-simulator-scenarios.module';
import { MatLegacyTooltipModule as MatTooltipModule } from '@angular/material/legacy-tooltip';

const routes: Routes = [
  {
    path: '',
    component: SimulatorScenariosOverviewComponent,
  },
];

@NgModule({
  declarations: [SimulatorScenariosOverviewComponent],
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    FormsModule,
    BaseModule,
    ReactiveFormsModule,
    LayoutModule,
    MatCheckboxModule,
    MatTableModule,
    MatPaginatorModule,
    MatSortModule,
    MatMenuModule,
    FlyPanelFilterSimulatorScenariosModule,
    MatTooltipModule
  ],
})
export class SimulatorScenariosOverviewModule {}

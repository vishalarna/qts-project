import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BaseModule } from 'src/app/components/base/base.module';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { RouterModule, Routes } from '@angular/router';
import { MatLegacySelectModule as MatSelectModule } from '@angular/material/legacy-select';
import { SimScenariosWizardCriteriaComponent } from './sim-scenarios-wizard-criteria.component';
import { MatLegacyTableModule as MatTableModule } from '@angular/material/legacy-table';
import { MatIconModule } from '@angular/material/icon';
import { FlyPanelEditPerformanceCriteriaModule } from './fly-panel-edit-performance-criteria/fly-panel-edit-performance-criteria.module';

const routes: Routes = [
  {
    path: '',
    component: SimScenariosWizardCriteriaComponent,
  },
];


@NgModule({
  declarations: [SimScenariosWizardCriteriaComponent],
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    FormsModule,
    BaseModule,
    ReactiveFormsModule,
    MatCheckboxModule,
    MatSelectModule,
    MatTableModule,
    MatIconModule,
    FlyPanelEditPerformanceCriteriaModule

  ],
  exports :[SimScenariosWizardCriteriaComponent]
})
export class SimScenariosWizardCriteriaModule {}

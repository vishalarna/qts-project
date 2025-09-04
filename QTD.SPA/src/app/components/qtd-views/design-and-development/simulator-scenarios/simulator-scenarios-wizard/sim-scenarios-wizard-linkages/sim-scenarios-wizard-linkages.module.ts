import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BaseModule } from 'src/app/components/base/base.module';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { RouterModule, Routes } from '@angular/router';
import { SimScenariosWizardLinkagesComponent } from './sim-scenarios-wizard-linkages.component';
import { MatLegacyTableModule as MatTableModule } from '@angular/material/legacy-table';
import { MatIconModule } from '@angular/material/icon';
import { FlyPanelAddPositionsLinkagesModule } from './fly-panel-add-positions-linkages/fly-panel-add-positions.module';
import { FlyPanelAddObjectivesLinkagesModule } from './fly-panel-add-objectives-linkages/fly-panel-add-objectives.module';
import { FlyPanelAddProceduresLinkagesModule } from './fly-panel-add-procedures-linkages/fly-panel-add-procedures.module';
import {  MatLegacyPaginatorModule as MatPaginatorModule } from '@angular/material/legacy-paginator';
import { MatSortModule } from '@angular/material/sort';
import { MatToolbarModule } from '@angular/material/toolbar';
import { DialogueUnlinkObjectivesModule } from './dialogue-unlink-objectives/dialogue-unlink-objectives.module';
import { DialogueUnlinkMultipleRecordsModule } from './dialogue-unlink-multiple-records/dialogue-unlink-multiple-records.module';

const routes: Routes = [
  {
    path: '',
    component: SimScenariosWizardLinkagesComponent,
  },
];

@NgModule({
  declarations: [SimScenariosWizardLinkagesComponent],
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    FormsModule,
    BaseModule,
    ReactiveFormsModule,
    MatCheckboxModule,
    MatTableModule,
    MatIconModule,
    FlyPanelAddPositionsLinkagesModule,
    FlyPanelAddObjectivesLinkagesModule,
    FlyPanelAddProceduresLinkagesModule,
    MatPaginatorModule,
    MatSortModule,
    MatToolbarModule,
    DialogueUnlinkObjectivesModule,
    DialogueUnlinkMultipleRecordsModule
  ],
  exports:[SimScenariosWizardLinkagesComponent]
})
export class SimScenariosWizardLinkagesModule {}

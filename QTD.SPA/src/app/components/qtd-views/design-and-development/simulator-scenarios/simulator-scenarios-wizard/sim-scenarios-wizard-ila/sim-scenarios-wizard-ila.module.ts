import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BaseModule } from 'src/app/components/base/base.module';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { MatLegacySelectModule as MatSelectModule } from '@angular/material/legacy-select';
import { SimScenariosWizardIlaComponent } from './sim-scenarios-wizard-ila.component';
import { MatLegacyTableModule as MatTableModule } from '@angular/material/legacy-table';
import { MatIconModule } from '@angular/material/icon';
import { FlyPanelAddPrerequisitesIlaModule } from './fly-panel-add-prerequisites-ila/fly-panel-add-prerequisites-ila.module';
import { FlyPanelAddIlaModule } from './fly-panel-add-ila-ila/fly-panel-add-ila-ila.module';
import { MatSortModule } from '@angular/material/sort';
import { MatLegacyPaginatorModule as MatPaginatorModule } from '@angular/material/legacy-paginator';
import { MatLegacyDialogModule as MatDialogModule } from '@angular/material/legacy-dialog';
import { MatToolbarModule } from '@angular/material/toolbar';
import { DialogueUnlinkMultipleRecordsModule } from '../sim-scenarios-wizard-linkages/dialogue-unlink-multiple-records/dialogue-unlink-multiple-records.module';


@NgModule({
  declarations: [SimScenariosWizardIlaComponent],
  imports: [
    CommonModule,
    FormsModule,
    BaseModule,
    ReactiveFormsModule,
    MatCheckboxModule,
    MatSelectModule,
    MatTableModule,
    MatIconModule,
    FlyPanelAddIlaModule,
    FlyPanelAddPrerequisitesIlaModule,
    MatSortModule,
    MatPaginatorModule,
    MatDialogModule,
    MatToolbarModule,
    DialogueUnlinkMultipleRecordsModule
  ],
  exports :[SimScenariosWizardIlaComponent]
})
export class SimScenariosWizardIlaModule {}

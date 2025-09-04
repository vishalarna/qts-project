import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BaseModule } from 'src/app/components/base/base.module';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { MatLegacySelectModule as MatSelectModule } from '@angular/material/legacy-select';
import { MatLegacyTableModule as MatTableModule } from '@angular/material/legacy-table';
import { MatIconModule } from '@angular/material/icon';
import { SimScenariosWizardEventsAndScriptsComponent } from './sim-scenarios-wizard-events-and-scripts.component';
import { MatLegacyMenuModule as MatMenuModule } from '@angular/material/legacy-menu';
import { FlyPanelAddEventModule } from './fly-panel-add-event/fly-panel-add-event.module';
import { DragDropModule } from '@angular/cdk/drag-drop';

@NgModule({
  declarations: [SimScenariosWizardEventsAndScriptsComponent],
  imports: [
    CommonModule,
    FormsModule,
    BaseModule,
    ReactiveFormsModule,
    MatCheckboxModule,
    MatSelectModule,
    MatTableModule,
    MatIconModule,
    MatMenuModule,
    FlyPanelAddEventModule,
    DragDropModule,
  ],
  exports :[SimScenariosWizardEventsAndScriptsComponent]
})
export class SimScenariosWizardEventsAndScriptsModule {}

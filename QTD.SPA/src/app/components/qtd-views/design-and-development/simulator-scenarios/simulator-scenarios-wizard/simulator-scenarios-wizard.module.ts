import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BaseModule } from 'src/app/components/base/base.module';
import { LayoutModule } from '../../../layout/layout.module';
import { SimulatorScenariosWizardComponent } from './simulator-scenarios-wizard.component';
import { RouterModule, Routes } from '@angular/router';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatStepperModule } from '@angular/material/stepper';
import { MatLegacyOptionModule as MatOptionModule } from '@angular/material/legacy-core';
import { MatIconModule } from '@angular/material/icon';
import { SimScenariosWizardDetailsModule } from './sim-scenarios-wizard-details/sim-scenarios-wizard-details.module';
import { SimScenariosWizardLinkagesModule } from './sim-scenarios-wizard-linkages/sim-scenarios-wizard-linkages.module';
import { SimScenariosWizardCriteriaModule } from './sim-scenarios-wizard-criteria/sim-scenarios-wizard-criteria.module';
import { SimScenariosWizardSpecificationsModule } from './sim-scenarios-wizard-specifications/sim-scenarios-wizard-Specifications.module';
import { SimScenariosWizardEventsAndScriptsModule } from './sim-scenarios-wizard-events-and-scripts/sim-scenarios-wizard-events-and-scripts.module';
import { SimScenariosWizardInstructorModule } from './sim-scenarios-wizard-instructor/sim-scenarios-wizard-instructor.module';
import { SimScenariosWizardIlaModule } from './sim-scenarios-wizard-ila/sim-scenarios-wizard-ila.module';
import { PublishSimulatorScenarioModalModule } from './publish-simulator-scenario-modal/publish-simulator-scenario-modal.module';
import { ColloboratorSimulatorScenarioModalModule } from './colloborator-simulator-scenario-modal/colloborator-simulator-scenario-modal.module';
import { MatLegacyDialogModule as MatDialogModule } from '@angular/material/legacy-dialog';

const routes: Routes = [
  {
    path: '',
    component: SimulatorScenariosWizardComponent,
  },
];

@NgModule({
  declarations: [SimulatorScenariosWizardComponent],
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    FormsModule,
    BaseModule,
    ReactiveFormsModule,
    LayoutModule,
    MatToolbarModule,
    MatStepperModule,
    MatOptionModule,
    MatIconModule,
    SimScenariosWizardDetailsModule,
    SimScenariosWizardLinkagesModule,
    SimScenariosWizardCriteriaModule,
    SimScenariosWizardSpecificationsModule,
    SimScenariosWizardEventsAndScriptsModule,
    SimScenariosWizardIlaModule,
    SimScenariosWizardInstructorModule,
    PublishSimulatorScenarioModalModule,
    ColloboratorSimulatorScenarioModalModule,
    MatDialogModule
  ],
})
export class SimulatorScenariosWizardModule { }

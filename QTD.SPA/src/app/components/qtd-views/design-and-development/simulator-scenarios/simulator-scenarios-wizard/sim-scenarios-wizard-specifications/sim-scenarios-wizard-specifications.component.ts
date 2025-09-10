import { Component, Input, OnInit } from '@angular/core';
import { UntypedFormBuilder, UntypedFormGroup } from '@angular/forms';
import { MatTreeNestedDataSource } from '@angular/material/tree';
import { SimulatorScenario } from '@models/SimulatorScenario/SimulatorScenario';
import { SimulatorScenario_Position_VM } from '@models/SimulatorScenarios_New/SimulatorScenario_Position_VM';
import { SimulatorScenario_VM } from '@models/SimulatorScenarios_New/SimulatorScenario_VM';
import * as ckcustomBuild from 'src/app/ckcustomBuild/build/ckeditor.js'

@Component({
  selector: 'app-sim-scenarios-wizard-specifications',
  templateUrl: './sim-scenarios-wizard-specifications.component.html',
  styleUrls: ['./sim-scenarios-wizard-specifications.component.scss']
})
export class SimScenariosWizardSpecificationsComponent implements OnInit {
  @Input() inputSimulatorScenario_VM: SimulatorScenario_VM = new SimulatorScenario_VM();
  @Input() inputSimScenariosId: string;
  @Input() mode:string;
  specificationsForm: UntypedFormGroup;
  displayObjectivesColumns: string[] = ['number', 'description']
  displayPositionsColumns: string[] = ['number', 'description']
  taskEoObjectivesData:any[];
  positionsData:SimulatorScenario_Position_VM[];
  editor = ckcustomBuild;
  constructor(private formBuilder: UntypedFormBuilder) { }

  async ngOnInit(): Promise<void> {
    this.taskEoObjectivesData = [];
    this.positionsData = [];
    this.initializeSpecificationsForm();
    await this.loadAsync();
  }

  initializeSpecificationsForm() {
    this.specificationsForm = this.formBuilder.group({
      networkConfiguration: [this.inputSimulatorScenario_VM?.networkConfiguration ?? ""],
      loadingConditions: [this.inputSimulatorScenario_VM?.loadingConditions ?? ""],
      generations: [this.inputSimulatorScenario_VM?.generation ?? ""],
      interchange: [this.inputSimulatorScenario_VM?.interchange ?? ""],
      otherBaseCase: [this.inputSimulatorScenario_VM?.otherBaseCase ?? ""],
      validityChecks: [this.inputSimulatorScenario_VM?.validityChecks ?? ""],
      rolePlays: [this.inputSimulatorScenario_VM?.rolePlays ?? ""],
      documentation: [this.inputSimulatorScenario_VM?.documentation ?? ""],
    });
  }

  async loadAsync() {
    this.taskEoObjectivesData = [... this.inputSimulatorScenario_VM?.enablingObjectives,...this.inputSimulatorScenario_VM?.tasks];
    this.positionsData = this.inputSimulatorScenario_VM?.positions;
  }

  onNetworkConfigurationInput() {
    this.inputSimulatorScenario_VM.networkConfiguration = this.specificationsForm.get('networkConfiguration')?.value;
  }

  onLoadingConditionsInput() {
    this.inputSimulatorScenario_VM.loadingConditions = this.specificationsForm.get('loadingConditions')?.value;
  }

  onGenerationInput() {
    this.inputSimulatorScenario_VM.generation = this.specificationsForm.get('generations')?.value;
  }

  onInterchangeInput() {
    this.inputSimulatorScenario_VM.interchange = this.specificationsForm.get('interchange')?.value;
  }

  onOtherBaseCaseInput() {
    this.inputSimulatorScenario_VM.otherBaseCase = this.specificationsForm.get('otherBaseCase')?.value;
  }

  onValidityChecksInput() {
    this.inputSimulatorScenario_VM.validityChecks = this.specificationsForm.get('validityChecks')?.value;
  }

  onRolePlaysInput() {
    this.inputSimulatorScenario_VM.rolePlays = this.specificationsForm.get('rolePlays')?.value;
  }

  onDocumentationInput() {
    this.inputSimulatorScenario_VM.documentation = this.specificationsForm.get('documentation')?.value;
  }

}

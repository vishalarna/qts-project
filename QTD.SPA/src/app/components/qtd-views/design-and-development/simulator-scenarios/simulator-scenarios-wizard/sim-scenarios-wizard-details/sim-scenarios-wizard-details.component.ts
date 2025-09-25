import { Component, Input, OnInit } from '@angular/core';
import { UntypedFormBuilder, UntypedFormGroup, Validators } from '@angular/forms';
import { SimulatorScenario_Difficulty_VM } from '@models/SimulatorScenarios_New/SimulatorScenario_Difficulty_VM';
import { SimulatorScenario_VM } from '@models/SimulatorScenarios_New/SimulatorScenario_VM';
import * as ckcustomBuild from 'src/app/ckcustomBuild/build/ckeditor.js'

@Component({
  selector: 'app-sim-scenarios-wizard-details',
  templateUrl: './sim-scenarios-wizard-details.component.html',
  styleUrls: ['./sim-scenarios-wizard-details.component.scss']
})
export class SimScenariosWizardDetailsComponent implements OnInit {
  @Input() inputSimulatorScenario_VM: SimulatorScenario_VM = new SimulatorScenario_VM();
  @Input() difficultyList: SimulatorScenario_Difficulty_VM[];
  @Input() mode: string;
  scenarioDetailsForm: UntypedFormGroup;
  editor = ckcustomBuild;
  originalScenarioDetailsForm: any = {}

  constructor(
    private formBuilder: UntypedFormBuilder
  ) { }

  async ngOnInit(): Promise<void> {
    this.initializeScenarioDetailsForm();
  }

  initializeScenarioDetailsForm() {
    this.scenarioDetailsForm = this.formBuilder.group({
      scenarioTitle: [this.inputSimulatorScenario_VM?.title ?? "", Validators.required],
      scenarioDescription: [this.inputSimulatorScenario_VM?.description ?? ""],
      scenarioHours: [this.inputSimulatorScenario_VM?.durationHours ?? ""],
      scenarioMins: [this.inputSimulatorScenario_VM?.durationMinutes ?? ""],
      difficultyLevel: [this.inputSimulatorScenario_VM?.difficultyId ?? ""],
    });
    if(this.mode == "view"){
      this.scenarioDetailsForm.disable();
    }
    this.originalScenarioDetailsForm = {...this.scenarioDetailsForm}
  }

  onTitleInput() {
    this.inputSimulatorScenario_VM.title = this.scenarioDetailsForm.get('scenarioTitle')?.value;
  }

  onDescriptionInput() {
    this.inputSimulatorScenario_VM.description = this.scenarioDetailsForm.get('scenarioDescription')?.value;
  }

  onHoursInput() {
    this.inputSimulatorScenario_VM.durationHours = this.scenarioDetailsForm.get('scenarioHours')?.value;
  }

  onMinutesInput() {
    this.inputSimulatorScenario_VM.durationMinutes = this.scenarioDetailsForm.get('scenarioMins')?.value;
  }

  onDifficultySelect(event : any){
    this.inputSimulatorScenario_VM.difficultyId = event.value;
  }
  validateNumberInput(event: KeyboardEvent): void {
    const key = event.key;
    if (!/^[0-9]$/.test(key)) {
      event.preventDefault();
    }
  }

}

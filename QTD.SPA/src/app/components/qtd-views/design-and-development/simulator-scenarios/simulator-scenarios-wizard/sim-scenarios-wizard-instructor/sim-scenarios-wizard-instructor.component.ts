import { Component, Input, OnInit } from '@angular/core';
import { UntypedFormBuilder, UntypedFormGroup } from '@angular/forms';
import * as ckcustomBuild from 'src/app/ckcustomBuild/build/ckeditor.js';
import { RatingScaleNewService } from 'src/app/_Services/QTD/rating-scale-new.service';
import { SimulatorScenario_VM } from '@models/SimulatorScenarios_New/SimulatorScenario_VM';

@Component({
  selector: 'app-sim-scenarios-wizard-instructor',
  templateUrl: './sim-scenarios-wizard-instructor.component.html',
  styleUrls: ['./sim-scenarios-wizard-instructor.component.scss']
})
export class SimScenariosWizardInstructorComponent implements OnInit {
  @Input() inputSimulatorScenario_VM: SimulatorScenario_VM = new SimulatorScenario_VM();
  @Input() inputSimScenariosId: string;
  @Input() mode:string;
  ratingScaleForm: UntypedFormGroup;
  editor = ckcustomBuild;
  ratingScaleList: any[] = [];
  originalratingScaleForm: any = {};

  constructor(
    private formBuilder: UntypedFormBuilder,
    private ratingScaleService: RatingScaleNewService,
  ) { }

  async ngOnInit(): Promise<void> {
    this.initializeRatingScaleForm();
    await this.getRatingScaleAllAsync();
  }

  initializeRatingScaleForm() {
    this.ratingScaleForm = this.formBuilder.group({
      ratingScaleId: [this.inputSimulatorScenario_VM?.ratingScaleId ?? ""],
      operatingSkills: [this.inputSimulatorScenario_VM?.operatingSkillsEvaluationMethod ?? ""],
      notes: [this.inputSimulatorScenario_VM?.notes ?? ""],
    });
    this.originalratingScaleForm = {...this.ratingScaleForm.value}
  }

  async getRatingScaleAllAsync() {
    await this.ratingScaleService.getAll().then((res) => {
      this.ratingScaleList = res;
    });
  }

  onRatingScaleChange(event: any) {
    this.inputSimulatorScenario_VM.ratingScaleId = event?.value;
  }

  onOperatingSkillsInput() {
    this.inputSimulatorScenario_VM.operatingSkillsEvaluationMethod = this.ratingScaleForm.get('operatingSkills')?.value;
    if (this.inputSimulatorScenario_VM.operatingSkillsEvaluationMethod.trim() !== '') {
      this.ratingScaleForm.get('operatingSkills')?.markAsDirty();
    } else {
      this.ratingScaleForm.get('operatingSkills')?.markAsPristine();
    }
  }
 
  onNotesInput() {
    this.inputSimulatorScenario_VM.notes = this.ratingScaleForm.get('notes')?.value;
    if (this.inputSimulatorScenario_VM.operatingSkillsEvaluationMethod.trim() !== '') {
      this.ratingScaleForm.get('notes')?.markAsDirty();
    } else {
      this.ratingScaleForm.get('notes')?.markAsPristine();
    }
  }
}
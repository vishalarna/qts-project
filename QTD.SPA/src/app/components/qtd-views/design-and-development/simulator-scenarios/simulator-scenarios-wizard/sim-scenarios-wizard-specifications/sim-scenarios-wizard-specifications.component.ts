import { TemplatePortal } from '@angular/cdk/portal';
import { StepperSelectionEvent } from '@angular/cdk/stepper';
import { Component, Input, OnInit, ViewContainerRef } from '@angular/core';
import { UntypedFormBuilder, UntypedFormGroup } from '@angular/forms';
import { MatStepper } from '@angular/material/stepper';
import { MatTreeNestedDataSource } from '@angular/material/tree';
import { SimulatorScenario } from '@models/SimulatorScenario/SimulatorScenario';
import { SimulatorScenario_Position_VM } from '@models/SimulatorScenarios_New/SimulatorScenario_Position_VM';
import { SimulatorScenario_UpdatePositions_VM } from '@models/SimulatorScenarios_New/SimulatorScenario_UpdatePositions_VM';
import { SimulatorScenario_VM } from '@models/SimulatorScenarios_New/SimulatorScenario_VM';
import { Subscription } from 'rxjs';
import { LabelReplacementPipe } from 'src/app/_Pipes/label-replacement.pipe';
import { SimulatorScenariosService } from 'src/app/_Services/QTD/SimulatorScenarios/simulator-scenarios.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
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
  @Input() stepper!: MatStepper;
  specificationsForm: UntypedFormGroup;
  displayObjectivesColumns: string[] = ['number', 'description']
  displayPositionsColumns: string[] = ['number', 'description']
  taskEoObjectivesData:any[];
  positionsData:SimulatorScenario_Position_VM[];
  editor = ckcustomBuild;
  positionsUpdateOptions: SimulatorScenario_UpdatePositions_VM;
  linkedPositionIds: any[] = []; 
  originalSpecifications: any = {};
 
  private stepperSub!: Subscription;

  constructor(
    public flyPanelService: FlyInPanelService,
    public vcf: ViewContainerRef,
    private alert: SweetAlertService,
    private labelPipe: LabelReplacementPipe,
    private formBuilder: UntypedFormBuilder,
    private simSceariosService: SimulatorScenariosService,
  ) { }

  async ngOnInit(): Promise<void> {
    this.taskEoObjectivesData = [];
    this.positionsData = [];
    this.positionsUpdateOptions = new SimulatorScenario_UpdatePositions_VM();
    this.initializeSpecificationsForm();
    await this.loadAsync();
    this.stepperSub = this.stepper.selectionChange.subscribe((event: StepperSelectionEvent) => {
      const activeIndex = event.selectedIndex;
      if (activeIndex === 3) {
        this.reload();
      }
    });
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
   this.originalSpecifications = { ...this.specificationsForm.value };
  }

  reload(): void {
    this.positionsData = this.inputSimulatorScenario_VM?.positions;
    this.linkedPositionIds =
      this.inputSimulatorScenario_VM?.positions?.map(
        (position) => position?.positionId
      ) ?? [];
  }

  async loadAsync() {
    this.taskEoObjectivesData = [... this.inputSimulatorScenario_VM?.enablingObjectives,...this.inputSimulatorScenario_VM?.tasks];
    this.positionsData = this.inputSimulatorScenario_VM?.positions;
    this.linkedPositionIds =
      this.inputSimulatorScenario_VM?.positions?.map(
        (position) => position?.positionId
      ) ?? [];
  }

  ngOnDestroy(): void {
    if (this.stepperSub) {
      this.stepperSub.unsubscribe();
    }
  }

  onNetworkConfigurationInput() {
    this.inputSimulatorScenario_VM.networkConfiguration = this.specificationsForm.get('networkConfiguration')?.value;
    if (this.inputSimulatorScenario_VM.networkConfiguration.trim() !== '') {
      this.specificationsForm.get('networkConfiguration')?.markAsDirty();
    } else {
      this.specificationsForm.get('networkConfiguration')?.markAsPristine();
    }
  }
 
  onLoadingConditionsInput() {
    this.inputSimulatorScenario_VM.loadingConditions = this.specificationsForm.get('loadingConditions')?.value;
    if (this.inputSimulatorScenario_VM.loadingConditions.trim() !== '') {
      this.specificationsForm.get('loadingConditions')?.markAsDirty();
    } else {
      this.specificationsForm.get('loadingConditions')?.markAsPristine();
    }
  }
 
  onGenerationInput() {
    this.inputSimulatorScenario_VM.generation = this.specificationsForm.get('generations')?.value;
    if (this.inputSimulatorScenario_VM.loadingConditions.trim() !== '') {
      this.specificationsForm.get('generations')?.markAsDirty();
    } else {
      this.specificationsForm.get('generations')?.markAsPristine();
    }
  }
 
  onInterchangeInput() {
    this.inputSimulatorScenario_VM.interchange = this.specificationsForm.get('interchange')?.value;
    if (this.inputSimulatorScenario_VM.loadingConditions.trim() !== '') {
      this.specificationsForm.get('interchange')?.markAsDirty();
    } else {
      this.specificationsForm.get('interchange')?.markAsPristine();
    }
  }
 
  onOtherBaseCaseInput() {
    this.inputSimulatorScenario_VM.otherBaseCase = this.specificationsForm.get('otherBaseCase')?.value;
    if (this.inputSimulatorScenario_VM.loadingConditions.trim() !== '') {
      this.specificationsForm.get('otherBaseCase')?.markAsDirty();
    } else {
      this.specificationsForm.get('otherBaseCase')?.markAsPristine();
    }
  }
 
  onValidityChecksInput() {
    this.inputSimulatorScenario_VM.validityChecks = this.specificationsForm.get('validityChecks')?.value;
 
    if (this.inputSimulatorScenario_VM.loadingConditions.trim() !== '') {
      this.specificationsForm.get('validityChecks')?.markAsDirty();
    } else {
      this.specificationsForm.get('validityChecks')?.markAsPristine();
    }
  }
 
  onRolePlaysInput() {
    this.inputSimulatorScenario_VM.rolePlays = this.specificationsForm.get('rolePlays')?.value;
    if (this.inputSimulatorScenario_VM.loadingConditions.trim() !== '') {
      this.specificationsForm.get('rolePlays')?.markAsDirty();
    } else {
      this.specificationsForm.get('rolePlays')?.markAsPristine();
    }
  }
 
  onDocumentationInput() {
    this.inputSimulatorScenario_VM.documentation = this.specificationsForm.get('documentation')?.value;
    if (this.inputSimulatorScenario_VM.loadingConditions.trim() !== '') {
      this.specificationsForm.get('documentation')?.markAsDirty();
    } else {
      this.specificationsForm.get('documentation')?.markAsPristine();
    }
  }

  openFlypanel(templateRef: any) {
    const portal = new TemplatePortal(templateRef, this.vcf);
    this.flyPanelService.open(portal);
  }

  async handleNewPositionsLinked(event: any) {
    const newPositions = event;
    newPositions.forEach((newPosition) => {
      const exists = this.positionsData.some(
        (position) => position.id === newPosition.id
      );
      if (!exists) {
        const position = {
          id: null,
          positionId: newPosition.positionId,
          positionTitle: newPosition.positionTitle,
        };
        this.positionsData.push(position);
        this.linkedPositionIds.push(newPosition.positionId);
      }
    });
    await this.linkPosToScenariosAsync();
    this.alert.successToast('Simulator Scenario ' + await this.labelPipe.transform('Position') + 's Linked Successfully');
  }
    
  async linkPosToScenariosAsync() {
    this.positionsUpdateOptions.positions = this.positionsData;
    let options = this.positionsUpdateOptions
    await this.simSceariosService.linkPosistionToScenarios(this.inputSimScenariosId, options).then(async (res) => {
      this.inputSimulatorScenario_VM.positions = res;
    });
  }
}

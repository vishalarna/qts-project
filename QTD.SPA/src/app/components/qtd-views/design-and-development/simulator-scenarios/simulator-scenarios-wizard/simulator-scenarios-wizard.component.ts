import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { UntypedFormBuilder } from '@angular/forms';
import { MatLegacyDialog as MatDialog } from '@angular/material/legacy-dialog';
import { MatStep, MatStepper } from '@angular/material/stepper';
import { Router } from '@angular/router';
import { SimScenariosWizardDetailsComponent } from './sim-scenarios-wizard-details/sim-scenarios-wizard-details.component';
import { SimulatorScenario_VM } from '@models/SimulatorScenarios_New/SimulatorScenario_VM';
import { SimulatorScenariosService } from 'src/app/_Services/QTD/SimulatorScenarios/simulator-scenarios.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { SimulatorScenario_Difficulty_VM } from '@models/SimulatorScenarios_New/SimulatorScenario_Difficulty_VM';
import { Location } from '@angular/common';
import { sideBarClose } from 'src/app/_Statemanagement/action/state.menutoggle';
import { Store } from '@ngrx/store';
import { SimScenariosWizardSpecificationsComponent } from './sim-scenarios-wizard-specifications/sim-scenarios-wizard-specifications.component';
import { SimScenariosWizardInstructorComponent } from './sim-scenarios-wizard-instructor/sim-scenarios-wizard-instructor.component';
import { PublishSimulatorScenarioModalComponent } from './publish-simulator-scenario-modal/publish-simulator-scenario-modal.component';
import { ColloboratorSimulatorScenarioModalComponent } from './colloborator-simulator-scenario-modal/colloborator-simulator-scenario-modal.component';
import { SimulatorScenario_Collaborator_VM } from '@models/SimulatorScenarios_New/SimulatorScenario_Collaborator_VM';
import { SimScenariosWizardCriteriaComponent } from './sim-scenarios-wizard-criteria/sim-scenarios-wizard-criteria.component';
import { LabelReplacementPipe } from 'src/app/_Pipes/label-replacement.pipe';

@Component({
  selector: 'app-simulator-scenarios-wizard',
  templateUrl: './simulator-scenarios-wizard.component.html',
  styleUrls: ['./simulator-scenarios-wizard.component.scss']
})
export class SimulatorScenariosWizardComponent implements OnInit, AfterViewInit {
  simScenariosId: string;
  @ViewChild('stepper') stepper: MatStepper;
  @ViewChild('simScenariosDetails') simScenariosDetails: SimScenariosWizardDetailsComponent;
  @ViewChild('simScenariosCriteria') simScenariosCriteria: SimScenariosWizardCriteriaComponent;
  @ViewChild('simScenariosSpecifications') simScenariosSpecifications: SimScenariosWizardSpecificationsComponent;
  @ViewChild('simScenariosInstructor') simScenariosInstructor: SimScenariosWizardInstructorComponent;
  @ViewChild('publishSimulatorScenario') publishSimulatorScenario: PublishSimulatorScenarioModalComponent;
  @ViewChild('collaboratorSimulatorScenario') collaboratorSimulatorScenario: ColloboratorSimulatorScenarioModalComponent;
  simulatorScenario_VM: SimulatorScenario_VM = new SimulatorScenario_VM();
  difficultyList: SimulatorScenario_Difficulty_VM[];
  collaborator_List: SimulatorScenario_Collaborator_VM[] = [];
  currentIndex: number = 0;
  navigatedUrl: string;
  mode: string;
  loader: boolean = true;
  goToNext:boolean = false;
  isWizardLoading:boolean;
  get isStep1FormValid(): boolean {
    return (this.simScenariosDetails?.scenarioDetailsForm?.valid ?? false) || (this.mode == "view");
  }


get isStep1FormDirty(): boolean {
  if (!this.simScenariosDetails?.scenarioDetailsForm) return false;

  const currentValues = this.simScenariosDetails?.scenarioDetailsForm.value;
  return Object.keys(currentValues).some(
    key => (currentValues[key] ?? "").trim() !== (this.simScenariosDetails.originalScenarioDetailsForm[key] ?? "").trim()
  );
}

get isStep4FormDirty(): boolean {
  if (!this.simScenariosSpecifications.specificationsForm) return false;

  const currentValues = this.simScenariosSpecifications.specificationsForm.value;
  return Object.keys(currentValues).some(
    key => (currentValues[key] ?? "").trim() !== (this.simScenariosSpecifications.originalSpecifications[key] ?? "").trim()
  );
}

get isStep6FormDirty(): boolean {
  if (!this.simScenariosInstructor?.ratingScaleForm) return false;

  const currentValues = this.simScenariosInstructor?.ratingScaleForm.value;
  return Object.keys(currentValues).some(
    key => (currentValues[key] ?? "").trim() !== (this.simScenariosInstructor.originalratingScaleForm[key] ?? "").trim()
  );
}

  constructor(
    private formBuilder: UntypedFormBuilder,
    private router: Router,
    public dialog: MatDialog,
    private simScenariosService: SimulatorScenariosService,
    private alert: SweetAlertService,
    private location: Location,
    private labelPipe: LabelReplacementPipe,
    private store: Store<{ toggle: string }>,) { }

  ngOnInit(){
    this.isWizardLoading = false;
    let segments = this.router.url.split('/');
    if (segments.includes('create')) {
      this.mode = 'create';
    }
    else if ((segments.includes('edit'))) {
      this.mode = 'edit';
      this.simScenariosId = segments.reverse()[0];
    }
    else if ((segments.includes('view'))) {
      this.mode = 'view';
      this.simScenariosId = segments.reverse()[0];
    }
    this.simulatorScenario_VM = new SimulatorScenario_VM();
    this.store.dispatch(sideBarClose());
    this.getAllDifficultyAsync();
  }

  ngAfterViewInit(): void {
    const state: any = this.location.getState();
    if (state?.goToNext && this.simScenariosId) {
      this.simulatorScenario_VM = state.data;
      this.goToNext = true;
      this.patchReviewDetails();
 
      if (this.stepper) {
        setTimeout(() => {
          this.stepper.selectedIndex = 1;
          history.replaceState({}, '');
        }, 1);
      }
    } else if (this.simScenariosId) {
      this.loadAsync();
    } else {
      if (this.stepper) {
        setTimeout(() => this.stepper.selectedIndex = 0, 1);
      }
    }
  }

  async loadAsync() {
    this.scrollToTop();
    await this.getSimScenariosAsync();
  }

   onSelectionChange(event: any) {
    let stepDifference= event.selectedIndex - event.previouslySelectedIndex;
    let matSteps:MatStep[] = this.stepper.steps.toArray();
    if(this.simScenariosId){
      if(stepDifference>1){

        for(let i=event.previouslySelectedIndex+1;i<event.selectedIndex;i++){
          matSteps[i].interacted=true;
          matSteps[i].completed=true;
        }
      }

    }
    this.currentIndex = event.selectedIndex;
     if (this.mode == 'edit') {
      if ((event.previouslySelectedIndex == 0 && !this.goToNext) || event.previouslySelectedIndex == 3 || event.previouslySelectedIndex == 5) {
         this.updateScenarioAsync(false,event.previouslySelectedIndex);
      }
    }
    if (event.selectedIndex == 2) {
      if (this.simScenariosCriteria && typeof this.simScenariosCriteria.initializeCriteria === 'function') {
          this.simScenariosCriteria.initializeCriteria();
      }
    }
    this.goToNext = false;
  }

 async continueClick() {
  if (this.mode === 'create' && this.currentIndex === 0) {
    await this.createScenarioAsync();
  } else {
    this.stepper.next();
  }
 }

  exitWizard(templateRef: any) {
    debugger
  const currentStep = this.stepper.selectedIndex;

  let isDirty = false;
  if (currentStep === 0) {
    isDirty = this.isStep1FormDirty;
  } else if (currentStep === 3) { 
    isDirty = this.isStep4FormDirty;
  } else if (currentStep === 5) { 
    isDirty = this.isStep6FormDirty;
  }

  if (this.mode !== 'view' && isDirty) {
    this.dialog.open(templateRef, {
      width: '600px',
      hasBackdrop: true,
      disableClose: true,
    });
  } else {
    this.router.navigate(['/dnd/simulatorscenarios/overview']);
  }
}


  navigateToOverview(){
    this.dialog.closeAll();
     this.router.navigate(['/dnd/simulatorscenarios/overview']);
  }
 
  async closeWizard(){
    await this.router.navigate(['/dnd/simulatorscenarios/overview']);
  }

  async backOrExitWizardAsync() {
    if(this.mode == 'create'){
      this.dialog.closeAll();
      await this.createScenarioAsync(true); 
    }
    else{
      this.dialog.closeAll();
      await this.updateScenarioAsync(true,0);
    }
  }

  async createScenarioAsync(isBackToOverview : boolean = false) {
    this.simScenariosService.createAsync(this.simulatorScenario_VM).then(async (res) => {
      if(!isBackToOverview){
        await this.router.navigate(['/dnd/simulatorscenarios/edit/' + res.id], { state: { goToNext: true,data:res } });
      }
      else{
        await this.closeWizard();
      }
      this.alert.successToast("Simulator Scenario Created Successfully");
    }).catch((error) => {
      this.alert.errorToast("Failed to Create Simulator Scenario");
    });
  }

  async updateScenarioAsync(isBackToOverview : boolean = false, stepIndex: number = 0) {
    this.simScenariosService.updateAsync(this.simScenariosId, this.simulatorScenario_VM).then(async (res) => {
      this.simScenariosId = res.id;
      this.simulatorScenario_VM = res;
      if(isBackToOverview){
        await this.closeWizard();
      }
      switch (stepIndex) {
        case 0:
            this.alert.successToast("Simulator Scenario Details Updated Successfully");
            break;
        case 3:
            this.alert.successToast("Simulator Scenario Specifications Updated Successfully");
            break;
        case 5:
            this.alert.successToast('Simulator Scenario ' + (await this.labelPipe.transform('Instructor')) + ' Prep Updated Successfully');
            break;
      }
      }).catch((error) => {
          this.alert.errorToast("Failed to Update Simulator Scenario");
      });
  }

  async getAllDifficultyAsync() {
    await this.simScenariosService.getAllDifficultyAsync().then((res) => {
      this.difficultyList = res;
    });
  }

  async getSimScenariosAsync() {
    this.isWizardLoading = true;
    if (this.simScenariosId) {
      await this.simScenariosService.getAsync(this.simScenariosId).then(res => {
        this.simulatorScenario_VM = res;
        this.patchReviewDetails();
        this.isWizardLoading=false;
        if (this.simScenariosSpecifications && typeof this.simScenariosSpecifications.initializeSpecificationsForm == 'function') {
          this.simScenariosSpecifications.inputSimulatorScenario_VM = res;
          this.simScenariosSpecifications.initializeSpecificationsForm();
        }
        if (this.simScenariosInstructor && typeof this.simScenariosInstructor.initializeRatingScaleForm == 'function') {
          this.simScenariosInstructor.inputSimulatorScenario_VM = res;
          this.simScenariosInstructor.initializeRatingScaleForm();
        }
      });
    }
    else {
      this.simulatorScenario_VM = new SimulatorScenario_VM();
    }
  }

  patchReviewDetails(){
    if (this.simScenariosDetails && typeof this.simScenariosDetails.initializeScenarioDetailsForm == 'function') {
      this.simScenariosDetails.inputSimulatorScenario_VM = this.simulatorScenario_VM;
      this.simScenariosDetails.initializeScenarioDetailsForm();
    }
  }

  private scrollToTop() {
    const stepperContainer = document.querySelector('.mat-sidenav-content');
    if (stepperContainer) {
      stepperContainer.scrollTop = 0;
    }
  }

  publishWizard(templateRef:any){
    const dialogRef = this.dialog.open(templateRef, {
      width: '600px',
      height: 'auto',
      hasBackdrop: true,
      disableClose: true,
    });
  }

  handlePublishScenarios(event: any){
    this.simulatorScenario_VM.publishedDate=event.effectiveDate;
    this.simulatorScenario_VM.publishedReason=event.note;
    this.simScenariosService.publishScenarioAsync(this.simScenariosId, this.simulatorScenario_VM).then(async (_) => {
      this.alert.successToast("Simulator Scenario Published Successfully");
      this.router.navigate(['/dnd/simulatorscenarios/overview']);
    });
  }

  openCollaborator(templateRef:any){
    const dialogRef = this.dialog.open(templateRef, {
      width: '670px',
      height: '730px',
      hasBackdrop: true,
      disableClose: true,
      panelClass : "collaborator-dialog"
    });
  }

  

}

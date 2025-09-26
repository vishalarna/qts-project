import { BreakpointObserver } from '@angular/cdk/layout';
import { StepperOrientation } from '@angular/cdk/stepper';
import { ChangeDetectorRef, Component, Inject, Input, OnDestroy,Output, OnInit, ViewChild, EventEmitter } from '@angular/core';
import { MatLegacyDialog as MatDialog } from '@angular/material/legacy-dialog';
import { MatStep, MatStepper } from '@angular/material/stepper';
import { ActivatedRoute, Router } from '@angular/router';
import { select, Store } from '@ngrx/store';
import { Observable, pipe, Subject } from 'rxjs';
import { map, takeUntil } from 'rxjs/operators';
import { sideBarClose, sideBarToggle } from 'src/app/_Statemanagement/action/state.menutoggle';
import { DataBroadcastService } from 'src/app/_Shared/services/DataBroadcast.service';
import { CollaborateIlaModalComponent } from './ila-wizard-components/collaborate-ila-modal/collaborate-ila-modal.component';
import { PublishIlaModalComponent } from './ila-wizard-components/publish-ila-modal/publish-ila-modal.component';
import { deleteILA, saveILA } from 'src/app/_Statemanagement/action/state.componentcommunication';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { ILA } from 'src/app/_DtoModels/ILA/ILA';
import { SubSink } from 'subsink';
import { IlaService } from 'src/app/_Services/QTD/ila.service';
import { CreateIlaComponent } from './ila-wizard-components/create-ila/create-ila.component';
import { IlaDetailsComponent } from './ila-wizard-components/ila-details/ila-details.component';
import { SeqAndScheduleComponent } from './ila-wizard-components/seq-and-schedule/seq-and-schedule.component';
import { IlaEvaluationComponent } from './ila-wizard-components/ila-evaluation/ila-evaluation.component';
import { TraineeEvaluationComponent } from './ila-wizard-components/trainee-evaluation/trainee-evaluation.component';
import { LabelReplacementPipe } from 'src/app/_Pipes/label-replacement.pipe';
import { IlaApplicationComponent } from './ila-wizard-components/ila-application/ila-application.component';
import { ILADetailsVM } from '@models/ILA/ILADetailsVM';

@Component({
  selector: 'app-ila-create-wizard',
  templateUrl: './ila-create-wizard.component.html',
  styleUrls: ['./ila-create-wizard.component.scss'],
})
export class IlaCreateWizardComponent implements OnInit, OnDestroy {
  contorlCheckSimulation : boolean = false;
  stepperOrientation: Observable<StepperOrientation>;
  selectedIndex: number = 0;
  hide: boolean = false;
  isLoading:boolean=true;
  previewContainer: string | undefined;
  create_new: boolean = false;
  saveEvent = new Observable<any>();
  deleteEvent = new Observable<any>();
  @ViewChild('stepper') stepper: MatStepper;
  @ViewChild('saveILA') saveILA:any;
  @ViewChild('createILA') createILA!:CreateIlaComponent;
  @ViewChild('ilaDetails') ilaDetails!:IlaDetailsComponent;
  @ViewChild('seqAndSchedule') seqAndSchedule!:SeqAndScheduleComponent;
  @Input() formStatus: string = "INVALID"
  IsTabNotSaving=true;
  doUpdate = false;
  enableNavigation = false;
  showSpinner = false;
  show_simulations: boolean;
  title_text: any;
  simulation_status: boolean = false;
  toggleMenu: Observable<string>;
  tabchange_bool: boolean = false;
  destroy = new Subject<void>();
  flyPanelChangesRecieved: any;
  ila_change: boolean = false;
  dynamicName: any;
  dynamicNickName: any;
  nerc_check:boolean=true;
  editIlaCheck:any;
  savedDraftCheck:boolean=false;
  confirm:any;
  ila!:ILADetailsVM;
  isEditMode:boolean = false;
  subscription = new SubSink();
  ilaId!:any;
  providerIsNerc = false;
  header : string;
  description = "Leaving this page will discard any unsaved changes.";
  mode:string;
  isILASaved:boolean = false;
  isPublicCourseChecked: boolean = false;

  @ViewChild('ilaEvaluation') ilaEvaluationComponent: IlaEvaluationComponent;
  @ViewChild('traineeAssessment') traineeAssessment: TraineeEvaluationComponent;
  @ViewChild('ilaApplication') ilaApplication:IlaApplicationComponent;

  constructor(
    private databroadcastSrvc: DataBroadcastService,
    public router: Router,
    public dialog: MatDialog,
    private breakpointObserver: BreakpointObserver,
    private store: Store<{ toggle: string }>,
    private saveStore: Store<{ saveIla: any }>,
    private deleteStore: Store<{ deleteIla: any }>,
    private alert: SweetAlertService,
    public changeDetector: ChangeDetectorRef,
    private route:ActivatedRoute,
    private ilaService:IlaService,
    private labelPipe: LabelReplacementPipe
  ) {
    this.stepperOrientation = breakpointObserver
      .observe('(min-width: 800px)')
      .pipe(map(({ matches }) => (matches ? 'horizontal' : 'vertical')));
  }

  ngOnInit(): void {
    this.header="Save " + this.labelPipe.transform('ILA');
    this.toggleMenu = this.store.select('toggle');
    this.changeDetector.detectChanges();
    this.store.dispatch(sideBarClose());
    this.subscription.sink = this.route.queryParams.subscribe((res)=>{
      if(res.data !== undefined){
        this.ilaId = res.data;
        this.getILAData();
        if(res.isViewMode) {
          this.mode = 'view';
        }else if(res.isCreateMode)
        {
          this.mode='create';
        }else{
          this.mode='edit';
        }
      }
      else{
        this.mode = 'create';
      }
    })
    this.isLoading = false;
  }

  async getILAData(){
    if(this.ilaId){
      this.ila = await this.ilaService.get(this.ilaId);
      this.dynamicName = this.ila.name + ' - ' + this.ila.number;
    }
  }

  recieveEditIlaCheck(e:any){
    this.editIlaCheck = e;
  }

  detectIlaDetailsFormStatus(e: any) {  
    switch (e) {
      case "VALID":
        this.formStatus = "VALID"
        this.showSpinner = false;
        this.doUpdate = true;
        this.enableNavigation = true;
        break;
      case "INVALID":
        this.formStatus = "INVALID"
        this.showSpinner = false;
        this.doUpdate = true;
        this.enableNavigation = true;
        break;

      default:
        this.formStatus = e;
        break;
    }
  }

  async detectCreateIlaFormStatus(event: any) {   
    switch (event) {
      case "SAVED":
        this.alert.successToast( await this.labelPipe.transform('ILA') +" Data Saved As Draft");
        this.formStatus = "INVALID";
        this.showSpinner = false;
        this.doUpdate = true;
        this.enableNavigation = true;
        this.stepper.selected?.completed ? true : undefined;
        if (this.stepper && this.IsTabNotSaving) {
          setTimeout(() => this.stepNext(), 1);
        }
        this.IsTabNotSaving=true;
        this.savedDraftCheck = true;
        break;
      case "FAILED":
        this.showSpinner = false;
        this.formStatus = "VALID";
        this.enableNavigation = true;
        break;
      case "UPDATED":
        this.showSpinner = false;
        this.formStatus = "INVALID";
        this.alert.successToast(await this.labelPipe.transform('ILA') + " Data Updated");
        if (this.stepper && this.IsTabNotSaving) {
          this.enableNavigation=true;
         // this.stepper.next();
        }
        break;
      case "DELETE":
        this.doUpdate = false;
        this.enableNavigation = false;
        this.stepper.reset();
        break;
      case "VALID":
        this.enableNavigation = true;
        this.formStatus = "VALID"
        break;
      case "INVALID":
        this.enableNavigation = false;
        this.formStatus = "INVALID"
        break;
      default:
        this.formStatus = event;
        break;
    }
  }

  // recieveName(e: any) {
  //   this.dynamicName = e.name + "-" + e.Nickname;

  //   
  // }

  dynamicnameReturn() {
    return this.dynamicName;
  }

  detectTrainingPlanFormStatus(event: any) {
    if (this.selectedIndex !== 2) {
      return;
    }
    switch (event) {
      case "INVALID":
        this.formStatus = event;
        break;
      case "VALID":
        this.formStatus = event;
        break;
    }
  }

  ngOnDestroy(): void {
    this.destroy.unsubscribe();
  }

  onSelectionChange(event: any) {
    if(event.selectedIndex != 0){
      this.isLoading = true;
    }  
    if (this.selectedIndex===0) {
      this.IsTabNotSaving=false;
      this.SaveIlaDetails();
    }
    
    let stepDifference= event.selectedIndex - event.previouslySelectedIndex;
      let matSteps:MatStep[] = this.stepper.steps.toArray();
      if(this.mode !=='create'){
        if(stepDifference>1){

          for(let i=event.previouslySelectedIndex+1;i<event.selectedIndex;i++){
            matSteps[i].interacted=true;
            matSteps[i].completed=true;
          }
        }

      } 
      if(!this.isPublicCourseChecked){
      this.createILA.validateNickName(this.isPublicCourseChecked);
    }     
      this.selectedIndex = event.selectedIndex;
    // if (this.selectedIndex > 0 && this.selectedIndex < 4) {
    //   this.formStatus = "VALID";
    //   return;
    // }
    // else if(this.selectedIndex === 4){
    //   this.formStatus = "INVALID";
    // }
    // else{
    //   this.formStatus = "INVALID";
    // }
  }

  toggleMainMenu() {
    //this.databroadcastSrvc.ToggleMainMenu.next('');
    this.store.dispatch(sideBarToggle())
  }

  async goBack() {   
    if(this.previewContainer === 'edit'){
      this.closeGuideEditor();
    }
    else if(this.ila !== null && this.ila?.isPublished !== undefined) {
      this.header = "Quit " + await this.labelPipe.transform('ILA') ;
      this.description = this.ila.isPublished ? "Leaving this page will discard any unsaved changes":"This " + await this.labelPipe.transform('ILA') + " is Saved as Draft unsaved changes will be lost!!";
      const dialogRef = this.dialog.open(this.saveILA, {
        width: '600px',
        height: 'auto',
        hasBackdrop: true,
        disableClose: true,
      });
      //this.continueClicked();
    /*   await this.router.navigate(['dnd/ila']); */
    }
    else{
      await this.router.navigate(['dnd/ila']);
    }
  }

  moveNext(e: Event) {
    
  }

  collaborate_Clicked() {
    const dialog_ref = this.dialog.open(CollaborateIlaModalComponent, {
      width: '60%',
    });
    dialog_ref.disableClose = true;
    dialog_ref.afterClosed().subscribe({
      next: (data) => {
        
      },
    });
  }

  publish_Clicked(templateRef:any) {
    this.previewContainer = undefined;
    const dialog_ref = this.dialog.open(templateRef, {
      width: '600px',
      height: 'auto',
      hasBackdrop: true,
      disableClose: true,
    });
    dialog_ref.afterClosed().subscribe({
      next: async(data) => {
        this.previewContainer = undefined;
        if(this.selectedIndex === 6){   
          if(this.seqAndSchedule.allowSettings && this.seqAndSchedule.ilaEmpTqSettings && typeof this.seqAndSchedule.ilaEmpTqSettings.saveTQRelease === 'function'){
            await this.seqAndSchedule.ilaEmpTqSettings.saveTQRelease();
          } 
          if(this.seqAndSchedule.allowSettings && this.seqAndSchedule.ilaEmpEvalSettings && typeof this.seqAndSchedule.ilaEmpEvalSettings.saveInfoEvalRelease === 'function'){
            await this.seqAndSchedule.ilaEmpEvalSettings.saveInfoEvalRelease();
          } 
          if(this.seqAndSchedule.allowSettings && this.seqAndSchedule.ilaTestRelease && typeof this.seqAndSchedule.ilaTestRelease.saveInfoTestRelease === 'function'){
            await this.seqAndSchedule.ilaTestRelease.saveInfoTestRelease();
          } 
          await this.seqAndSchedule.saveSequenceAndScheduleDataAsync();
        }
        if(this.selectedIndex==7){
          await this.ilaApplication.saveApplicationInfo();
        }
        //this.selectedIndex = this.providerIsNerc ? 7:6;
      },
    });
  }
  close_Clicked() {
    this.previewContainer = undefined;
    this.selectedIndex = 1;
  }
  async continueClicked() {
    this.isLoading=true;
    if (this.mode === 'view') {
      if (this.stepper) {
        const currentStep = this.stepper.steps.toArray()[this.selectedIndex];
        if (currentStep) {
          currentStep.completed = true;
        }
  
        const nextIndex = this.selectedIndex + 1;
        if (nextIndex < this.stepper.steps.length) {
          this.stepper.selectedIndex = nextIndex;
        }
        const headers = document.querySelectorAll(".create-ila-stepper .mat-horizontal-stepper-header-container .mat-step-header");
        const lines = document.querySelectorAll(".create-ila-stepper .mat-horizontal-stepper-header-container .mat-stepper-horizontal-line");
        if (headers[this.selectedIndex]) headers[this.selectedIndex].classList.add('interacted');
        if (lines[this.selectedIndex]) lines[this.selectedIndex].classList.add('interacted');
      }
      this.showSpinner = false;
      return;
    }
    if(this.mode==='create'){
      let matStepHeaders = document.querySelectorAll(".create-ila-stepper .mat-horizontal-stepper-header-container .mat-step-header");
      let matStepHeadersAdjacent = document.querySelectorAll(".create-ila-stepper .mat-horizontal-stepper-header-container .mat-stepper-horizontal-line");
      matStepHeaders[this.selectedIndex].classList.add('interacted');
      matStepHeadersAdjacent[this.selectedIndex].classList.add('interacted');
    }
    if(this.selectedIndex === 2){
      
      this.databroadcastSrvc.saveTrainingPlan.next(null);
    }
    if(this.selectedIndex === 5){
     await this.ilaEvaluationComponent.saveILAStudentEvaluation();
    }
    if (this.stepper && this.selectedIndex !== 0) {
      setTimeout(() =>this.stepper.next(), 1);
      this.showSpinner = false;
    }
    else {
      this.formStatus = "INVALID";
      this.showSpinner = true;
      this.saveStore.dispatch(saveILA({ saveData: {}, tabIndex: this.selectedIndex, update: this.doUpdate }));
      setTimeout(() =>this.stepper.next(), 1);
    }
    if(this.selectedIndex===1){
        await this.ilaDetails.savePrerequisitesAsync(this.ilaId);
    }
    if(this.selectedIndex === 6){   
      if(this.seqAndSchedule.allowSettings && this.seqAndSchedule.ilaEmpTqSettings && typeof this.seqAndSchedule.ilaEmpTqSettings.saveTQRelease === 'function'){
        await this.seqAndSchedule.ilaEmpTqSettings.saveTQRelease();
      } 
      if(this.seqAndSchedule.allowSettings && this.seqAndSchedule.ilaEmpEvalSettings && typeof this.seqAndSchedule.ilaEmpEvalSettings.saveInfoEvalRelease === 'function'){
        await this.seqAndSchedule.ilaEmpEvalSettings.saveInfoEvalRelease();
      } 
      if(this.seqAndSchedule.allowSettings && this.seqAndSchedule.ilaTestRelease && typeof this.seqAndSchedule.ilaTestRelease.saveInfoTestRelease === 'function'){
        await this.seqAndSchedule.ilaTestRelease.saveInfoTestRelease();
      } 
      await this.seqAndSchedule.saveSequenceAndScheduleDataAsync();
    }
    if(this.selectedIndex === 4){
       await this.traineeAssessment.updateEvalMethod();
     
    }
  }

  getLoadingFlag(isLoading: any){
    this.isLoading = isLoading;
  }

  detectILACreation(e:any){
    this.isILASaved = e;
  }

  SaveIlaDetails() {   
    if (this.formStatus !== "INVALID") {
      this.formStatus = "INVALID";
      this.saveStore.dispatch(saveILA({ saveData: {result:this.ila}, tabIndex: 0, update: this.doUpdate }));
    }
  }

  CaptureFormEventEval(data:any){
    
  }

  deleteIla(dialog: any) {
    const dialogRef = this.dialog.open(dialog,
      { width: '50%' },)
    dialogRef.afterClosed().subscribe((res) => {
      if (res === 'true') {
        this.deleteStore.pipe(takeUntil(this.destroy));
        this.deleteStore.dispatch(deleteILA({ tabIndex: this.selectedIndex }));
      }
    })
  }

  async stepNext() {
    this.stepper.next();
  }

  closeEditPreview() {
    this.previewContainer = undefined;
  }

  guideEditEvent(event: any) {
    this.previewContainer = event;
  }

  receiveCreateNew(d: any) {
    this.create_new = d;
    
    this.contorlCheckSimulation = true;
  }

  recieveNewEvent(d: any) {
    this.create_new = d;
    this.show_simulations = true;
    this.simulation_status = true;
  }

  recieveTitleText(d: any) {
    this.title_text = d;
    
  }

  closeGuideEditor() {
    this.previewContainer = undefined;
    if (this.stepper) this.stepper.selectedIndex = 3;
  }

  async closeILA(){
    if(this.selectedIndex === 6){   
      if(this.seqAndSchedule.allowSettings && this.seqAndSchedule.ilaEmpTqSettings && typeof this.seqAndSchedule.ilaEmpTqSettings.saveTQRelease === 'function'){
        await this.seqAndSchedule.ilaEmpTqSettings.saveTQRelease();
      } 
      if(this.seqAndSchedule.allowSettings && this.seqAndSchedule.ilaEmpEvalSettings && typeof this.seqAndSchedule.ilaEmpEvalSettings.saveInfoEvalRelease === 'function'){
        await this.seqAndSchedule.ilaEmpEvalSettings.saveInfoEvalRelease();
      } 
      if(this.seqAndSchedule.allowSettings && this.seqAndSchedule.ilaTestRelease && typeof this.seqAndSchedule.ilaTestRelease.saveInfoTestRelease === 'function'){
        await this.seqAndSchedule.ilaTestRelease.saveInfoTestRelease();
      } 
      await this.seqAndSchedule.saveSequenceAndScheduleDataAsync();
    }
    if(this.selectedIndex === 4){
      await this.traineeAssessment.updateEvalMethod();
    }
    this.router.navigate(['dnd/ila']);
  }

  deleteSpinner = false;
  async deleteILA(){
    this.deleteSpinner = true;
    await this.ilaService.delete(this.createILA.editILAId).then(async (_)=>{
      this.alert.successToast(await this.labelPipe.transform('ILA') + " Deleted Successfully");
      this.router.navigate(['dnd/ila']);
    }).finally(()=>{
      this.deleteSpinner = false;
    })
  }

  checkProviderIsNerc(event:any){
    this.providerIsNerc=event;  
  }

  onNickNameEmpty(event: { isChecked: boolean; isEmpty: boolean }): void {
  if (event.isChecked && event.isEmpty) {
    this.stepper.selectedIndex = 0;
    this.createILA?.validateNickName(event.isChecked);
  }  
  }

  isPublicCourseCheckboxChecked(event: boolean){
    this.isPublicCourseChecked = event;
  }
  
  async closeView() {
    await this.router.navigate(['dnd/ila']);
  }
  
}

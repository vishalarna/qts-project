import { BreakpointObserver } from '@angular/cdk/layout';
import { AfterViewInit, Component, Input, OnInit, ViewChild } from '@angular/core';
import {
  MatStep,
  MatStepper,
  StepperOrientation,
} from '@angular/material/stepper';
import { ActivatedRoute, Router } from '@angular/router';
import { DIFSurvey_CreateOptions } from '@models/DIFSurvey/DIFSurvey_CreateOptions';
import { Store } from '@ngrx/store';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { ApiDifSurveyService } from 'src/app/_Services/QTD/DifSurvey/api.difsurvey.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { sideBarClose } from 'src/app/_Statemanagement/action/state.menutoggle';
import { Location } from '@angular/common';
import { DIFSurvey_UpdateOptions } from '@models/DIFSurvey/DIFSurvey_UpdateOptions';
import { DIFSurveyVM } from '@models/DIFSurvey/DIFSurveyVM';
import { DifCreateSurveyComponent } from './dif-create-survey/dif-create-survey.component';
@Component({
  selector: 'app-dif-survey-create-wizard',
  templateUrl: './dif-survey-create-wizard.component.html',
  styleUrls: ['./dif-survey-create-wizard.component.scss'],
})
export class DifSurveyCreateWizardComponent implements OnInit,AfterViewInit  {
  @Input() inputDifId :string;
  @Input() handleLoad :() => void;
  @Input() handleContinueClick :(e) => void;
  stepperOrientation: Observable<StepperOrientation>;
  @ViewChild('stepper') stepper: MatStepper;
  @ViewChild('createSurvey') createSurvey: DifCreateSurveyComponent;
  selectedIndex: number = 0;
  currentStep: number = 1;
  isDIFSurveyFormValid:boolean = false;
  difSurveyVM:DIFSurveyVM;
  isStep1FormValid:boolean;

  constructor(
    private store: Store<{ toggle: string }>,
    private breakpointObserver: BreakpointObserver,
    private router: Router,
    private difSurveyService: ApiDifSurveyService,
    private alert: SweetAlertService,
    private location: Location
  ) {
    this.stepperOrientation = breakpointObserver
      .observe('(min-width: 800px)')
      .pipe(map(({ matches }) => (matches ? 'horizontal' : 'vertical')));
  }

  ngOnInit(): void {
    this.store.dispatch(sideBarClose());
    let urlSegment = this.router.url.split("/");
    if (urlSegment.includes('edit')) {
      this.inputDifId = urlSegment.reverse()[0];
    }
    this.loadAsync();
  }
  ngAfterViewInit(): void {
    var checkRedirect:any= this.location.getState();
      if(checkRedirect?.goToNext && this.inputDifId){
        if(this.stepper){
           setTimeout(() => this.stepper.next(), 1);
        }
      }
  }

  _handleLoad() {
    if (this.handleLoad &&typeof this.handleLoad === 'function') {
      this.handleLoad();
    }
  }

  _handlContinueClick(e) {
    if (this.handleContinueClick &&typeof this.handleContinueClick === 'function') {
      this.handleContinueClick(e);
    }
  }

  async loadAsync() {
    this.scrollToTop();
    await this.getDIFSurvey();
    this._handleLoad();
  }

  async getDIFSurvey(){
    if(this.inputDifId){
      await this.difSurveyService.getAsync(this.inputDifId).then(res=>{
        this.difSurveyVM=res;
        if(this.createSurvey && typeof this.createSurvey.setFormValues =='function'){
          this.createSurvey.inputDifSurveyVM=res;
          this.createSurvey.setFormValues();
        }
      });
    }
    else{
      this.difSurveyVM= new DIFSurveyVM();
    }
  }

  onSelectionChange(event: any) {
    let stepDifference= event.selectedIndex - event.previouslySelectedIndex;
    let matSteps:MatStep[] = this.stepper.steps.toArray();
    if(this.inputDifId){
      if(stepDifference>1){

        for(let i=event.previouslySelectedIndex+1;i<event.selectedIndex;i++){
          matSteps[i].interacted=true;
          matSteps[i].completed=true;
        }
      }

    }
    this.currentStep = event.selectedIndex + 1;
  }

  async continueClickAsync(e: any) {
    if(this.currentStep == 1){
      if(this.difSurveyVM?.id == null){
        var createOptions= new DIFSurvey_CreateOptions();
        createOptions = this.setOptionsData(createOptions);
        await this.difSurveyService.createAsync(createOptions).then(async res=>{
          await this.router.navigate(['analysis/dif-survey/edit/' + res.id],{ state: { goToNext: true }});
          this.alert.successToast("DIF Survey Saved Successfully");
        });
      }
      else{
        var updateOptions= new DIFSurvey_UpdateOptions();
        updateOptions = this.setOptionsData(updateOptions);
        await this.difSurveyService.updateAsync(this.inputDifId,updateOptions).then(res=>{
          this.difSurveyVM = res;
          this.alert.successToast("DIF Survey Updated Successfully");
          this.stepper.next();
        })
      }
    }
    else{
      this.stepper.next();
    }
    this.scrollToTop();
    this._handlContinueClick(e);
  }
  async publishClickAsync(){
    var publishOptions = new DIFSurvey_UpdateOptions();
    publishOptions = this.setOptionsData(publishOptions,true);
    await this.difSurveyService.updateAsync(this.inputDifId,publishOptions).then(res=>{
      this.difSurveyVM = res;
      this.alert.successToast("DIF Survey Published Successfully");
      this.router.navigate(['/analysis/dif-survey/overview'])
    })
  }
  
  setOptionsData(options:any,isPublish?:boolean){
    if(isPublish){
      options.isReleaseToEMP= this.difSurveyVM.releasedToEMP;
      options.isPublish=true;
    }
    else{
      options.title= this.difSurveyVM.surveyTitle;
      options.positionId= this.difSurveyVM.positionId;
      options.startDate=this.difSurveyVM.startDate;
      options.dueDate=this.difSurveyVM.dueDate;
      options.instructions = this.difSurveyVM.instructions;
    }
    return options;
  }

  goBack(){
    this.router.navigate(['/analysis/dif-survey/overview']);
  }

  getDIFSuveyFormStatus(isValid: boolean){
    this.isDIFSurveyFormValid = isValid;
  }

  private scrollToTop() {
    const stepperContainer = document.querySelector('.mat-sidenav-content');
    if (stepperContainer) {
      stepperContainer.scrollTop = 0;
    }
  }
}

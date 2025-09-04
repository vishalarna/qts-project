import { BreakpointObserver } from '@angular/cdk/layout';
import { StepperOrientation } from '@angular/cdk/stepper';
import { Component, Input, OnInit, ViewChild } from '@angular/core';
import { MatStepper } from '@angular/material/stepper';
import { ActivatedRoute, Router } from '@angular/router';
import { Store } from '@ngrx/store';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { ILATraineeEvaluationCreateOptions } from 'src/app/_DtoModels/ILATraineeEvaluation/ILATraineeEvaluationCreateOptions';
import { Test } from 'src/app/_DtoModels/Test/Test';
import { TestCreateOptions } from 'src/app/_DtoModels/Test/TestCreateOptions';
import { IlaTraineeEvaluationService } from 'src/app/_Services/QTD/ila-trainee-evaluation.service';
import { TestsService } from 'src/app/_Services/QTD/tests.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { sideBarClose } from 'src/app/_Statemanagement/action/state.menutoggle';
import { CreateTestInformationComponent } from './test-wizard-components/create-test-information/create-test-information.component';
import {Location} from '@angular/common';
import { SequenceTestQuestionsComponent } from './test-wizard-components/sequence-test-questions/sequence-test-questions.component';
import { ImportTestQuestionsComponent } from './test-wizard-components/import-test-questions/import-test-questions.component';
import { AddNewTestQuestionsComponent } from './test-wizard-components/add-new-test-questions/add-new-test-questions.component';
import { SubSink } from 'subsink';
import { LabelReplacementPipe } from 'src/app/_Pipes/label-replacement.pipe';

@Component({
  selector: 'app-test-create-wizard',
  templateUrl: './test-create-wizard.component.html',
  styleUrls: ['./test-create-wizard.component.scss']
})
export class TestCreateWizardComponent implements OnInit {

  isTestCreated = false;
  showSpinner = false;
  @Input() formStatus: string = 'INVALID';
  stepperOrientation: Observable<StepperOrientation>;
  previewContainer: string | undefined;
  enableNavigation = false; // Should be false
  isDraft = true;
  selectedIndex: number = 0;
  @ViewChild('stepper') stepper: MatStepper;
  testData = new TestCreateOptions();
  evaluationData = new ILATraineeEvaluationCreateOptions();
  mode: 'add' | 'copy' | 'edit' = 'add';
  testId:any = '';
  newMode: 'copy' | 'edit' | 'none' = 'none';
  subscription = new SubSink();
  functionCall:any;
  linear:boolean = true;

  @ViewChild('createInfo') createInfo !: CreateTestInformationComponent;
  @ViewChild('importTestQuestion') importTestQuestion !: ImportTestQuestionsComponent;
  @ViewChild('sequenceTestQues') sequenceTestQues!: SequenceTestQuestionsComponent;
  @ViewChild('addNewTestQuestion') addNewTestQuestion!: AddNewTestQuestionsComponent;

  constructor(private router: Router,
    private breakpointObserver: BreakpointObserver,
    private alert: SweetAlertService,
    private testService: TestsService,
    private ilaTraineeEvalService: IlaTraineeEvaluationService,
    private store: Store<any>,
    private route: ActivatedRoute,
    private location: Location,
    private labelPipe: LabelReplacementPipe) {
    this.stepperOrientation = breakpointObserver
      .observe('(min-width: 800px)')
      .pipe(map(({ matches }) => (matches ? 'horizontal' : 'vertical')));
  }

  ngOnInit(): void {
    this.store.dispatch(sideBarClose());
    var modeFromRoute = this.router.url.split('/');
    this.selectMode(modeFromRoute);
  }

  ngAfterViewInit() {
    this.subscription.sink = this.route.params.subscribe((res: any) => {
      if(res.id !== undefined)
      {
        this.testId = res.id;
        this.enableNavigation = true;
      }

    });

    var local = localStorage.getItem('stepper');
    if(local){
      
      this.linear = false;
      this.enableNavigation = true;
      setTimeout(() => this.stepNext(), 1);
      
    }else{
      this.linear = true;
      this.enableNavigation = false;
    }
  }

  selectMode(modeFromRoute: string[]) {
    switch (modeFromRoute[3]) {
      case 'create':
        this.mode = 'add';
        break;
      case 'edit':
        this.mode = 'edit';
        this.testId = modeFromRoute[4];
        break;
      case 'copy':
        this.mode = 'copy';
        this.testId = modeFromRoute[4];
        break;
    }
  }

  async goBack() {
    this.router.navigate(['dnd/tests/overview']);
  }

  async continueClicked() {
    if (this.selectedIndex === 0) {
      this.showSpinner = true;
      this.isDraft = true;
      // await this.testService.create(this.testData)
      //   .then((res: Test) => {
      //     
      //     //this.saveTestItemLinks(res.id);
      //     this.saveILATraineeEvaluation(res.id, 'Written');

      //   })
      this.createInfo.saveInfo();
    }
    else if(this.selectedIndex === 3)
    {
      this.sequenceTestQues.UpdateSequence();
    }
    else {
      this.isDraft = false;
      this.stepNext();
    }

  }

  updateClicked(){
    this.showSpinner = true;
    if(this.selectedIndex === 0){
      this.createInfo.updateData();

    }
    else if(this.selectedIndex === 3)
    {
      this.sequenceTestQues.UpdateSequence();
    }

    this.enableNavigation = true;
    setTimeout(() => this.stepNext(), 1);
    this.showSpinner = false;
  }

  async saveILATraineeEvaluation(testId: any, name: string) {
    switch (name) {
      case 'Written':
        this.showSpinner = true;
        this.evaluationData.testId = testId;
        if (this.evaluationData.testTimeLimitHours === null) {
          this.evaluationData.testTimeLimitHours = 0;
        }
        if (this.evaluationData.testTimeLimitMinutes === null) {
          this.evaluationData.testTimeLimitMinutes = 0;
        }
        this.ilaTraineeEvalService.create(this.evaluationData).then((res: any) => {
          this.enableNavigation = true;
          this.selectedIndex = this.selectedIndex + 1;
          this.alert.successToast("Test Saved Successfully");

        }).catch((err: any) => {
          this.alert.errorToast("Error Saving Test Data");
        }).finally(() => {
          this.showSpinner = false;
        })
        break;
    }
  }

  CreateTestData(event: TestCreateOptions) {
    this.testData = new TestCreateOptions();
    this.testData.testTitle = event.testTitle;
    this.testData.testStatusId = event.testStatusId;
  }

  CreateEvaluationData(event: ILATraineeEvaluationCreateOptions) {
    this.evaluationData = new ILATraineeEvaluationCreateOptions();
    this.evaluationData.evaluationTypeId = 'xY';
    this.evaluationData.ilaId = event.ilaId;
    this.evaluationData.testId = event.testId;
    this.evaluationData.testInstruction = event.testInstruction
    this.evaluationData.testTimeLimitHours = event.testTimeLimitHours;
    this.evaluationData.testTimeLimitMinutes = event.testTimeLimitMinutes
    this.evaluationData.testTitle = event.testTitle
    this.evaluationData.testTypeId = event.testTypeId
    this.evaluationData.trainingEvaluationMethod = event.trainingEvaluationMethod;

  }

  onSelectionChange(event: any) {
    this.selectedIndex = event.selectedIndex;
    if (this.selectedIndex === 0) {
      this.isDraft = true;
    }
    else if(this.selectedIndex == 1)
    {
      //this.importTestQuestion;
      this.isDraft = false;
    }
    else if(this.selectedIndex == 2)
    {
      //this.addNewTestQuestion.getTestItems();
      this.isDraft = false;
    }
    else if(this.selectedIndex == 3)
    {
      this.sequenceTestQues.getTestItems();
      this.isDraft = false;
    }
    else if (this.selectedIndex === 4) {
      this.router.navigate([`/dnd/tests/publish/${this.testId}`]);
      this.isDraft = false;
    }


  }

  async detectCreateTestInformationStatus(event: any) {
    switch (event) {
      case "VALID":
        this.formStatus = "VALID";
        this.showSpinner = false;
        //this.doUpdate = true;

        break;
      case "FAILED":
        this.showSpinner = false;
        this.formStatus = "VALID";
        this.enableNavigation = false;
        break;
      case "UPDATED":
        this.showSpinner = false;
        this.formStatus = "INVALID";
        this.enableNavigation = true;
        this.stepper.next();
        
        this.alert.successToast( await this.labelPipe.transform('ILA') +" Data Updated");
        break;
      case "DELETE":
        //this.doUpdate = false;
        this.enableNavigation = false;
        this.stepper.reset();
        break;
      default:
        this.formStatus = event;
        break;
    }
  }

  async stepNext() {
    this.stepper.next();
    localStorage.removeItem('stepper');
  }

  changeStep(){
    this.showSpinner = false;
    this.isTestCreated = true;
    this.testId = this.createInfo.Id;
    this.enableNavigation = true;

    this.location.go('/dnd/tests/edit/'+ this.testId);

    setTimeout(() => this.stepNext(), 1);

  }

  setNotSaved(){
    this.showSpinner = false;
  }

}

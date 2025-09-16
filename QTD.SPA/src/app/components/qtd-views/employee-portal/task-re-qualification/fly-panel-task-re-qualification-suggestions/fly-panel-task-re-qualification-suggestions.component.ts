import { query, style, group, animate, transition, trigger } from '@angular/animations';
import { BreakpointObserver } from '@angular/cdk/layout';
import { DatePipe } from '@angular/common';
import { ChangeDetectorRef, Component, OnInit, ViewChild, ViewContainerRef } from '@angular/core';
import { MatLegacyDialog as MatDialog } from '@angular/material/legacy-dialog';
import { MatStepper, StepperOrientation } from '@angular/material/stepper';
import { ActivatedRoute, Router } from '@angular/router';
import { Store } from '@ngrx/store';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { TaskReQualificationCreateOption, TaskReQualificationQuestionsCreateOption, TaskReQualificationSignOffOption, TaskReQualificationStepsCreateOption } from 'src/app/_DtoModels/taskRequalfication/TaskReQualificationCreateOption';
import { LabelReplacementPipe } from 'src/app/_Pipes/label-replacement.pipe';
import { EvaluationMethodService } from 'src/app/_Services/QTD/evaluation-method.service';
import { TasksService } from 'src/app/_Services/QTD/tasks.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { sideBarClose, sideBarOpen } from 'src/app/_Statemanagement/action/state.menutoggle';
import * as ckcustomBuild from 'src/app/ckcustomBuild/build/ckeditor.js';

const left = [
  query(':enter, :leave', style({ position: 'fixed', width: '50%', display: 'flex' }), { optional: true }),
  group([
    query(':enter', [style({ transform: 'translateX(-100%)' }), animate('.1s ease-out', style({ transform: 'translateX(0%)', opacity: 1 }))], {
      optional: true,
    }),
    query(':leave', [style({ transform: 'translateX(0%)' }), animate('.1s ease-out', style({ transform: 'translateX(100%)', opacity: 0 }))], {
      optional: true,
    }),
  ]),
];

const right = [
  query(':enter, :leave', style({ position: 'fixed', width: '50%', display: 'flex' }), { optional: true }),
  group([
    query(':enter', [style({ transform: 'translateX(100%)' }), animate('.1s ease-in', style({ transform: 'translateX(0%)', opacity: 1 }))], {
      optional: true,
    }),
    query(':leave', [style({ transform: 'translateX(0%)' }), animate('.1s ease-in', style({ transform: 'translateX(-100%)', opacity: 0 }))], {
      optional: true,
    }),
  ]),
];
@Component({
  selector: 'app-fly-panel-task-re-qualification-suggestions',
  templateUrl: './fly-panel-task-re-qualification-suggestions.component.html',
  styleUrls: ['./fly-panel-task-re-qualification-suggestions.component.scss'],
  animations: [
    trigger('animSlider', [
      transition(':increment', right),
      transition(':decrement', left),
    ]),
  ],
})
export class FlyPanelTaskReQualificationSuggestionsComponent implements OnInit {
  @ViewChild('stepper') stepper: MatStepper;
  stepperOrientation: Observable<StepperOrientation>;
  currentIndex: number = 0;
  counter: number = 0;
  tempIds: string = '';
  taskId: string = '';
  empId: string = '';
  exitDescription: string;
  saveDescription: string;
  closeDescription:string='';
  goBackDescription:string='';
  qualificationId: string = '';
  editor = ckcustomBuild;
  taskSuggestionsObjList: any;
  taskStepsObjList: any;
  taskQuestionsObjList: any;
  taskSignOffObjList: any={};
  evalMethodList:any[]=[];
  isLoading: boolean = false;
  // Suggestions Model start
  isSuggestionCompleted: boolean = false;
  model = new TaskReQualificationCreateOption();
  datePipe = new DatePipe('en-us');
  // Suggestions Model end
  taskLetter:any;
  taskNumberURL:any;
  modelSteps = new TaskReQualificationStepsCreateOption();
  modelQuestion = new TaskReQualificationQuestionsCreateOption();
  modelSingOff= new TaskReQualificationSignOffOption();
  tempTaskUrl:string='';
  isOpen:boolean=false;
  allTaskStepsCompleted: boolean;
  comments:string='';
  skillId: string;
  skillNumberURL:any;
  skillQualificationId:string;
  skillNumber:string='';
  checkType:string;
   allSkillStepsCompleted: boolean;
  constructor(
    private store: Store<{ toggle: string }>,
    private _router: Router,
    public dialog: MatDialog,
    public changeDetector: ChangeDetectorRef,
    public breakpointObserver: BreakpointObserver,
    public flyPanelService: FlyInPanelService,
    private vcf: ViewContainerRef,
    private route: ActivatedRoute,
    private taskService: TasksService,
    private evalMethodService: EvaluationMethodService,
    private labelPipe: LabelReplacementPipe,
  ) {
    this.stepperOrientation = breakpointObserver
      .observe('(min-width: 800px)')
      .pipe(map(({ matches }) => (matches ? 'horizontal' : 'vertical')));
  }

  async ngOnInit(): Promise<void> {
    this.store.dispatch(sideBarClose());
    this.route.params.subscribe((params: any) => {
    
    if (params.hasOwnProperty('id')) {
      this.tempIds = params['id'];
      const parts = String(this.tempIds).split('.');
       this.checkType = parts[parts.length - 1];

      if (this.checkType == 'eo') {
        this.skillId = String(this.tempIds).split('-')[1].replace('§_', '').split('.')[0];
        this.empId = parts[1];
        this.skillNumberURL = parts.slice(2, -1).join('.'); 

        this.skillQualificationId = String(this.tempIds).split('-')[0];
        this.getSuggestionSQBit();
        this.getQuestionSQBit();
        this.getSQSuggestion();
      } 
      if(this.checkType == 'task'){
        this.taskLetter = String(this.tempIds).split('-')[1].split('_')[0];
        this.taskId = String(this.tempIds).split('-')[1].replace('§_', '').split('.')[0];
        this.empId = parts[1];
        this.taskNumberURL = parts.slice(2).join('.');
        this.qualificationId = String(this.tempIds).split('-')[0];
        this.getSuggestionBit();
        this.getQuestionBit();
        this.getTaskSuggestion();
      }
    }
  });
  this.exitDescription = `You are selecting to Exit the ` + await this.transformTitle('Procedure') + ` Review without submitting your responses. Your progress will be saved.`;
  this.saveDescription = `You are choosing to submit and finalize your ` + await this.transformTitle('Procedure') + ` Review response.`;
  }

  goToTaskDetail() {
    this.tempTaskUrl= this.taskId + '-' + this.taskLetter + '_' + this.taskNumberURL+ "-"+ this.qualificationId;
    this.isOpen=true;
  }

  //if the QTD Admin did not enable the Show Task Specific Suggestions option when setting up the Task Qualification, then the first page I should see is the OJT for the Task
  answer:any;
  getSuggestionBit(){
    this.taskService.getSuggestionBit(this.qualificationId).then((res)=>{
      this.answer = res;
    });
  }

  answer1:any;
  getQuestionBit(){
    this.taskService.getQuestionBit(this.qualificationId).then((res)=>{
      this.answer1 = res;
    });
  }

  getEvalMethod() {
    this.evalMethodService.getAll().then((res) => {
      this.evalMethodList = res;
    }).catch((res: any) => {
    })
  }

  getSuggestionSQBit(){
    this.taskService.getSuggestionSQBit(this.skillQualificationId).then((res)=>{
      this.answer = res;
    });
  }

  getQuestionSQBit(){
    this.taskService.getQuestionSQBit(this.skillQualificationId).then((res)=>{
      this.answer1 = res;
    });
  }
  async selectedChanged(event: any) {
    this.isLoading=true;
    this.saveCurrentView(event.previouslySelectedIndex);
    this.currentIndex = event.selectedIndex;
    this.isLoading=true;
    switch(event.selectedIndex){
      case 0:
        if(this.checkType == 'task'){
          this.getTaskSuggestion();
        }
        if(this.checkType == 'eo'){
           this.getSQSuggestion();
        }
        break;
      case 1:
        if(this.checkType == 'task'){
          this.getTaskSteps();
        }
        if(this.checkType == 'eo'){
          this.getSQSteps();
        }
        break;
      case 2:
         if(this.checkType == 'task'){
          this.getTaskQuestions();
         }
           if(this.checkType == 'eo'){
             this.getSQQuestions();
           }
        break;
      case 3:
        if(this.checkType == 'task'){
          this.getTaskSignOff();
         }
         if(this.checkType == 'eo'){
             this.getSQSignOff();
          }
        this.getEvalMethod();
        //this.getTaskQualificationEmpSteps();
        break;
    }
  }

  saveCurrentView(currentViewIndex:number){
    switch(currentViewIndex){
      case 0:
        if(this.answer){
          let updateSuggestionItem = this.taskSuggestionsObjList?.suggestionList[this.counter];
          if(updateSuggestionItem){
            this.model.suggestionList.push({ isCompleted: updateSuggestionItem.isCompleted, comments: updateSuggestionItem.comments, suggesntionDescription: updateSuggestionItem.suggesntionDescription, suggestionId: updateSuggestionItem.suggestionId })
            this.updateTaskandSkillComments();
          }
        }
        break;
      case 1:
        let updateStepItem = this.taskStepsObjList?.stepsList[this.counter];
        if(updateStepItem?.isCompleted != null){
          this.modelSteps.stepsList.push({ isCompleted: updateStepItem.isCompleted, comments: updateStepItem.comments, stepDescription: updateStepItem.stepDescription, stepId: updateStepItem.stepId })
          this.updateTaskAndSkillStepsComments();
        }
        break;
      case 2:
        if(this.answer1){
          let updateQuestionItem = this.taskQuestionsObjList?.quesionAnswerList[this.counter];
          if(updateQuestionItem){
            this.modelQuestion.quesionAnswerList.push({ isCompleted: updateQuestionItem.isCompleted, comments: updateQuestionItem.comments, questionDescription: updateQuestionItem.stepDescription, questionId: updateQuestionItem.questionId,answer:updateQuestionItem.answer})
            this.updateTaskAndSkillQuestionsComments();
          }
        }
        break;
      case 3:
        if(this.taskSignOffObjList){
         this.fillDataSingOff(this.taskSignOffObjList,false);
        }
        break;
    }
  }

  updateTaskandSkillComments() {
    this.model.taskId = this.taskSuggestionsObjList?.taskId;
    this.model.taskQualificationId = this.taskSuggestionsObjList?.taskQualificationId;
    this.model.taskDescription = this.taskSuggestionsObjList?.taskDescription;
    this.model.traineeId = this.empId;
    this.model.skillDescription =this.taskSuggestionsObjList?.skillDescription;
    this.model.skillId =this.taskSuggestionsObjList?.skillId;
    this.model.skillQualificationId =this.taskSuggestionsObjList?.skillQualificationId;
    this.taskService.saveTaskSuggestionData(this.model).then((res) => {
      this.isLoading=false;
      this.model=new TaskReQualificationCreateOption();
    }).catch((res: any) => {

    })
  }
  taskNumber:string='';
  getTaskSuggestion() {
    this.taskService.getSuggestionData(this.qualificationId, this.taskId, this.empId).then((res) => {
      this.taskSuggestionsObjList = res;
      this.taskNumber=this.taskSuggestionsObjList.concateNatedTaskNumber;
      this.isLoading=false;
      this.counter=0;
    }).catch((res: any) => {

    })
  }

  getSQSuggestion() {
    this.taskService.getSuggestionSQData(this.skillQualificationId, this.skillId, this.empId).then((res) => {
      this.taskSuggestionsObjList = res;
      this.skillNumber=this.taskSuggestionsObjList.concateNatedSkillNumber;
      this.isLoading=false;
      this.counter=0;
    }).catch((res: any) => {

    })
  }

  getTaskSteps() {
    this.taskService.getStepsData(this.qualificationId, this.taskId, this.empId).then((res) => {
      this.taskStepsObjList = res;
      this.isLoading=false;
      this.counter=0;

    }).catch((res: any) => {
    })
  }

  getSQSteps() {
    this.taskService.getStepsSQData(this.skillQualificationId, this.skillId, this.empId).then((res) => {
      this.taskStepsObjList = res;
      this.isLoading=false;
      this.counter=0;

    }).catch((res: any) => {
    })
  }

  fillDataSteps(row) {
    if (this.currentIndex === 1) {
      let updateCount=this.counter
      if(updateCount === this.taskStepsObjList?.stepsList.length-1){
        this.stepper.next();
        this.currentIndex=2;
      }
      else{
        this.modelSteps.stepsList.push({ isCompleted: row.isCompleted, comments: row.comments, stepDescription: row.stepDescription, stepId: row.stepId })
        this.onNext();
        this.updateTaskAndSkillStepsComments();
      }
    }
  }

  updateTaskAndSkillStepsComments() {
    this.modelSteps.taskId = this.taskStepsObjList?.taskId;
    this.modelSteps.taskQualificationId = this.taskStepsObjList?.taskQualificationId;
    this.modelSteps.taskDescription = this.taskStepsObjList?.taskDescription;
    this.modelSteps.traineeId = this.empId;
    this.modelSteps.skillDescription = this.taskStepsObjList?.skillDescription;
    this.modelSteps.skillId = this.taskStepsObjList?.skillId;
    this.modelSteps.skillQualificationId =this.taskStepsObjList?.skillQualificationId;
    this.taskService.saveStepsData(this.modelSteps).then((res) => {
      this.isLoading=false;
      this.modelSteps=new TaskReQualificationStepsCreateOption();
    }).catch((res: any) => {

    })
  }

  getTaskQuestions() {

    this.taskService.getQuestionData(this.qualificationId, this.taskId, this.empId).then((res) => {
      this.taskQuestionsObjList = res;
      this.counter=0;
      this.isLoading=false;

    }).catch((res: any) => {
    })
  }

  getSQQuestions() {
    this.taskService.getQuestionSQData(this.skillQualificationId, this.skillId, this.empId).then((res) => {
      this.taskQuestionsObjList = res;
      this.counter=0;
      this.isLoading=false;

    }).catch((res: any) => {
    })
  }

  fillDataQuestion(row) {
    if (this.currentIndex === 2) {
      let updateCount= this.counter;
      if(updateCount === this.taskQuestionsObjList?.quesionAnswerList.length -1){
        this.stepper.next()
        this.currentIndex=3;
      }
      else{
        this.modelQuestion.quesionAnswerList.push({ isCompleted: row.isCompleted, comments: row.comments, questionDescription: row.stepDescription, questionId: row.questionId,answer:row.answer})
        this.onNext();
        this.updateTaskAndSkillQuestionsComments();
      }
    }
  }
  
  
  updateTaskAndSkillQuestionsComments() {
    this.modelQuestion.taskId = this.taskQuestionsObjList?.taskId;
    this.modelQuestion.taskQualificationId = this.taskQuestionsObjList?.taskQualificationId;
    this.modelQuestion.taskDescription = this.taskQuestionsObjList?.taskDescription;
    this.modelQuestion.traineeId = this.empId;
    this.modelQuestion.skillQualificationId =this.taskStepsObjList?.skillQualificationId;
    this.modelQuestion.skillId =this.taskQuestionsObjList?.skillId;
     this.modelQuestion.skillDescription =this.taskQuestionsObjList?.skillDescription;
    this.taskService.saveQuestionData(this.modelQuestion).then((res) => {
      this.isLoading=false;
      this.modelQuestion=new TaskReQualificationQuestionsCreateOption();
    }).catch((res: any) => {

    })
  }

  getTaskSignOff() {
    const stepsList = this.taskStepsObjList?.stepsList;
    this.allTaskStepsCompleted = Array.isArray(stepsList) && stepsList.every((step: any) => step.isCompleted === true);
    this.taskService.getTaskSignOffData(this.qualificationId, this.taskId, this.empId).then((res) => {
      this.taskSignOffObjList = res;
      this.isLoading=false;
      if(this.taskSignOffObjList!.taskQualificationDate === null)
      {
        this.taskSignOffObjList.taskQualificationDate = this.datePipe.transform(Date.now(), "yyyy-MM-dd");
      }
      else{
        this.taskSignOffObjList.taskQualificationDate = this.datePipe.transform(this.taskSignOffObjList.taskQualificationDate, "yyyy-MM-dd");
      }
      this.counter=0;
      this.comments = this.taskSignOffObjList?.comments ?? '';

    }).catch((res: any) => {
    })
  }

  getSQSignOff() {
    const stepsList = this.taskStepsObjList?.stepsList;
    this.allSkillStepsCompleted = Array.isArray(stepsList) && stepsList.every((step: any) => step.isCompleted === true);
    this.taskService.getSQSignOffData(this.skillQualificationId, this.skillId, this.empId).then((res) => {
      this.taskSignOffObjList = res;
      this.isLoading=false;
      if(this.taskSignOffObjList!.skilQualificationDate === null)
      {
        this.taskSignOffObjList.skilQualificationDate = this.datePipe.transform(Date.now(), "yyyy-MM-dd");
      }
      else{
        this.taskSignOffObjList.skilQualificationDate = this.datePipe.transform(this.taskSignOffObjList.skilQualificationDate, "yyyy-MM-dd");
      }
      this.counter=0;
      this.comments = this.taskSignOffObjList?.comments ?? '';
    }).catch((res: any) => {
    })
  }

  fillDataSingOff(row,isFormSubmitted:boolean) {
    if (this.currentIndex === 3) {
      this.modelSingOff.comments=this.comments;
      this.modelSingOff.evaluationMethodId=row.evaluationMethodId;
      this.modelSingOff.evaluatorId=row.evaluatorId;
      this.modelSingOff.isCriteriaMet=row.isCriteriaMet;
      this.modelSingOff.taskQualificationDate=row.taskQualificationDate;
      this.modelSingOff.taskQualificationId=row.taskQualificationId;
      this.modelSingOff.traineeId = this.empId;
      this.modelSingOff.isFormSubmitted= isFormSubmitted;
      this.modelSingOff.isEvaluatorSignOff= row.isEvaluatorSignOff;
      this.modelSingOff.isTraineeSignOff= row.isTraineeSignOff;
      this.modelSingOff.skillQualificationDate=row.skillQualificationDate;
      this.modelSingOff.skillQualificationId =row.skillQualificationId;
      this.updateTaskAndSkillSignOff();
    }
  }

  updateTaskAndSkillSignOff() {
    if(this.checkType == 'eo'){
        this.taskService.saveSQSignOffData(this.modelSingOff).then((res) => {
      this.isLoading=false;
      if(this.modelSingOff.isFormSubmitted){
        this.store.dispatch(sideBarOpen());
        this._router.navigate(['emp/task-re-qualification/overview'], { queryParams: { isRedirectToEval: 'true' } });
      }
      this.modelSingOff=new TaskReQualificationSignOffOption();
      }).catch((res: any) => {

       })
    }
    if(this.checkType == 'task'){
      this.taskService.saveSingOffData(this.modelSingOff).then((res) => {
        this.isLoading=false;
        if(this.modelSingOff.isFormSubmitted){
          this.store.dispatch(sideBarOpen());
          this._router.navigate(['emp/task-re-qualification/overview'], { queryParams: { isRedirectToEval: 'true' } });
        }
        this.modelSingOff=new TaskReQualificationSignOffOption();
      }).catch((res: any) => {

      })
   }
  }
  onNext() {
      this.counter++;
  }

  fillData(row) {
    if (this.currentIndex === 0) {
      let updateCount=this.counter
      if(updateCount === this.taskSuggestionsObjList?.suggestionList.length-1){
        this.stepper.next();
        this.currentIndex=1;
      }
      else{
        this.model.suggestionList.push({ isCompleted: row.isCompleted, comments: row.comments, suggesntionDescription: row.suggesntionDescription, suggestionId: row.suggestionId })
        this.onNext();
        this.updateTaskandSkillComments();
      }
    }
  }

  async transformTitle(title: string) {
    const labelName = await this.labelPipe.transform(title);
    return labelName;
  }

  submitDialog(templateRef,data) {

    this.saveDescription = this.checkType?.trim().toLowerCase() == 'eo'? `You are selecting to submit the  Skill Qualification for ${this.taskSignOffObjList!.traineeName} for Skill ${this?.skillNumberURL}`
                            : `You are selecting to submit the Task / Skill Qualification for ${this.taskSignOffObjList!.traineeName} for Task ${this?.taskNumberURL}`;

     const dialogRef = this.dialog.open(templateRef, {
       width: '600px',
       height: 'auto',
       hasBackdrop: true,
       disableClose: true,
     });
  }
  onPrevious() {

    if (this.counter > 0) {
      this.counter--;
    }
  }
  async goBack() {
    this.saveCurrentView(this.currentIndex);
    this.store.dispatch(sideBarOpen());
    this._router.navigate(['emp/task-re-qualification/overview'], { queryParams: { isRedirectToEval: 'true' } });
  }

  async goBackDialog(templateRef) {
    this.goBackDescription='You are selecting to end the Task / Skill (Re)Qualification. All responses will be saved, and the Task / Skill (Re)Qualification will not be submitted to QTD.';
    const dialogRef = this.dialog.open(templateRef, {
      width: '600px',
      height: 'auto',
      hasBackdrop: true,
      disableClose: true,
    });
  }

  async closeDialog(templateRef) {

    this.closeDescription=`You are selecting to end the ` + await this.transformTitle('Task') +` / Skill  (Re)Qualification. All responses will be saved, and the ` + await this.transformTitle('Task') +` / Skill   (Re)Qualification will not be submitted to QTD.`;
     const dialogRef = this.dialog.open(templateRef, {
       width: '600px',
       height: 'auto',
       hasBackdrop: true,
       disableClose: true,
     });
  }

}

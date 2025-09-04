import { CdkDragDrop, moveItemInArray } from '@angular/cdk/drag-drop';
import { TemplatePortal } from '@angular/cdk/portal';
import { DatePipe } from '@angular/common';
import { AfterViewInit, Component, Input, OnDestroy, OnInit, ViewChild, ViewContainerRef, ViewEncapsulation } from '@angular/core';
import { MatLegacyDialog as MatDialog } from '@angular/material/legacy-dialog';
import { MatLegacyPaginator as MatPaginator } from '@angular/material/legacy-paginator';
import { MatLegacyTableDataSource as MatTableDataSource } from '@angular/material/legacy-table';
import { MatLegacyTab as MatTab } from '@angular/material/legacy-tabs';
import { ActivatedRoute, Router } from '@angular/router';
import { Task } from 'src/app/_DtoModels/Task/Task';
import { TaskOptions } from 'src/app/_DtoModels/Task/TaskOptions';
import { Task_Question } from 'src/app/_DtoModels/Task_Question/Task_Question';
import { Task_QuestionNumberOptions } from 'src/app/_DtoModels/Task_Question/Task_QuestionNumberOptions';
import { Task_Step } from 'src/app/_DtoModels/Task_Step/Task_Step';
import { Task_StepNumberOptions } from 'src/app/_DtoModels/Task_Step/Task_StepNumberOptions';
import { Task_Suggestion } from 'src/app/_DtoModels/Task_Suggestion/Task_Suggestion';
import { Task_SuggestionNumberOptions } from 'src/app/_DtoModels/Task_Suggestion/Task_SuggestionNumberOptions';
import { Tool } from 'src/app/_DtoModels/Tool/Tool';
import { TrainingGroup } from 'src/app/_DtoModels/TrainingGroup/TrainingGroup';
import { LabelReplacementPipe } from 'src/app/_Pipes/label-replacement.pipe';
import { TasksService } from 'src/app/_Services/QTD/tasks.service';
import { DataBroadcastService } from 'src/app/_Shared/services/DataBroadcast.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { SubSink } from 'subsink';

@Component({
  selector: 'app-task-ojt-guide',
  templateUrl: './task-ojt-guide.component.html',
  styleUrls: ['./task-ojt-guide.component.scss'],
  encapsulation: ViewEncapsulation.None,
})
export class TaskOjtGuideComponent implements OnInit, OnDestroy, AfterViewInit {
  @Input() isActive = true;
  @Input() isEMPView = false;

  @Input() taskNumber:any;
  steps: Task_Step[];
  datePipe = new DatePipe('en-us');
  displayColumns = ['order','number', 'question', 'answer', 'buttons'];
  suggestionDisplay = ['order','number','description', 'buttons'];
  questions: Task_Question[] = [];
  suggestions: Task_Suggestion[] = [];
  trainingGroup: TrainingGroup[] = [];
  Datasource: MatTableDataSource<Task_Question> = new MatTableDataSource();
  SuggestionSource: MatTableDataSource<Task_Suggestion> = new MatTableDataSource();
  task: any;
  collapseImage: boolean[] = [];
  tools: Tool[] = [];
  tags: any[] = [];
  subscription = new SubSink();
  taskId = "";
  images: any[] = [];
  originalNumbers: any[] = [];
  savingOrder = false;
  selectedStep: Task_Step = new Task_Step();
  dialogTitle = "";
  dialogDesc = "";

  isStep = false;
  isSuggestion = false;
  stepDeleteId = "";
  questionDeleteId = "";
  suggestionDeleteId = "";
  taskSuggestion: Task_Suggestion | undefined;

  deleteDisable = false;
  taskQuestion: Task_Question | undefined;
  originalQANumbers:number[] = [];
  originalSuggNumbers:number[] = [];
  popupCheck:boolean=false;
  showTaskSuggestionForEmp:boolean= false;
  showTaskQyestionsForEmp:boolean= false;

  @ViewChild('suggestionPaginator') suggestionPaginator: MatPaginator;
  @ViewChild('questionPaginator') questionPaginator: MatPaginator;

  constructor(
    public flypanelSrvc: FlyInPanelService,
    private vcf: ViewContainerRef,
    private taskService: TasksService,
    private route: ActivatedRoute,
    public dialog: MatDialog,
    private alert: SweetAlertService,
    private dataBraodcastService : DataBroadcastService,
    private router : Router,
    private labelPipe: LabelReplacementPipe
  ) { }

  ngOnInit(): void {

  }

  ngAfterViewInit(): void {
    this.subscription.sink = this.route.params.subscribe(async (res: any) => {
      if(this.router.url.includes('task-suggestions')){
       this.taskId = String(res.id).split('-')[1].replace('§_', '').split('.')[0];;
      }else{
        this.taskId = String(res.id).split('-')[0];
      }
      this.readyData();
    })

    this.subscription.sink = this.dataBraodcastService.refreshMeta.subscribe(()=>{
      this.readyData();
    })
  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }

  async transformTitle(title: string) {
    const labelName = await this.labelPipe.transform(title);
    return labelName;
  } 

  async readyData() {
    this.task = await this.taskService.getAllData(this.taskId);
    await this.getToolsData();
    await this.getQAData();
    await this.getSuggestionsData();
    await this.getTrainingGroupData();
    await this.closeAndRefreshSteps();

    this.showTaskSuggestionForEmp = this.task.taskQualifications[0]?.tqEmpSetting?.showTaskSuggestions;
    this.showTaskQyestionsForEmp = this.task.taskQualifications[0]?.tqEmpSetting?.showTaskQuestions;
    //
  }

  async getTrainingGroupData() {
    this.trainingGroup = [];
    this.trainingGroup = await this.taskService.getLinkedTrainingGroups(this.taskId);
  }

  openFlyPanel(templateRef: any) {

    const portal = new TemplatePortal(templateRef, this.vcf);
    this.flypanelSrvc.open(portal);
  }

  openStepFlyPanel(templateRef: any, step: Task_Step | undefined, index: any) {
    if (step !== undefined) {
      this.selectedStep = Object.assign(this.selectedStep, step ?? new Task_Step());
      this.selectedStep.description = this.selectedStep.description.concat(this.images[index] ?? "");
    }
    else {
      this.selectedStep = new Task_Step();
    }

    const portal = new TemplatePortal(templateRef, this.vcf);
    this.flypanelSrvc.open(portal);
  }

  async deleteStep(templateRef: any, stepId: any) {
    this.stepDeleteId = stepId;
    this.isStep = true;
    this.isSuggestion = false;
    this.dialogTitle = "Delete " + await this.transformTitle('Task') + " Step";

    this.dialogDesc = `This ` + await this.transformTitle('Task') +` is currently available for ` + await this.transformTitle('Task') +` Qualification in EMP, are you sure you wish to delete this Step?`;
    const dialogRef = this.dialog.open(templateRef, {
      width: '600px',
      height: 'auto',
      hasBackdrop: true,
      disableClose: true,
    });
  }

  async getDeleteData(e: any) {
    this.deleteDisable = true;
    var options = new TaskOptions();
    var data = JSON.parse(e);
    options.changeNotes = data['reason'];
    options.effectiveDate = data['effectiveDate'];
    options.actionType = "delete";


    if (this.isSuggestion) {
      this.taskService.deleteSuggestion(this.taskId, this.suggestionDeleteId, options).then(async (_) => {
        this.alert.successToast(await this.transformTitle('Task') + " Suggestion Deleted");
        this.getSuggestionsData();
      })
    }
    else if (this.isStep) {
      await this.taskService.deleteSteps(this.taskId, this.stepDeleteId, options).then((_) => {
        this.alert.successToast("Step Deleted");
        this.closeAndRefreshSteps();
      }).finally(() => {
        this.deleteDisable = false;
      });
    }
    else {
      this.taskService.removeQuestion(this.taskId, this.questionDeleteId, options).then((_) => {
        this.alert.successToast("Question Deleted");
        this.refreshQuestions();
      });
    }

  }

  async deleteQuestion(templateRef: any, question: Task_Question) {
    this.questionDeleteId = question.id;
    this.isStep = false;
    this.isSuggestion = false;
    this.dialogTitle = "Delete " + await this.transformTitle('Task') + " Question";

    this.dialogDesc = `This ` + await this.transformTitle('Task') +` is currently available for ` + await this.transformTitle('Task') +` Qualification in EMP, are you sure you wish to delete this Question?`;
    const dialogRef = this.dialog.open(templateRef, {
      width: '600px',
      height: 'auto',
      hasBackdrop: true,
      disableClose: true,
    });
  }

  async deleteSuggestion(templateRef: any, suggestion: Task_Suggestion) {
    this.suggestionDeleteId = suggestion.id;
    this.isStep = false;
    this.isSuggestion = true;
    this.dialogTitle = "Delete " + await this.transformTitle('Task') + " Suggestion";

    this.dialogDesc = `This ` + await this.transformTitle('Task') +` is currently available for ` + await this.transformTitle('Task') +` Qualification in EMP, are you sure you wish to delete this Suggestion?`;
    const dialogRef = this.dialog.open(templateRef, {
      width: '600px',
      height: 'auto',
      hasBackdrop: true,
      disableClose: true,
    });
  }

  openQuestionFlyPanel(templateRef: any, question: Task_Question | undefined) {
    this.taskQuestion = question;
    const portal = new TemplatePortal(templateRef, this.vcf);
    this.flypanelSrvc.open(portal);
  }

  openSuggestionFlyPanel(templateRef: any, suggestion: Task_Suggestion | undefined) {
    this.taskSuggestion = suggestion;
    const portal = new TemplatePortal(templateRef, this.vcf);
    this.flypanelSrvc.open(portal);
  }

  async getSuggestionsData() {
    this.suggestions = await this.taskService.getSuggestions(this.task.id);    
    this.originalSuggNumbers = [];
    this.suggestions.forEach(element => {
      this.originalSuggNumbers.push(element.number);
    });
    
    this.SuggestionSource.data = this.suggestions;
    this.SuggestionSource.paginator = this.suggestionPaginator;
  }

  async getToolsData() {
    this.tools = await this.taskService.getTools(this.taskId);
  }

  async getQAData() {
    this.questions = await this.taskService.getTaskQuestions(this.taskId);
    this.originalQANumbers = [];
    this.questions.forEach((data)=>{
      this.originalQANumbers.push(data.questionNumber);
    })

    this.Datasource.data = this.questions;
    this.Datasource.paginator = this.questionPaginator;
  }

  async closeAndRefreshSteps() {

   // this.flypanelSrvc.close()
    this.steps = await this.taskService.getSteps(this.taskId);

    const regex = /<img (.*?)>/;
    this.images = [];
    this.originalNumbers = [];
    this.steps.forEach((data: Task_Step) => {
      this.collapseImage.push(false);
      this.originalNumbers.push(data.number);
      this.images.push(data.description?.match(regex)?.[0]);
      var description = data.description.split(regex);
      if (description.length > 1) {
        data.description = description[0] + description[2];
      }
    });

  }

  refreshReference(data: any) {
    this.flypanelSrvc.close();
    this.task.references = data;
  }

  refreshCriteria(data: any) {
    this.flypanelSrvc.close();
    this.task.criteria = data;
  }

  refreshConditions(data: any) {
    this.flypanelSrvc.close();
    this.task.conditions = data;
  }

  refreshTools() {
    this.getToolsData();
    //this.flypanelSrvc.close();
  }

  refreshQuestions() {
    this.getQAData();
    //this.flypanelSrvc.close();
  }

  refreshSuggestion() {
    this.getSuggestionsData();
   // this.flypanelSrvc.close();
  }

  refreshTrainingGroups() {
    this.getTrainingGroupData();
    this.flypanelSrvc.close();
  }

  async dropQA(event:any){
    moveItemInArray(this.questions,event.previousIndex,event.currentIndex);
    this.Datasource.data = this.questions;
    this.Datasource.data.forEach((element:Task_Question,i:any)=>{
      element.questionNumber = this.originalQANumbers[i];
    });
    var options = new Task_QuestionNumberOptions();
    options.numbers = Object.assign([],this.originalQANumbers);
    options.questionIds = this.questions.map((question : Task_Question)=>{
      return question.id;
    });
    await this.taskService.updateQANumbers(options);
  }

  async dropSuggestion(event:any){
    moveItemInArray(this.suggestions,event.previousIndex,event.currentIndex);
    this.SuggestionSource.data = this.suggestions;
    this.SuggestionSource.data.forEach((element:Task_Suggestion,i:any)=>{
      element.number = this.originalSuggNumbers[i];
    })
    var options = new Task_SuggestionNumberOptions();
    options.numbers = Object.assign([],this.originalSuggNumbers);
    options.suggestionIds = this.suggestions.map((suggestion:Task_Suggestion)=>{
      return suggestion.id;
    });
    await this.taskService.updateSuggestionNumbers(options);
  }

  async drop(event: CdkDragDrop<any[]>) {
    this.savingOrder = true;
    moveItemInArray(this.steps, event.previousIndex, event.currentIndex);
    moveItemInArray(this.images, event.previousIndex, event.currentIndex);
    moveItemInArray(this.collapseImage,event.previousIndex,event.currentIndex);
    var options = new Task_StepNumberOptions();
    options.numbers = Object.assign([], this.originalNumbers);
    options.stepIds = this.steps.map((step: Task_Step) => {
      return step.id;
    });
    await this.taskService.UpdateStepNumbers(options).finally(() => {
      this.savingOrder = false;
    });
  }
}

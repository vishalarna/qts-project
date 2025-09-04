import { moveItemInArray } from '@angular/cdk/drag-drop';
import { TemplatePortal } from '@angular/cdk/portal';
import { Component, Input, OnInit, ViewContainerRef } from '@angular/core';
import { MatLegacyDialog as MatDialog } from '@angular/material/legacy-dialog';
import { MatLegacyTableDataSource as MatTableDataSource } from '@angular/material/legacy-table';
import { ActivatedRoute } from '@angular/router';
import { MetaTaskOJTVM } from 'src/app/_DtoModels/Task/MetaTaskOJTVM';
import { MetaTask_OJTVM, MetaTask_QuestionsVM, MetaTask_SuggestionsVM } from 'src/app/_DtoModels/Task/MetaTask_OJTVM';
import { Task } from 'src/app/_DtoModels/Task/Task';
import { TaskOptions } from 'src/app/_DtoModels/Task/TaskOptions';
import { Task_Question } from 'src/app/_DtoModels/Task_Question/Task_Question';
import { Task_Step } from 'src/app/_DtoModels/Task_Step/Task_Step';
import { Task_Suggestion } from 'src/app/_DtoModels/Task_Suggestion/Task_Suggestion';
import { Task_SuggestionNumberOptions } from 'src/app/_DtoModels/Task_Suggestion/Task_SuggestionNumberOptions';
import { Task_Tool } from 'src/app/_DtoModels/Task_Tool/Task_Tool';
import { Tool } from 'src/app/_DtoModels/Tool/Tool';
import { TrainingGroup } from 'src/app/_DtoModels/TrainingGroup/TrainingGroup';
import { LabelReplacementPipe } from 'src/app/_Pipes/label-replacement.pipe';
import { TasksService } from 'src/app/_Services/QTD/tasks.service';
import { DataBroadcastService } from 'src/app/_Shared/services/DataBroadcast.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { SubSink } from 'subsink';

@Component({
  selector: 'app-task-meta-ojt-guide',
  templateUrl: './task-meta-ojt-guide.component.html',
  styleUrls: ['./task-meta-ojt-guide.component.scss']
})
export class TaskMetaOjtGuideComponent implements OnInit {
  @Input() isActive:boolean = true;
  task!:Task;
  displayColumns = ["order","number","question","answer","buttons"];
  questions:MetaTask_QuestionsVM[] = [];
  steps:MetaTask_OJTVM[] = [];
  selectedStep!:Task_Step;
  taskQuestion!:Task_Question | undefined;
  taskSuggestion!:Task_Suggestion | undefined;
  collapseImage: boolean[] = [];
  images: any[] = [];
  Datasource = new MatTableDataSource<MetaTask_QuestionsVM>();
  SuggestionSource = new MatTableDataSource<MetaTask_SuggestionsVM>();
  stepDeleteId!:any;
  isStep = false;
  isSuggestion = false;
  dialogTitle = "";
  dialogDesc = "";
  questionDeleteId!:any;
  suggestions:MetaTask_SuggestionsVM[] = [];
  suggestionDisplay = ['order','number', 'description', 'buttons'];
  originalQANumbers:number[] = [];
  originalSuggNumbers:number[] = [];
  originalNumbers:number[] = [];
  suggestionDeleteId!:any;
  tools:Tool[] = [];
  trainingGroup:TrainingGroup[] = [];
  taskId = "";
  deleteDisable = false;
  subscription = new SubSink();
  ojtData!:MetaTaskOJTVM;
  canDeleteOJTData = false;

  constructor(
    public flypanelSrvc:FlyInPanelService,
    private vcf:ViewContainerRef,
    public dialog:MatDialog,
    private taskService:TasksService,
    private alert :SweetAlertService,
    private route : ActivatedRoute,
    private dataBroadcastService: DataBroadcastService,
    private labelPipe: LabelReplacementPipe
  ) { }

  ngOnInit(): void {
    this.subscription.sink = this.route.params.subscribe(async (res: any) => {
      var t = String(res.id).split('-');
      this.taskId = String(res.id).split('-')[0];

      await this.readyData();
    })

    this.subscription.sink = this.dataBroadcastService.refreshMeta.subscribe(()=>{
        this.readyData();
    })
  }

  async transformTitle(title: string) {
    const labelName = await this.labelPipe.transform(title);
    return labelName;
  }

  async readyData(){
    this.task = await this.taskService.getAllData(this.taskId);
    this.canDeleteOJTData = await this.taskService.canMakeInactive(this.taskId);
    await this.readyTools();
    await this.readyQuestions();
    await this.readySuggestions();
    await this.readyTrainingGroups();
    await this.readySteps();
    await this.readyCondCritRef();
  }

  async readyTrainingGroups(){
    this.trainingGroup = await this.taskService.getMetaTrainingGroups(this.taskId);
  }

  async readySuggestions(){
    this.suggestions = await this.taskService.getMetaSuggestions(this.taskId);
    this.SuggestionSource.data = this.suggestions;
  }

  async readyQuestions(){
    this.questions = await this.taskService.getMetaQuestionsData(this.taskId);
    this.Datasource.data = this.questions;
  }

  async readyTools(){
    this.tools = await this.taskService.getMetaToolsData(this.taskId);
  }

  async readyCondCritRef(){
    this.ojtData = await this.taskService.getMetaCondCritRefAsync(this.taskId);
  }

  async readySteps(){
    
    this.steps = await this.taskService.getMetaSteps(this.taskId);

    const regex = /<img (.*?)>/;
    this.images = [];
    this.originalNumbers = [];
    this.steps.forEach((data: MetaTask_OJTVM) => {
      this.collapseImage.push(false);
      this.originalNumbers.push(data.number ?? 0);
      this.images.push(data.description?.match(regex)?.[0]);
      var description = data.description.split(regex);
      if (description.length > 1) {
        data.description = description[0] + description[2];
      }
    });

  }

  openStepFlyPanel(templateRef: any, step: MetaTask_OJTVM | undefined, index: any) {
    if (step !== undefined) {
      var myStep:MetaTask_OJTVM = Object.assign(step ?? new MetaTask_OJTVM());
      myStep.description = myStep.description.concat(this.images[index] ?? "");
      this.selectedStep = new Task_Step();
      this.selectedStep.description = myStep.description;
      this.selectedStep.id = myStep.id;
      this.selectedStep.number = myStep.number ?? 0;
      this.selectedStep.taskId = myStep.taskId;
    }
    else {
      this.selectedStep = new Task_Step();
    }

    const portal = new TemplatePortal(templateRef, this.vcf);
    this.flypanelSrvc.open(portal);
  }

  openQuestionFlyPanel(templateRef: any, question: MetaTask_QuestionsVM | undefined) {
    if(question === undefined){
      this.taskQuestion = undefined;
    }
    else{
      this.taskQuestion = new Task_Question();
      this.taskQuestion.id = question.id;
      this.taskQuestion.questionNumber = question.questionNumber;
      this.taskQuestion.question = question.question;
      this.taskQuestion.answer = question.answer;
      this.taskQuestion.taskId = question.taskId;
    }
    const portal = new TemplatePortal(templateRef, this.vcf);
    this.flypanelSrvc.open(portal);
  }

  openSuggestionFlyPanel(templateRef: any, suggestion: MetaTask_SuggestionsVM | undefined) {
    if(suggestion === undefined){
      this.taskSuggestion = undefined;
    }
    else{
      this.taskSuggestion = new Task_Suggestion();
      this.taskSuggestion.id = suggestion.id;
      this.taskSuggestion.number = suggestion.number;
      this.taskSuggestion.description = suggestion.description;
      this.taskSuggestion.taskId = suggestion.taskId;
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

  async deleteQuestion(templateRef: any, question: MetaTask_QuestionsVM) {
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

  async dropSuggestion(event:any){
    moveItemInArray(this.suggestions,event.previousIndex,event.currentIndex);
    this.SuggestionSource.data = this.suggestions;
    this.SuggestionSource.data.forEach((element:MetaTask_SuggestionsVM,i:any)=>{
      element.number = this.originalSuggNumbers[i];
    })
    var options = new Task_SuggestionNumberOptions();
    options.numbers = Object.assign([],this.originalSuggNumbers);
    options.suggestionIds = this.suggestions.map((suggestion:MetaTask_SuggestionsVM)=>{
      return suggestion.id;
    });
    await this.taskService.updateSuggestionNumbers(options);
  }

  async deleteSuggestion(templateRef: any, suggestion: MetaTask_SuggestionsVM) {
    this.suggestionDeleteId = suggestion.id;
    this.isStep = false;
    this.isSuggestion = true;
    this.dialogTitle = "Delete " + await this.transformTitle('Task') +" Suggestion";

    this.dialogDesc = `This ` + await this.transformTitle('Task') +` is currently available for ` + await this.transformTitle('Task') +` Qualification in EMP, are you sure you wish to delete this Suggestion?`;
    const dialogRef = this.dialog.open(templateRef, {
      width: '600px',
      height: 'auto',
      hasBackdrop: true,
      disableClose: true,
    });
  }

  openFlyPanel(templateRef: any) {

    const portal = new TemplatePortal(templateRef, this.vcf);
    this.flypanelSrvc.open(portal);
  }

  closeAndRefreshSteps(){
    this.steps = [];
    //this.flypanelSrvc.close();
    this.readySteps();
  }

  refreshQuestions(){
    this.questions = [];
    //this.flypanelSrvc.close();
    this.readyQuestions();
  }

  refreshSuggestion(){
    //this.flypanelSrvc.close();
    this.getSuggestionsData();
  }

  getSuggestionsData(){
    this.suggestions = [];
    //this.flypanelSrvc.close();
    this.readySuggestions();
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

}

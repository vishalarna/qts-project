import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { UntypedFormBuilder, UntypedFormControl, UntypedFormGroup, Validators } from '@angular/forms';
import { Task_Question } from '@models/Task_Question/Task_Question';
import { TaskReviewActionItem_OperationType_VM } from '@models/Task_Review/TaskReviewActionItem_OperationType_VM';
import { TaskReviewActionItem_QuestionAndAnswer_Operation_VM } from '@models/Task_Review/TaskReviewActionItem_QuestionAndAnswer_Operation_VM';
import { TaskReviewActionItemService } from 'src/app/_Services/QTD/TaskReviewActionItem/api.task-review-actionItem.service';
import { TasksService } from 'src/app/_Services/QTD/tasks.service';

@Component({
  selector: 'app-flypanel-question-and-answer-operation',
  templateUrl: './flypanel-question-and-answer-operation.component.html',
  styleUrls: ['./flypanel-question-and-answer-operation.component.scss']
})
export class FlypanelQuestionAndAnswerOperationComponent implements OnInit {

  @Input () editOperation:TaskReviewActionItem_QuestionAndAnswer_Operation_VM;
  @Output () quesAnsOperation_Data  = new EventEmitter<TaskReviewActionItem_QuestionAndAnswer_Operation_VM>();
  @Input () taskId : string;
  @Output () closed = new EventEmitter<any>();
  @Input() actionItemTypeName : string;
  operationType:TaskReviewActionItem_OperationType_VM[];
  quesAnsOperationForm:UntypedFormGroup;
  operationTypeName:string;
  taskQuestionData:Task_Question[];
  isLoading:boolean;
  constructor(
    private taskReviewActionItemSrvc:TaskReviewActionItemService,
    private fb:UntypedFormBuilder,
    private taskService:TasksService
  ) { }

  ngOnInit(): void {
    this.isLoading = true;
    this.loadAsync();
    this.initializeForm();
    this.operationTypeName = this.editOperation?.operation;
  }

  async loadAsync(){
    await this.getOperationTypes();
    await this.getTaskQuestionsData();
    this.isLoading = false;
  }

  initializeForm(){
    this.quesAnsOperationForm = this.fb.group({
      operation: new UntypedFormControl(this.editOperation?.operationTypeId,[Validators.required]),
      taskQuestion: new UntypedFormControl(this.editOperation?.task_QuestionId,[Validators.required]),
      question: new UntypedFormControl(this.editOperation?.question,[Validators.required]),
      answer: new UntypedFormControl(this.editOperation?.answer,[Validators.required])
    })
    this.quesAnsOperationForm.get('operation')?.valueChanges.subscribe(val => this.onOperationTypeChange(val));
    this.quesAnsOperationForm.get('taskQuestion')?.valueChanges.subscribe(val => this.onTaskQuestionChange(val));
    this.checkEditConditions();
  }

  checkEditConditions(){
    if(this.editOperation?.operation=='RemoveLink'){
      this.quesAnsOperationForm.get('question')?.disable();
      this.quesAnsOperationForm.get('answer')?.disable();
    }
  }

  async getTaskQuestionsData(){
    this.taskQuestionData = (await this.taskService.getTaskQuestions(this.taskId));
  }

  onOperationTypeChange(operationTypeId:any){
    this.quesAnsOperationForm.get('taskQuestion')?.setValue(null);
    this.quesAnsOperationForm.get('question')?.setValue(null);
    this.quesAnsOperationForm.get('answer')?.setValue(null);
    const operationType = this.operationType.find(x => x.id == operationTypeId);
    this.operationTypeName = operationType?.type;
    if (this.operationTypeName == 'RemoveLink') {
      this.getTaskQuestionsData();
      this.quesAnsOperationForm.get('taskQuestion')?.enable();
      this.quesAnsOperationForm.get('question')?.disable();
      this.quesAnsOperationForm.get('answer')?.disable();
    } else if (this.operationTypeName == 'CreateRecord') {
      this.quesAnsOperationForm.get('question')?.enable();
      this.quesAnsOperationForm.get('answer')?.enable();
    }else if(this.operationTypeName=='UpdateRecord'){
      this.getTaskQuestionsData();
      this.quesAnsOperationForm.get('taskQuestion')?.enable();
      if(this.quesAnsOperationForm.get('taskQuestion')?.value){
        this.quesAnsOperationForm.get('question')?.enable();
        this.quesAnsOperationForm.get('answer')?.enable();
      }else{
        this.quesAnsOperationForm.get('question')?.disable();
        this.quesAnsOperationForm.get('answer')?.disable();
      }
    }
  }

  async getOperationTypes(){
    this.operationType = await this.taskReviewActionItemSrvc.getOperationTypesAsync(this.actionItemTypeName);
  }

  closeFlyPanel(){
    this.closed.emit();
  }

  onTaskQuestionChange(taskQuesId:string){
    if (this.operationTypeName == 'RemoveLink') {
      this.quesAnsOperationForm.get('question')?.disable();
      this.quesAnsOperationForm.get('answer')?.disable();
    }
    if(this.operationTypeName == 'UpdateRecord'){
      this.quesAnsOperationForm.get('question')?.enable();
      this.quesAnsOperationForm.get('answer')?.enable();
    }
    const selectedQues = this.taskQuestionData?.find(item => item.id == taskQuesId);
    if (selectedQues) {
      const tempDiv1 = document.createElement('div');
      tempDiv1.innerHTML = selectedQues.question;
      const descriptionWithoutTagsQues = tempDiv1.textContent || tempDiv1.innerText || '';
      this.quesAnsOperationForm.get('question')?.setValue(descriptionWithoutTagsQues);
      const tempDiv2 = document.createElement('div');
      tempDiv2.innerHTML = selectedQues.answer;
      const descriptionWithoutTagsAns = tempDiv2.textContent || tempDiv2.innerText || '';
      this.quesAnsOperationForm.get('answer')?.setValue(descriptionWithoutTagsAns);
    }
  }

  saveDisabled(){
    if((this.quesAnsOperationForm.get('question').value) && (this.quesAnsOperationForm.get('answer').value)){
      return false;
      }
    return true;
  }

  onQuesAnsOperationSave(){
    var quesAnsOperationData = new TaskReviewActionItem_QuestionAndAnswer_Operation_VM();
    quesAnsOperationData.id = this.editOperation?.id;
    quesAnsOperationData.operationTypeId = this.quesAnsOperationForm.get('operation')?.value;
    quesAnsOperationData.task_QuestionId = this.quesAnsOperationForm.get('taskQuestion')?.value;
    quesAnsOperationData.question = this.quesAnsOperationForm.get('question')?.value;
    quesAnsOperationData.answer = this.quesAnsOperationForm.get('answer')?.value;
    quesAnsOperationData.operation = this.operationType.find(x => x.id == quesAnsOperationData.operationTypeId).type;
    this.quesAnsOperation_Data.emit(quesAnsOperationData);
    this.closeFlyPanel();
  }

}

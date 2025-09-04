import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { UntypedFormBuilder, UntypedFormControl, UntypedFormGroup, Validators } from '@angular/forms';
import { TaskReviewActionItem_OperationType_VM } from '@models/Task_Review/TaskReviewActionItem_OperationType_VM';
import { TaskReviewActionItem_Step_Operation_VM } from '@models/Task_Review/TaskReviewActionItem_Step_Operation_VM';
import { Task_Step } from '@models/Task_Step/Task_Step';
import { TaskReviewActionItemService } from 'src/app/_Services/QTD/TaskReviewActionItem/api.task-review-actionItem.service';
import { TasksService } from 'src/app/_Services/QTD/tasks.service';

@Component({
  selector: 'app-flypanel-step-operation',
  templateUrl: './flypanel-step-operation.component.html',
  styleUrls: ['./flypanel-step-operation.component.scss']
})
export class FlypanelStepOperationComponent implements OnInit {

  @Input () editOperation:TaskReviewActionItem_Step_Operation_VM;
  @Output () stepOperation_Data = new EventEmitter<TaskReviewActionItem_Step_Operation_VM>();
  @Input () taskId : string;
  @Output () closed = new EventEmitter<any>();
  @Input() actionItemTypeName : string;
  operationType:TaskReviewActionItem_OperationType_VM[];
  stepOperationForm:UntypedFormGroup
  operationTypeName:string;
  taskSteps:Task_Step[];
  isLoading:boolean;
  constructor(
    private taskReviewActionItemSrvc:TaskReviewActionItemService,
    private fb:UntypedFormBuilder,
    private taskService:TasksService
  ) { }

  ngOnInit(): void {
    this.isLoading = true;
    this.loadAsync();
    this.initializeStepForm();
    this.operationTypeName = this.editOperation?.operation;
  }

  async loadAsync(){
    await this.getOperationTypes();
    await this.getTaskSteps();
    this.isLoading = false;
  }

  initializeStepForm(){
    this.stepOperationForm = this.fb.group({
      operation:new UntypedFormControl(this.editOperation?.operationTypeId,[Validators.required]),
      taskStep:new UntypedFormControl(this.editOperation?.task_StepId,[Validators.required]),
      description:new UntypedFormControl(this.editOperation?.description,[Validators.required])
    })
    this.stepOperationForm.get('operation')?.valueChanges.subscribe(val => this.onOperationTypeChange(val));
    this.stepOperationForm.get('taskStep')?.valueChanges.subscribe(val => this.onTaskStepChange(val));
    this.checkEditConditions();
  }

  checkEditConditions(){
    if(this.editOperation?.operation=='RemoveLink'){
      this.stepOperationForm.get('description')?.disable();
    }
  }

  async getTaskSteps(){
    this.taskSteps = (await this.taskService.getSteps(this.taskId));
  }

  async getOperationTypes(){
    this.operationType = await this.taskReviewActionItemSrvc.getOperationTypesAsync(this.actionItemTypeName);
  }

  onOperationTypeChange(operationTypeId: any) {
    this.stepOperationForm.get('taskStep')?.setValue(null);
    this.stepOperationForm.get('description')?.setValue(null);
    const operationType = this.operationType.find(x => x.id == operationTypeId);
    this.operationTypeName = operationType?.type;
    if (this.operationTypeName == 'RemoveLink') {
      this.getTaskSteps();
      this.stepOperationForm.get('taskStep')?.enable();
      this.stepOperationForm.get('description')?.disable();
    } else if (this.operationTypeName == 'CreateRecord') {
      this.stepOperationForm.get('taskStep')?.disable();
      this.stepOperationForm.get('description')?.enable();
    }else if(this.operationTypeName=='UpdateRecord'){
      this.getTaskSteps();
      this.stepOperationForm.get('taskStep')?.enable();
      if(this.stepOperationForm.get('taskStep')?.value){
        this.stepOperationForm.get('description')?.enable();
      }else{
        this.stepOperationForm.get('description')?.disable();
      }
    }
  }

  saveDisabled(){
   if(this.stepOperationForm.get('description').value){
        return false;
    }
    return true;
  }

  onTaskStepChange(taskStepId: any) {
    if (this.operationTypeName == 'RemoveLink') {
      this.stepOperationForm.get('description')?.disable();
    }
    if(this.operationTypeName == 'UpdateRecord'){
      this.stepOperationForm.get('description')?.enable();
    }
    const selectedStep = this.taskSteps?.find(step => step.id == taskStepId);
    if (selectedStep) {
      const tempDiv = document.createElement('div');
      tempDiv.innerHTML = selectedStep.description;
      const descriptionWithoutTags = tempDiv.textContent || tempDiv.innerText || '';
      this.stepOperationForm.get('description')?.setValue(descriptionWithoutTags);
    }
  }

  closeFlyPanel(){
    this.closed.emit();
  }

  onStepOperationSave(){
    var stepOperationData = new TaskReviewActionItem_Step_Operation_VM();
    stepOperationData.id = this.editOperation?.id;
    stepOperationData.operationTypeId = this.stepOperationForm.get('operation')?.value;
    stepOperationData.task_StepId = this.stepOperationForm.get('taskStep')?.value;
    stepOperationData.description = this.stepOperationForm.get('description')?.value;
    stepOperationData.operation = this.operationType.find(x => x.id == stepOperationData.operationTypeId).type;
    this.stepOperation_Data.emit(stepOperationData);
    this.closeFlyPanel();
  }

}

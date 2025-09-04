import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { UntypedFormBuilder, UntypedFormControl, UntypedFormGroup, Validators } from '@angular/forms';
import { TaskReviewActionItem_OperationType_VM } from '@models/Task_Review/TaskReviewActionItem_OperationType_VM';
import { TaskReviewActionItem_Suggestion_Operation_VM } from '@models/Task_Review/TaskReviewActionItem_Suggestion_Operation_VM';
import { Task_Suggestion } from '@models/Task_Suggestion/Task_Suggestion';
import { TaskReviewActionItemService } from 'src/app/_Services/QTD/TaskReviewActionItem/api.task-review-actionItem.service';
import { TasksService } from 'src/app/_Services/QTD/tasks.service';

@Component({
  selector: 'app-flypanel-suggestion-operation',
  templateUrl: './flypanel-suggestion-operation.component.html',
  styleUrls: ['./flypanel-suggestion-operation.component.scss']
})
export class FlypanelSuggestionOperationComponent implements OnInit {

  @Output () suggestionOperation_Data = new EventEmitter<TaskReviewActionItem_Suggestion_Operation_VM>();
  @Input () taskId : string;
  @Output () closed = new EventEmitter<any>();
  @Input() actionItemTypeName : string;
  @Input () editOperation : TaskReviewActionItem_Suggestion_Operation_VM;
  operationType:TaskReviewActionItem_OperationType_VM[];
  suggestionOperationForm:UntypedFormGroup;
  operationTypeName:string;
  taskSuggestions:Task_Suggestion[];
  isLoading:boolean;
  constructor(
    private taskReviewActionItemSrvc:TaskReviewActionItemService,
    private fb:UntypedFormBuilder,
    private taskService:TasksService
  ) { }

  ngOnInit(): void {
    this.isLoading = true;
    this.loadAsync();
    this.initializeFormData();
    this.operationTypeName = this.editOperation?.operation;
  }

  async loadAsync(){
    await this.getOperationTypes();
    await this.getTaskSuggestions();
    this.isLoading = false;
  }

  async getOperationTypes(){
    this.operationType = await this.taskReviewActionItemSrvc.getOperationTypesAsync(this.actionItemTypeName);
  }

  initializeFormData(){
    this.suggestionOperationForm = this.fb.group({
      operation: new UntypedFormControl(this.editOperation?.operationTypeId,[Validators.required]),
      taskSuggestion: new UntypedFormControl(this.editOperation?.task_SuggestionId,[Validators.required]),
      description: new UntypedFormControl(this.editOperation?.description,[Validators.required])
    });
    this.suggestionOperationForm.get('operation')?.valueChanges.subscribe(val => this.onOperationTypeChange(val));
    this.suggestionOperationForm.get('taskSuggestion')?.valueChanges.subscribe(val => this.onTaskSuggestionChange(val));
    this.checkEditConditions();
  }

  checkEditConditions(){
    if(this.suggestionOperationForm.get('operation')?.value){
      this.getTaskSuggestions();
    }
    if(this.editOperation?.operation=='RemoveLink'){
      this.suggestionOperationForm.get('description')?.disable();
    }
  }

  async getTaskSuggestions(){
    this.taskSuggestions = (await this.taskService.getSuggestions(this.taskId));
  }

  onOperationTypeChange(operationTypeId: any) {
    this.suggestionOperationForm.get('taskSuggestion')?.setValue(null);
    this.suggestionOperationForm.get('description')?.setValue(null);
    const operationType = this.operationType.find(x => x.id == operationTypeId);
    this.operationTypeName = operationType?.type;
    if (this.operationTypeName == 'RemoveLink') {
      this.getTaskSuggestions();
      this.suggestionOperationForm.get('taskSuggestion')?.enable();
      this.suggestionOperationForm.get('description')?.disable();
    } else if (this.operationTypeName == 'CreateRecord') {
      this.suggestionOperationForm.get('taskSuggestion')?.disable();
      this.suggestionOperationForm.get('description')?.enable();
    }else if(this.operationTypeName=='UpdateRecord'){
      this.getTaskSuggestions();
      this.suggestionOperationForm.get('taskSuggestion')?.enable();
      if(this.suggestionOperationForm.get('taskSuggestion')?.value){
        this.suggestionOperationForm.get('description')?.enable();
      }else{
        this.suggestionOperationForm.get('description')?.disable();
      }
    }
  }

  onTaskSuggestionChange(taskSuggestionId: string){
    if (this.operationTypeName == 'RemoveLink') {
      this.suggestionOperationForm.get('description')?.disable();
    }
    if(this.operationTypeName == 'UpdateRecord'){
      this.suggestionOperationForm.get('description')?.enable();
    }
    const selectedStep = this.taskSuggestions?.find(step => step.id == taskSuggestionId);
    if (selectedStep) {
      const tempDiv = document.createElement('div');
      tempDiv.innerHTML = selectedStep.description;
      const descriptionWithoutTags = tempDiv.textContent || tempDiv.innerText || '';
      this.suggestionOperationForm.get('description')?.setValue(descriptionWithoutTags);
    }
  }

  closeFlyPanel(){
    this.closed.emit();
  }

  saveDisabled(){
    if(this.suggestionOperationForm.get('description').value){
         return false;
     }
     return true;
   }

   onSuggestionOperationSave(){
    var suggestionOperationData = new TaskReviewActionItem_Suggestion_Operation_VM();
    suggestionOperationData.id = this.editOperation?.id;
    suggestionOperationData.operationTypeId = this.suggestionOperationForm.get('operation')?.value;
    suggestionOperationData.task_SuggestionId = this.suggestionOperationForm.get('taskSuggestion')?.value;
    suggestionOperationData.description = this.suggestionOperationForm.get('description')?.value;
    suggestionOperationData.operation = this.operationType.find(x => x.id == suggestionOperationData.operationTypeId).type;
    this.suggestionOperation_Data.emit(suggestionOperationData);
    this.closeFlyPanel();
   }

}

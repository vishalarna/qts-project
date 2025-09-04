import { Component, EventEmitter, Inject, Input, OnInit, Output } from '@angular/core';
import { UntypedFormBuilder, UntypedFormControl, UntypedFormGroup, Validators } from '@angular/forms';
import { MatLegacyTableDataSource as MatTableDataSource } from '@angular/material/legacy-table';
import { QtdUserVM } from '@models/QtdUser/QtdUserVM';
import { Task } from '@models/Task/Task';
import { TaskReviewActionItemPriority_VM } from '@models/Task_Review/TaskReviewActionItemPriority_VM';
import { TaskReviewActionItem_VM } from '@models/Task_Review/TaskReviewActionItem_VM';
import { TrainingIssue_ActionItem_VM } from '@models/TrainingIssues/TrainingIssue_ActionItem_VM';
import { TrainingIssue_ActionItems_VM } from '@models/TrainingIssues/TrainingIssue_ActionItems_VM';
import { TrainingIssue_VM } from '@models/TrainingIssues/TrainingIssue_VM';
import { DynamicLabelReplacementPipe } from 'src/app/_Pipes/dynamic-label-replacement.pipe';
import { ApiTaskReviewService } from 'src/app/_Services/QTD/TaskReview/api.taskReview.service';
import { TaskReviewActionItemService } from 'src/app/_Services/QTD/TaskReviewActionItem/api.task-review-actionItem.service';
import { TrainingIssuesService } from 'src/app/_Services/QTD/TrainingIssues/training-issues.service';
import { QTDService } from 'src/app/_Services/QTD/qtd.service';
import { TasksService } from 'src/app/_Services/QTD/tasks.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import * as ckcustomBuild from 'src/app/ckcustomBuild/build/ckeditor.js';

@Component({
  selector: 'app-flypanel-action-item',
  templateUrl: './flypanel-action-item.component.html',
  styleUrls: ['./flypanel-action-item.component.scss']
})
export class FlypanelActionItemComponent implements OnInit {

  public Editor = ckcustomBuild;
  @Input () selected_ActionItemId:string;
  @Input () task_ReviewId:string
  @Output () actionItem_Data = new EventEmitter<TaskReviewActionItem_VM>();
  @Input () actionItem:string;
  @Input () taskId: string;
  actionItemType: string;
  actionItemForm:UntypedFormGroup;
  actionItemsTypes:string[];
  openOperationFlyIn:boolean;
  tableColumns:string[];
  priorities:TaskReviewActionItemPriority_VM[];
  assignees:QtdUserVM[];
  actionItemData:TaskReviewActionItem_VM;
  selectedOperation:any;
  selectedOperationIndex:number;
  isLoading:boolean;
  taskDetails:Task;
  checkStatus:boolean =false;
  trainingIssue_VM: TrainingIssue_VM;
  constructor(
    public flyPanelSrvc:FlyInPanelService,
    private fb:UntypedFormBuilder,
    private actionItemSrvc:TaskReviewActionItemService,
    private qtdUserSrvc:QTDService,
    private taskReviewService:ApiTaskReviewService,
    private apiTaskService:TasksService,
    private trainingIssuesService: TrainingIssuesService
  ) { }

  ngOnInit(): void {
    this.isLoading = true;
    this.loadAsync();
    this.getActionItemDetails();
    this.openOperationFlyIn = false;
    this.actionItemType = this.actionItem;
    this.tableColumns = ["operation","action"];
  }

  async loadAsync(){
    this.getActionItemTypes();
    this.getAllActionItemPriority();
    this.getAllAssignees();
    this.initialActionItemForm();
    await this.getTrainingIssueAsync();
  }

   async getTrainingIssueAsync() {
     const res = await this.trainingIssuesService.getTrainingIssueByTaskReviewIdAsync(this.task_ReviewId);
     if (res) {
     this.trainingIssue_VM = res;
    }
  }

  async getActionItemTypes(){
    this.actionItemsTypes =await this.actionItemSrvc.getActionItemTypes();
  }

  async getAllActionItemPriority(){
    this.priorities = await this.actionItemSrvc.getAllActionItemPriorities();
  }

  async getAllAssignees(){
    this.assignees = await this.qtdUserSrvc.getAllActiveAsync();
  }

  async getActionItemDetails(){
    if(this.selected_ActionItemId){
      const actionItemDetails = await this.actionItemSrvc.getAsync(this.selected_ActionItemId);
      this.actionItemData = actionItemDetails;
      this.actionItemType = actionItemDetails?.type;
      await this.getTaskDetails();
      this.updateFormValues();
    }else{
      this.actionItemData = new TaskReviewActionItem_VM();
      await this.getTaskDetails();
      this.initialActionItemForm();
      this.isLoading = false;
    }
  }

  async getTaskDetails(){
    this.taskDetails = await this.apiTaskService.get(this.taskId);
  }

  formatDateToYyyyMmDd(dateString: string): string {
    const date = new Date(dateString);
  
    const year = date.getFullYear();
    const month = ('0' + (date.getMonth() + 1)).slice(-2);
    const day = ('0' + date.getDate()).slice(-2);
  
    return `${year}-${month}-${day}`;
  }

  actionTypeChange(e:any){
    this.actionItemForm.get('type').setValue(e.value);
    this.actionItemType = this.actionItemForm.get('type')?.value;
    this.resetTaskreviewActionItem();
  }

  resetTaskreviewActionItem(){
    var actionItem = new TaskReviewActionItem_VM();
    actionItem.type = this.actionItemForm.get('type')?.value;
    actionItem.assigneeId = this.actionItemForm.get('assignee')?.value;
    actionItem.priorityId = this.actionItemForm.get('priority')?.value;
    actionItem.assignedDate = this.actionItemForm.get('assignedDate')?.value;
    actionItem.dueDate = this.actionItemForm.get('dueDate')?.value;
    actionItem.notes = this.actionItemForm.get('notes')?.value;
    actionItem.isMeta = null;
    this.actionItemData = actionItem;
  }

  initialActionItemForm(){
    this.actionItemForm =  this.fb.group({
      type:new UntypedFormControl(this.actionItem,[Validators.required]),
      assignee:new UntypedFormControl(null),
      priority: new UntypedFormControl(null,[Validators.required]),
      assignedDate:new UntypedFormControl(null,[Validators.required]),
      dueDate:new UntypedFormControl(null,[Validators.required]),
      notes:new UntypedFormControl(null),
      task_number:new UntypedFormControl(this.taskDetails?.number),
      task_statement:new UntypedFormControl(this.taskDetails?.description),
      conditions:new UntypedFormControl(this.taskDetails?.conditions),
      criteria:new UntypedFormControl(this.taskDetails?.criteria),
      references:new UntypedFormControl(this.taskDetails?.references),
      isMeta:new UntypedFormControl(this.taskDetails?.isMeta),
    })
  }

  updateFormValues(){
    this.actionItemForm.patchValue({
      type: (this.actionItemData?.type ?this.actionItemData?.type :this.actionItem ),
      assignee: (this.actionItemData?.assigneeId),
      priority:  (this.actionItemData?.priorityId),
      assignedDate: this.formatDateToYyyyMmDd(this.actionItemData?.assignedDate.toString()),
      dueDate: this.formatDateToYyyyMmDd(this.actionItemData?.dueDate.toString()),
      notes: (this.actionItemData?.notes),
      task_number: (this.actionItemData?.task_number),
      task_statement: (this.actionItemData?.task_statement),
      conditions: (this.actionItemData?.conditions),
      criteria: (this.actionItemData?.criteria),
      references: (this.actionItemData?.references),
      isMeta: (this.actionItemData?.isMeta),
    })
    this.actionItemForm.get('type').disable();
    this.isLoading = false;
  }
  

  closeFlyPanel(){
    this.flyPanelSrvc.close();
    this.resetTaskreviewActionItem();
  }

  setDataSource(data:any[]){
    return new MatTableDataSource(data);
  }
  addToDataSource(operation:any,data:any){
    data.push(operation);
  }
  deleteOperation(data:any[], index:number){
    if(data != null && data.length > 0){
      data.splice(index,1);
    }
  }
  updateToDataSource(operation:any,data:any,index:number){
    if(data != null && data.length > 0){
      data[index]=operation;
    }
  }

  async onActionItemSave(){
    this.actionItemData.type = this.actionItemForm.get('type')?.value;
    this.actionItemData.assigneeId = this.actionItemForm.get('assignee')?.value;
    this.actionItemData.priorityId = this.actionItemForm.get('priority')?.value;
    this.actionItemData.assignedDate = this.actionItemForm.get('assignedDate')?.value;
    this.actionItemData.dueDate = this.actionItemForm.get('dueDate')?.value;
    this.actionItemData.notes = this.actionItemForm.get('notes')?.value;
    this.actionItemData.task_number = this.actionItemForm.get('task_number')?.value;
    this.actionItemData.task_statement = this.actionItemForm.get('task_statement')?.value;
    this.actionItemData.conditions = this.actionItemForm.get('conditions')?.value;
    this.actionItemData.criteria = this.actionItemForm.get('criteria')?.value;
    this.actionItemData.references = this.actionItemForm.get('references')?.value;
    this.actionItemData.isMeta = this.actionItemForm.get('isMeta')?.value;
    if(this.selected_ActionItemId){
      var actionItemDataResult = await this.actionItemSrvc.updateAsync(this.selected_ActionItemId,this.actionItemData)
    }else{
      var actionItemDataResult = await this.taskReviewService.createTaskReviewActionItemAsync(this.task_ReviewId,this.actionItemData);
    }
    if(this.trainingIssue_VM?.id != null && this.trainingIssue_VM?.id != undefined){
      this.updateActionItems();
    }
    this.actionItem_Data.emit(actionItemDataResult);
    this.closeFlyPanel();
  }

 async updateActionItems() {
  const actionItems = new TrainingIssue_ActionItems_VM();
  let maxOrder = 0;
  if (this.trainingIssue_VM && this.trainingIssue_VM.actionItems?.length > 0) {
    maxOrder = Math.max(...this.trainingIssue_VM.actionItems.map(ai => ai.order || 0));
  }
  const mappedActionItem: TrainingIssue_ActionItem_VM = {
    id: null,
    order: maxOrder + 1,
    actionItemName: this.actionItemForm.get('type')?.value,
    priorityId: this.actionItemForm.get('priority')?.value,
    dateAssigned: this.actionItemForm.get('assignedDate')?.value,
    dueDate: this.actionItemForm.get('dueDate')?.value,
    dateCompleted: null,
    statusId: '',
    notes: this.actionItemForm.get('notes')?.value,
    priority: this.actionItemForm.get('priority')?.value,
    status: '',
    assigneeName: this.actionItemForm.get('assignee')?.value,
  };
  actionItems.actionItem_VMs = [mappedActionItem];
  await this.trainingIssuesService.updateActionItemsAsync(actionItems, this.trainingIssue_VM.id,this.checkStatus);
 }

  addSpacesBetweenCapitalLetters(input: string): string {
    return input.replace(/([a-z])([A-Z])/g, '$1 $2');
  }

  onTaskNumberInput(e:any){
    let data = e.data.trim();
    if(data=='e'){
      this.actionItemForm.get('task_number')?.setValue(null);
    }
  }

}

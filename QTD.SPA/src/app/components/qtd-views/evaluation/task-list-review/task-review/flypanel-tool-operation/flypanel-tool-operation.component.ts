import { Component, EventEmitter, Input, OnInit, Output, ViewChild } from '@angular/core';
import { UntypedFormBuilder, UntypedFormControl, UntypedFormGroup, Validators } from '@angular/forms';
import { MatLegacySelect as MatSelect } from '@angular/material/legacy-select';
import { TaskReviewActionItem_OperationType_VM } from '@models/Task_Review/TaskReviewActionItem_OperationType_VM';
import { TaskReviewActionItem_Tool_Operation_VM } from '@models/Task_Review/TaskReviewActionItem_Tool_Operation_VM';
import { Tool } from '@models/Tool/Tool';
import { ToolCategory } from '@models/ToolCategory/ToolCategory';
import { TaskReviewActionItemService } from 'src/app/_Services/QTD/TaskReviewActionItem/api.task-review-actionItem.service';
import { TasksService } from 'src/app/_Services/QTD/tasks.service';
import { ToolsService } from 'src/app/_Services/QTD/tools.service';
import { cloneDeep } from 'lodash';

@Component({
  selector: 'app-flypanel-tool-operation',
  templateUrl: './flypanel-tool-operation.component.html',
  styleUrls: ['./flypanel-tool-operation.component.scss']
})
export class FlypanelToolOperationComponent implements OnInit {

  @Input () editOperation:TaskReviewActionItem_Tool_Operation_VM;
  @Output () toolOperation_Data = new EventEmitter<TaskReviewActionItem_Tool_Operation_VM>();
  @Input () taskId : string;
  @Output () closed = new EventEmitter<any>();
  @Input() actionItemTypeName : string;
  operationType:TaskReviewActionItem_OperationType_VM[];
  toolOperationForm:UntypedFormGroup;
  task_tools:Tool[];
  operationTypeName:string;
  toolCategory:ToolCategory[];
  isLoading:boolean;
  filtered_taskTool:Tool[];
  filteredTools: ToolCategory[];
  @ViewChild('select', { static: false }) select!: MatSelect;
  constructor(
    private taskReviewActionItemSrvc:TaskReviewActionItemService,
    private fb:UntypedFormBuilder,
    private taskService:TasksService,
    private toolService:ToolsService
  ) { }

  ngOnInit(): void {
    this.isLoading = true;
    this.loadAsync();
    this.initialFormData();
    this.operationTypeName = this.editOperation?.operation;
  }

  async loadAsync(){
    await this.getOperationTypes();
    await this.getToolWithCategory();
    await this.getTaskTool();
    this.isLoading = false;
  }

  initialFormData(){
    this.toolOperationForm = this.fb.group({
      operation: new UntypedFormControl(this.editOperation?.operationTypeId,[Validators.required]),
      tool: new UntypedFormControl(this.editOperation?.toolId,[Validators.required]),
      searchText : new UntypedFormControl('')
    })
  }

  async getOperationTypes(){
    this.operationType = await this.taskReviewActionItemSrvc.getOperationTypesAsync(this.actionItemTypeName);
  }

  onOperationTypeChange(operationTypeId:string){
    this.toolOperationForm.get('searchText')?.setValue('');
    this.toolOperationForm.get('tool')?.setValue(null);
    const operationType = this.operationType?.find(x => x.id == operationTypeId);
    this.operationTypeName = operationType?.type;
  }

  async getToolWithCategory(){
    this.toolCategory = await this.toolService.getAllToolCategories(true);
    this.filteredTools = Object.assign(this.toolCategory);
  }

  async getTaskTool(){
    this.task_tools = (await this.taskService.getTools(this.taskId));
    this.filtered_taskTool = Object.assign(this.task_tools);
  }
  
  closeFlyPanel(){
    this.closed.emit();
  }

  getFilteredTools(toolsArray:Tool[]){
    const array = toolsArray?.filter((x)=>{return x.active && this.task_tools?.findIndex(m=>m.id==x.id)==-1});
    return array;
  }

  saveDisabled(){
    if(this.toolOperationForm.get('tool').value){
         return false;
     }
     return true;
   }

   onToolOperationSave(){
    var toolOperationData = new TaskReviewActionItem_Tool_Operation_VM();
    toolOperationData.id = this.editOperation?.id;
    toolOperationData.operationTypeId = this.toolOperationForm.get('operation')?.value;
    toolOperationData.toolId = this.toolOperationForm.get('tool')?.value;
    toolOperationData.operation = this.operationType.find(x => x.id == toolOperationData.operationTypeId).type;
    this.toolOperation_Data.emit(toolOperationData);
    this.closeFlyPanel();
   }

   toolSearch(){
    var searchValue = this.toolOperationForm.get('searchText')?.value;
    if(this.operationTypeName=='RemoveLink'){
      this.filtered_taskTool = cloneDeep(this.task_tools);
      this.filtered_taskTool = this.task_tools.filter(x=>{
        var fullData = `${x.toolCategory.title} - ${x.number} - ${x.name}`
        return fullData.toLowerCase().includes(searchValue.toLowerCase().trim())
      })
    }
    if (this.operationTypeName == 'CreateLink') {
      this.filteredTools = cloneDeep(this.toolCategory);
      this.filteredTools.forEach((x)=>{
        x.tools = x.tools.filter((m)=>{
          var fullData = `${x.title} - ${m.number} - ${m.name}`
          return fullData.toLowerCase().includes(searchValue.toLowerCase().trim())
        })
      })
    }
  }

  resetSearch(){
    setTimeout(() => {
      this.toolOperationForm.get('searchText')?.setValue('');
      this.toolSearch();
    }, 500);
  }


  handleKeydown(event: KeyboardEvent) {
    this.select._handleKeydown = (event: KeyboardEvent) => {
      if (event.key === 'SPACE') return;
    };
  }

}

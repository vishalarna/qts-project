import { Component, EventEmitter, Input, OnInit, Output, ViewChild } from '@angular/core';
import { UntypedFormBuilder, UntypedFormControl, UntypedFormGroup, Validators } from '@angular/forms';
import { MatLegacySelect as MatSelect } from '@angular/material/legacy-select';
import { SafetyHazardWithLinkCount } from '@models/SaftyHazard/SafetyHazardWithLinkCount';
import { SaftyHazard_CategoryCompactOptions } from '@models/SaftyHazard_Category/SaftyHazard_CategoryCompactOptions';
import { TaskReviewActionItem_OperationType_VM } from '@models/Task_Review/TaskReviewActionItem_OperationType_VM';
import { TaskReviewActionItem_SafetyHazard_Operation_VM } from '@models/Task_Review/TaskReviewActionItem_SafetyHazard_Operation_VM';
import { TaskReviewActionItemService } from 'src/app/_Services/QTD/TaskReviewActionItem/api.task-review-actionItem.service';
import { SafetyHazardsService } from 'src/app/_Services/QTD/safety-hazards.service';
import { TasksService } from 'src/app/_Services/QTD/tasks.service';
import { cloneDeep } from 'lodash';

@Component({
  selector: 'app-flypanel-safety-hazard-operation',
  templateUrl: './flypanel-safety-hazard-operation.component.html',
  styleUrls: ['./flypanel-safety-hazard-operation.component.scss']
})
export class FlypanelSafetyHazardOperationComponent implements OnInit {

  @Input () editOperation:TaskReviewActionItem_SafetyHazard_Operation_VM;
  @Output() safetyHazardOperation_Data = new EventEmitter<TaskReviewActionItem_SafetyHazard_Operation_VM>();
  @Input () taskId : string;
  @Output () closed = new EventEmitter<any>();
  @Input() actionItemTypeName : string;
  operationType:TaskReviewActionItem_OperationType_VM[];
  safetyHazardOpertionForm:UntypedFormGroup;
  allSafetyHazard:SaftyHazard_CategoryCompactOptions[];
  task_SafetyHazards:SafetyHazardWithLinkCount[];
  operationTypeName:string;
  isLoading:boolean;
  filteredTask_SafetyHazards:SafetyHazardWithLinkCount[];
  filteredSafetyHazards:SaftyHazard_CategoryCompactOptions[];
  @ViewChild('select', { static: false }) select!: MatSelect;
  constructor(
    private taskReviewActionItemSrvc:TaskReviewActionItemService,
    private fb:UntypedFormBuilder,
    private safetyHazardService:SafetyHazardsService,
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
    await this.getAllSafetyHazards();
    await this.getTaskSafetyHazards();
    this.isLoading =false;
  }

  initializeFormData(){
    this.safetyHazardOpertionForm = this.fb.group({
      operation: new UntypedFormControl(this.editOperation?.operationTypeId,[Validators.required]),
      safetyHazard: new UntypedFormControl(this.editOperation?.safetyHazardId,[Validators.required]),
      searchText : new UntypedFormControl('')
    })
  }

  async getOperationTypes(){
    this.operationType = await this.taskReviewActionItemSrvc.getOperationTypesAsync(this.actionItemTypeName);
  }

  async getAllSafetyHazards(){
    this.allSafetyHazard = await this.safetyHazardService.getSHCategoryWithSH();
    this.filteredSafetyHazards = Object.assign(this.allSafetyHazard);
  }

  async getTaskSafetyHazards(){
    this.task_SafetyHazards = (await this.taskService.GetLinkedSHWithCount(this.taskId));
    this.filteredTask_SafetyHazards = Object.assign(this.task_SafetyHazards);
  }

  onOperationTypeChange(operationTypeId:string){
    this.safetyHazardOpertionForm.get('searchText')?.setValue('');
    this.safetyHazardOpertionForm.get('safetyHazard')?.setValue(null);
    const operationType = this.operationType?.find(x => x.id == operationTypeId);
    this.operationTypeName = operationType?.type;
  }

  getFiltererdSafetyHazard(rrArray:any[]){
    const array = rrArray?.filter((x)=>{return x.active && this.task_SafetyHazards?.findIndex(m=>m.id==x.id)==-1});
    return array;
  }

  closeFlyPanel(){
    this.closed.emit();
  }


  onSafetyHazardSave(){
    var safetyHazardOperationData = new TaskReviewActionItem_SafetyHazard_Operation_VM();
    safetyHazardOperationData.id = this.editOperation?.id;
    safetyHazardOperationData.operationTypeId = this.safetyHazardOpertionForm.get('operation')?.value;
    safetyHazardOperationData.safetyHazardId = this.safetyHazardOpertionForm.get('safetyHazard')?.value;
    safetyHazardOperationData.operation = this.operationType.find(x => x.id == safetyHazardOperationData.operationTypeId).type;
    this.safetyHazardOperation_Data.emit(safetyHazardOperationData);
    this.closeFlyPanel();
  }

  safetyHazardSearch(){
    var searchValue = this.safetyHazardOpertionForm.get('searchText')?.value;
    if(this.operationTypeName=='RemoveLink'){
      this.filteredTask_SafetyHazards = cloneDeep(this.task_SafetyHazards);
      this.filteredTask_SafetyHazards = this.task_SafetyHazards.filter(x=>{
        var fullData = `${x.category_Title} - ${x.number} - ${x.title}`
        return fullData.toLowerCase().includes(searchValue.toLowerCase().trim())
      })
    }
    if (this.operationTypeName == 'CreateLink') {
      this.filteredSafetyHazards = cloneDeep(this.allSafetyHazard);
      this.filteredSafetyHazards.forEach((x)=>{
        x.saftyHazardCompactOptions = x.saftyHazardCompactOptions.filter(m=>{
          var  fullData =  `${x.saftyHazard_Category.title} - ${m.number} - ${m.title}`
          return fullData.toLowerCase().includes(searchValue.toLowerCase().trim())
        })
      })
    }
  }

  resetSearch(){
    setTimeout(() => {
      this.safetyHazardOpertionForm.get('searchText')?.setValue('');
      this.safetyHazardSearch();
    }, 500);
  }


  handleKeydown(event: KeyboardEvent) {
    this.select._handleKeydown = (event: KeyboardEvent) => {
      if (event.key === 'SPACE') return;
    };
  }
}

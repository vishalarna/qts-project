import { Component, EventEmitter, Input, OnInit, Output, ViewChild } from '@angular/core';
import { UntypedFormBuilder, UntypedFormControl, UntypedFormGroup, Validators } from '@angular/forms';
import { TaskReviewActionItem_EnablingObjective_Operation_VM } from '@models/Task_Review/TaskReviewActionItem_EnablingObjective_Operation_VM';
import { TaskReviewActionItem_OperationType_VM } from '@models/Task_Review/TaskReviewActionItem_OperationType_VM';
import { EOCatTreeVM } from '@models/TreeVMs/EOTreeVM';
import { TaskReviewActionItemService } from 'src/app/_Services/QTD/TaskReviewActionItem/api.task-review-actionItem.service';
import { EnablingObjectivesService } from 'src/app/_Services/QTD/enabling-objectives.service';
import { TasksService } from 'src/app/_Services/QTD/tasks.service';
import { cloneDeep } from 'lodash';
import { MatLegacySelect as MatSelect } from '@angular/material/legacy-select';
import { EOWithCountOptions } from '@models/EnablingObjective/EOWithCountOptions';

@Component({
  selector: 'app-flypanel-enabling-objective-operation',
  templateUrl: './flypanel-enabling-objective-operation.component.html',
  styleUrls: ['./flypanel-enabling-objective-operation.component.scss']
})
export class FlypanelEnablingObjectiveOperationComponent implements OnInit {

  @Input () editOperation:TaskReviewActionItem_EnablingObjective_Operation_VM;
  @Output () eoOperation_Data = new EventEmitter<TaskReviewActionItem_EnablingObjective_Operation_VM>();
  @Input () taskId : string;
  @Output () closed = new EventEmitter<any>();
  @Input() actionItemTypeName : string;
  enabObjOperationForm:UntypedFormGroup;
  operationType:TaskReviewActionItem_OperationType_VM[];
  task_Eo:EOWithCountOptions[];
  allEo:EOCatTreeVM[];
  operationTypeName:string;
  isLoading:boolean;
  @ViewChild('select', { static: false }) select!: MatSelect;
  filteredAllEo:EOCatTreeVM[];
  filtered_taskEo:EOWithCountOptions[];
  constructor(
    private taskReviewActionItemSrvc:TaskReviewActionItemService,
    private fb:UntypedFormBuilder,
    private taskService:TasksService,
    private eoService:EnablingObjectivesService
  ) { }

  ngOnInit(): void {
    this.isLoading = true;
    this.loadAsync();
    this.initializeFormData();
    this.operationTypeName = this.editOperation?.operation;
  }

  initializeFormData(){
    this.enabObjOperationForm = this.fb.group({
      operation: new UntypedFormControl(this.editOperation?.operationTypeId,[Validators.required]),
      enablingObjective : new UntypedFormControl(this.editOperation?.enablingObjectiveId,[Validators.required]),
      searchText : new UntypedFormControl('')
    })
    
  }

  async loadAsync(){
    await this.getOperationTypes();
    await this.getAllEo();
    await this.getTaskEo();
    this.isLoading = false;
  }
  async getAllEo(){
    this.allEo = (await this.eoService.getMinimizedForTree());
    this.filteredAllEo = Object.assign(this.allEo);
  }

  async getTaskEo(){
    this.task_Eo = (await this.taskService.getLinkedEOWithCount(this.taskId));
    this.filtered_taskEo = Object.assign(this.task_Eo);
  }

  async getOperationTypes(){
    this.operationType = await this.taskReviewActionItemSrvc.getOperationTypesAsync(this.actionItemTypeName);
  }

  onOperationTypeChange(operationTypeId:string){
    this.enabObjOperationForm.get('searchText')?.setValue('');
    this.enabObjOperationForm.get('enablingObjective')?.setValue(null);
    const operationType = this.operationType.find(x => x.id == operationTypeId);
    this.operationTypeName = operationType?.type;
  }

  getFilteredEo(eoArray:any[]){
    const array = eoArray?.filter((x)=>{return x.active && this.task_Eo?.findIndex(m=>m.id==x.id)==-1});
    return array;
  }

  closeFlyPanel(){
    this.closed.emit();
  }

  onEOSave(){
    var eoOperationData = new TaskReviewActionItem_EnablingObjective_Operation_VM();
    eoOperationData.id = this.editOperation?.id;
    eoOperationData.operationTypeId = this.enabObjOperationForm.get('operation')?.value;
    eoOperationData.enablingObjectiveId = this.enabObjOperationForm.get('enablingObjective')?.value;
    eoOperationData.operation = this.operationType.find(x => x.id == eoOperationData.operationTypeId).type;
    this.eoOperation_Data.emit(eoOperationData);
    this.closeFlyPanel();
  }

  enablingObjectiveSearch(){
    var searchValue = this.enabObjOperationForm.get('searchText')?.value;
    if(this.operationTypeName=='RemoveLink'){
      this.filtered_taskEo = cloneDeep(this.task_Eo);
      this.filtered_taskEo = this.task_Eo.filter(x=>{
        var fullData = `${x.number} - ${x.description}`
        return fullData.toLowerCase().includes(searchValue.toLowerCase().trim())
      })
    }
    if (this.operationTypeName == 'CreateLink') {
      this.filteredAllEo = cloneDeep(this.allEo);
      this.filteredAllEo.forEach((x)=>{
        x.enablingObjective_SubCategories.forEach((m)=>{
          m.enablingObjective_Topics.forEach((k)=>{
            k.enablingObjectives = k.enablingObjectives.filter((c)=>{
              var fullData = `${x.number}.${m.number}.${k.number}.${c.number} - ${c.description}`
              return fullData.toLowerCase().includes(searchValue.toLowerCase().trim())
            })
          })
        })
      })
    }
  }

  resetSearch(){
    setTimeout(() => {
      this.enabObjOperationForm.get('searchText')?.setValue('');
      this.enablingObjectiveSearch();
    }, 500);
  }


  handleKeydown(event: KeyboardEvent) {
    this.select._handleKeydown = (event: KeyboardEvent) => {
      if (event.key === 'SPACE') return;
    };
  }

}

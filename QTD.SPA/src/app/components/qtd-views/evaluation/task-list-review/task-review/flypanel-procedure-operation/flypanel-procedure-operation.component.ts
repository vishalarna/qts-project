import { Component, EventEmitter, Input, OnInit, Output, ViewChild } from '@angular/core';
import { UntypedFormBuilder, UntypedFormControl, UntypedFormGroup, Validators } from '@angular/forms';
import { Procedure } from '@models/Procedure/Procedure';
import { Procedure_IssuingAuthority } from '@models/Procedure_IssuingAuthority/Procedure_IssuingAuthority';
import { TaskReviewActionItem_OperationType_VM } from '@models/Task_Review/TaskReviewActionItem_OperationType_VM';
import { TaskReviewActionItem_Procedure_Operation_VM } from '@models/Task_Review/TaskReviewActionItem_Procedure_Operation_VM';
import { TaskReviewActionItemService } from 'src/app/_Services/QTD/TaskReviewActionItem/api.task-review-actionItem.service';
import { IssuingAuthoritiesService } from 'src/app/_Services/QTD/issuing-authorities.service';
import { TasksService } from 'src/app/_Services/QTD/tasks.service';
import { cloneDeep } from 'lodash';
import { MatLegacySelect as MatSelect } from '@angular/material/legacy-select';

@Component({
  selector: 'app-flypanel-procedure-operation',
  templateUrl: './flypanel-procedure-operation.component.html',
  styleUrls: ['./flypanel-procedure-operation.component.scss']
})
export class FlypanelProcedureOperationComponent implements OnInit {

  @Input () editOperation:TaskReviewActionItem_Procedure_Operation_VM;
  @Output () procedureOperation_Data = new EventEmitter<TaskReviewActionItem_Procedure_Operation_VM>();
  @Input () taskId : string;
  @Output () closed = new EventEmitter<any>();
  @Input() actionItemTypeName : string;
  operationType:TaskReviewActionItem_OperationType_VM[];
  procedureOperationForm:UntypedFormGroup;
  allProcedure:Procedure_IssuingAuthority[];
  task_Procedure:Procedure[];
  operationTypeName:string;
  isLoading:boolean;
  filteredAllProcedure:Procedure_IssuingAuthority[];
  filtered_taskProcedure:Procedure[];
  @ViewChild('select', { static: false }) select!: MatSelect;
  constructor(
    private taskReviewActionItemSrvc:TaskReviewActionItemService,
    private fb:UntypedFormBuilder,
    private issuingAuthoritiesService:IssuingAuthoritiesService,
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
    await this.getIssuingAuthorityProcedure();
    await this.getTaskProcedures();
    this.isLoading = false;
  }

  initializeFormData(){
    this.procedureOperationForm = this.fb.group({
      operation: new UntypedFormControl(this.editOperation?.operationTypeId,[Validators.required]),
      procedure: new UntypedFormControl(this.editOperation?.procedureId,[Validators.required]),
      searchText : new UntypedFormControl('')
    })
  }

  async getOperationTypes(){
    this.operationType = await this.taskReviewActionItemSrvc.getOperationTypesAsync(this.actionItemTypeName);
  }

  onOperationTypeChange(operationTypeId:string){
    this.procedureOperationForm.get('searchText')?.setValue('');
    this.procedureOperationForm.get('procedure')?.setValue(null);
    const operationType = this.operationType?.find(x => x.id == operationTypeId);
    this.operationTypeName = operationType?.type;
  }

  async getIssuingAuthorityProcedure(){
    this.allProcedure = await this.issuingAuthoritiesService.getAll();
    this.filteredAllProcedure = Object.assign(this.allProcedure);
  }

  async getTaskProcedures(){
    this.task_Procedure = (await this.taskService.getLinkedProcedures(this.taskId));
    this.filtered_taskProcedure = Object.assign(this.task_Procedure);
  }

  getFilteredProcedures(procedureArray:Procedure[]){
    const array = procedureArray?.filter((x)=>{return x.active && this.task_Procedure?.findIndex(m=>m.id==x.id)==-1});
    return array;
  }

  closeFlyPanel(){
    this.closed.emit();
  }

  onProcedureSave(){
    var procedureOperationData = new TaskReviewActionItem_Procedure_Operation_VM();
    procedureOperationData.id = this.editOperation?.id;
    procedureOperationData.operationTypeId = this.procedureOperationForm.get('operation')?.value;
    procedureOperationData.procedureId = this.procedureOperationForm.get('procedure')?.value;
    procedureOperationData.operation = this.operationType.find(x => x.id == procedureOperationData.operationTypeId).type;
    this.procedureOperation_Data.emit(procedureOperationData);
    this.closeFlyPanel();
  }

  procedureSearch(){
    var searchValue = this.procedureOperationForm.get('searchText')?.value;
    if(this.operationTypeName=='RemoveLink'){
      this.filtered_taskProcedure = cloneDeep(this.task_Procedure);
      this.filtered_taskProcedure = this.task_Procedure.filter(x=>{
        var fullData = `${x.procedure_IssuingAuthority.title} - ${x.number} - ${x.title}`
        return fullData.toLowerCase().includes(searchValue.toLowerCase().trim())
      })
    }
    if (this.operationTypeName == 'CreateLink') {
      this.filteredAllProcedure = cloneDeep(this.allProcedure);
      this.filteredAllProcedure.forEach((x)=>{
        x.procedures = x.procedures.filter((m)=>{
          var fullData = `${x.title} - ${m.number} - ${m.title}`
          return fullData.toLowerCase().includes(searchValue.toLowerCase().trim())
        })
      })
    }
  }

  resetSearch(){
    setTimeout(() => {
      this.procedureOperationForm.get('searchText')?.setValue('');
      this.procedureSearch();
    }, 500);
  }


  handleKeydown(event: KeyboardEvent) {
    this.select._handleKeydown = (event: KeyboardEvent) => {
      if (event.key === 'SPACE') return;
    };
  }

}

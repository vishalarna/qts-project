import { Component, EventEmitter, Input, OnInit, Output, ViewChild } from '@angular/core';
import { UntypedFormBuilder, UntypedFormControl, UntypedFormGroup, Validators } from '@angular/forms';
import { MatLegacySelect as MatSelect } from '@angular/material/legacy-select';
import { RRIssuingAuthority } from '@models/RR_IssuingAuthority/RR_IssuingAuthority';
import { TaskRRWithCount } from '@models/Task/TaskRRWithCount';
import { TaskReviewActionItem_OperationType_VM } from '@models/Task_Review/TaskReviewActionItem_OperationType_VM';
import { TaskReviewActionItem_RegulatoryRequirement_Operation_VM } from '@models/Task_Review/TaskReviewActionItem_RegulatoryRequirement_Operation_VM';
import { TaskReviewActionItemService } from 'src/app/_Services/QTD/TaskReviewActionItem/api.task-review-actionItem.service';
import { RRIssuingAuthorityService } from 'src/app/_Services/QTD/rr-issuing-authority.service';
import { TasksService } from 'src/app/_Services/QTD/tasks.service';
import { cloneDeep } from 'lodash';

@Component({
  selector: 'app-flypanel-regulatory-requirement-operation',
  templateUrl: './flypanel-regulatory-requirement-operation.component.html',
  styleUrls: ['./flypanel-regulatory-requirement-operation.component.scss']
})
export class FlypanelRegulatoryRequirementOperationComponent implements OnInit {

  @Input () editOperation:TaskReviewActionItem_RegulatoryRequirement_Operation_VM;
  @Output () rrOperation_Data = new EventEmitter<TaskReviewActionItem_RegulatoryRequirement_Operation_VM>();
  @Input () taskId : string;
  @Output () closed = new EventEmitter<any>();
  @Input() actionItemTypeName : string;
  operationType:TaskReviewActionItem_OperationType_VM[];
  rrOperationForm:UntypedFormGroup;
  task_RR:TaskRRWithCount[];
  allRegulatoryRequirements:RRIssuingAuthority[];
  operationTypeName:string;
  isLoading:boolean;
  filtered_TaskRR:TaskRRWithCount[];
  filtered_AllRR:RRIssuingAuthority[];
  @ViewChild('select', { static: false }) select!: MatSelect;
  constructor(
    private taskReviewActionItemSrvc:TaskReviewActionItemService,
    private fb:UntypedFormBuilder,
    private taskService:TasksService,
    private rrIssuingService:RRIssuingAuthorityService
  ) { }

  ngOnInit(): void {
    this.isLoading = true;
    this.loadAsync();
    this.initializeFormData();
    this.operationTypeName = this.editOperation?.operation;
  }

  async loadAsync(){
    await this.getOperationTypes();
    await this.getAllRegulatoryRequirements();
    await this.getTaskRR();
    this.isLoading = false;
  }

  initializeFormData(){
    this.rrOperationForm = this.fb.group({
      operation: new UntypedFormControl(this.editOperation?.operationTypeId,[Validators.required]),
      regulatoryRequirement: new UntypedFormControl(this.editOperation?.regulatoryRequirementId,[Validators.required]),
      searchText : new UntypedFormControl('')
    })
  }

  async getAllRegulatoryRequirements(){
    this.allRegulatoryRequirements = await this.rrIssuingService.getAll();
    this.filtered_AllRR = Object.assign(this.allRegulatoryRequirements);
  }

  async getTaskRR(){
    this.task_RR = (await this.taskService.getLinkedRRWithCount(this.taskId));
    this.filtered_TaskRR = Object.assign(this.task_RR);
  }

  async getOperationTypes(){
    this.operationType = await this.taskReviewActionItemSrvc.getOperationTypesAsync(this.actionItemTypeName);
  }

  onOperationTypeChange(operationTypeId:string){
    this.rrOperationForm.get('searchText')?.setValue('');
    this.rrOperationForm.get('regulatoryRequirement')?.setValue(null);
    const operationType = this.operationType?.find(x => x.id == operationTypeId);
    this.operationTypeName = operationType?.type;
  }

  getFiltererdRR(rrArray:any[]){
    const array = rrArray?.filter((x)=>{return x.active && this.task_RR?.findIndex(m=>m.id==x.id)==-1});
    return array;
  }

  closeFlyPanel(){
    this.closed.emit();
  }

  onRRsave(){
    var rrOperationData = new TaskReviewActionItem_RegulatoryRequirement_Operation_VM();
    rrOperationData.id = this.editOperation?.id;
    rrOperationData.operationTypeId = this.rrOperationForm.get('operation')?.value;
    rrOperationData.regulatoryRequirementId = this.rrOperationForm.get('regulatoryRequirement')?.value;
    rrOperationData.operation = this.operationType.find(x => x.id == rrOperationData.operationTypeId).type;
    this.rrOperation_Data.emit(rrOperationData);
    this.closeFlyPanel();
  }

  regulatoryRequirementSearch(){
    var searchValue = this.rrOperationForm.get('searchText')?.value;
    if(this.operationTypeName=='RemoveLink'){
      this.filtered_TaskRR = cloneDeep(this.task_RR);
      this.filtered_TaskRR = this.task_RR.filter(x=>{
        var fullData = `${x.rR_IssuingAuthority_Title} - ${x.number} - ${x.description}`
        return fullData.toLowerCase().includes(searchValue.toLowerCase().trim())
      })
    }
    if (this.operationTypeName == 'CreateLink') {
      this.filtered_AllRR = cloneDeep(this.allRegulatoryRequirements);
      this.filtered_AllRR.forEach((x)=>{
        x.regulatoryRequirements = x.regulatoryRequirements.filter((m)=>{
          var fullData = `${x.title} - ${m.number} - ${m.title}`
          return fullData.toLowerCase().includes(searchValue.toLowerCase().trim())
        })
      })
    }
  }

  resetSearch(){
    setTimeout(() => {
      this.rrOperationForm.get('searchText')?.setValue('');
      this.regulatoryRequirementSearch();
    }, 500);
  }


  handleKeydown(event: KeyboardEvent) {
    this.select._handleKeydown = (event: KeyboardEvent) => {
      if (event.key === 'SPACE') return;
    };
  }

}

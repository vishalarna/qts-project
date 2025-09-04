import { Component, EventEmitter, Input, OnInit, Output, ViewChild } from '@angular/core';
import { UntypedFormBuilder, UntypedFormControl, UntypedFormGroup, Validators } from '@angular/forms';
import { MatLegacySelect as MatSelect } from '@angular/material/legacy-select';
import { DutyArea } from '@models/DutyArea/DutyArea';
import { SubdutyArea } from '@models/SubdutyArea/SubdutyArea';
import { Task } from '@models/Task/Task';
import { TaskReviewActionItem_OperationType_VM } from '@models/Task_Review/TaskReviewActionItem_OperationType_VM';
import { TaskReviewActionItem_SubDutyArea_Operation_VM } from '@models/Task_Review/TaskReviewActionItem_SubDutyArea_Operation_VM';
import { TaskReviewActionItemService } from 'src/app/_Services/QTD/TaskReviewActionItem/api.task-review-actionItem.service';
import { DutyAreaService } from 'src/app/_Services/QTD/duty-area.service';
import { TasksService } from 'src/app/_Services/QTD/tasks.service';

@Component({
  selector: 'app-flypanel-subduty-operation',
  templateUrl: './flypanel-subduty-operation.component.html',
  styleUrls: ['./flypanel-subduty-operation.component.scss']
})
export class FlypanelSubdutyOperationComponent implements OnInit {

  @Input () editOperation:TaskReviewActionItem_SubDutyArea_Operation_VM;
  @Output () subDutyOperationData = new EventEmitter<TaskReviewActionItem_SubDutyArea_Operation_VM>()
  @Output () closed = new EventEmitter<any>();
  @Input() actionItemTypeName : string;
  @Input() task_Details : Task;
  operationType:TaskReviewActionItem_OperationType_VM[];
  subDutyForm:UntypedFormGroup;
  subDutyAreas: SubdutyArea[] = [];
  dutyAreas: DutyArea[] = [];
  editedDutyAreaId:string;
  isLoading:boolean;
  @ViewChild('select', { static: false }) select!: MatSelect;
  @ViewChild('subDutySelect', { static: false }) subDutySelect!: MatSelect;
  filteredDutyArea: DutyArea[];
  filteredSubDutyArea:SubdutyArea[];
  constructor(
    private taskReviewActionItemSrvc:TaskReviewActionItemService,
    public taskService: TasksService,
    private fb:UntypedFormBuilder,
    private daSrvc: DutyAreaService,
  ) { }

  ngOnInit(): void {
    this.isLoading = true;
    this.loadData();
    this.initializeFormData();
  }

  async loadData(){
    await this.getOperationTypes();
    await this.getallDutyAreas();
    this.isLoading = false;
  }

  initializeFormData(){
    this.subDutyForm = this.fb.group({
      operation:new UntypedFormControl(null,[Validators.required]),
      dutyArea:new UntypedFormControl(null, [Validators.required]),
      subDutyArea:new UntypedFormControl(null, [Validators.required]),
      searchText : new UntypedFormControl('')
    })
  }

  updateFormData(){
    this.subDutyForm.patchValue({
      operation: this.editOperation?.operationTypeId,
      dutyArea:this.editedDutyAreaId,
      subDutyArea:this.editOperation?.subDutyAreaId
    });
  }

  async getOperationTypes(){
    this.operationType = await this.taskReviewActionItemSrvc.getOperationTypesAsync(this.actionItemTypeName);
  }

  async getSubDutyAreas(id: any) {
    await this.daSrvc.getSubDutyAreasByDutyArea(id).then((res) => {
      this.subDutyAreas = res.filter(c=>c.active);
      this.filteredSubDutyArea = Object.assign(this.subDutyAreas);
    });
  }

  async getDutyAreaId(){
    let dutyArea = this.dutyAreas.find(x=>x.subdutyAreas.some(y=>y.id == this.editOperation?.subDutyAreaId));
    if(dutyArea != null){
      this.editedDutyAreaId = dutyArea.id;
      await this.getSubDutyAreas(this.editedDutyAreaId);
    }
    this.updateFormData();
  }

  async getallDutyAreas() {
    await this.daSrvc.getWithSubdutyAreas().then(async(res) => {
      this.dutyAreas = res.filter(m=>m.active);
      this.filteredDutyArea = Object.assign(this.dutyAreas)
      if(this.editOperation?.subDutyAreaId){
        await this.getDutyAreaId();
     }else{
       if(this.task_Details?.subdutyArea.dutyAreaId){
        this.getSubDutyAreas(this.task_Details?.subdutyArea.dutyAreaId);
      }
      this.subDutyForm.patchValue({ dutyArea: this.task_Details?.subdutyArea.dutyAreaId, subDutyArea: this.task_Details?.subdutyAreaId });
     }
    });
  }

  async setTaskNumber(event: any) {
    await this.taskService
      .getTaskNumberWithLetter(event)
      .then((res: any) => {
      })
  }

  closeFlyPanel(){
    this.closed.emit();
  }

  saveSubDutyOperation(){
    var subDutyAreaOperationData = new TaskReviewActionItem_SubDutyArea_Operation_VM();
    subDutyAreaOperationData.id = this.editOperation?.id;
    subDutyAreaOperationData.operationTypeId =  this.subDutyForm.get('operation')?.value;
    subDutyAreaOperationData.subDutyAreaId =   this.subDutyForm.get('subDutyArea')?.value;
    subDutyAreaOperationData.operation = this.operationType.find(x => x.id == subDutyAreaOperationData.operationTypeId).type;
      this.subDutyOperationData.emit(subDutyAreaOperationData);
      this.closeFlyPanel();
  }

  dutyAreaSearch(){
    var searchValue = this.subDutyForm.get('searchText')?.value;
    this.filteredDutyArea = this.dutyAreas.filter((x)=>{
      var fullData = `${x.letter} - ${x.number} - ${x.title}`
      return fullData.toLowerCase().includes(searchValue.toLowerCase().trim())
    })
  }

  subDutyAreaSearch(){
    var searchValue = this.subDutyForm.get('searchText')?.value;
    this.filteredSubDutyArea = this.subDutyAreas.filter(x=>{
      var fullData = `${x.subNumber} - ${x.title}`
      return fullData.toLowerCase().includes(searchValue.toLowerCase().trim())
    })
  }

  resetSearch(){
    setTimeout(() => {
      this.subDutyForm.get('searchText')?.setValue('');
      this.dutyAreaSearch();
      this.subDutyAreaSearch();
    }, 500);
  }


  handleKeydown(event: KeyboardEvent) {
    this.select._handleKeydown = (event: KeyboardEvent) => {
      if (event.key === 'SPACE') return;
    };
  }

  handleKeyDownSd(event: KeyboardEvent) {
    this.subDutySelect._handleKeydown = (event: KeyboardEvent) => {
      if (event.key === 'SPACE') return;
    };
  }

}

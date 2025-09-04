import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { UntypedFormBuilder, UntypedFormControl, UntypedFormGroup } from '@angular/forms';
import { Position } from '@models/Position/Position';
import { PositionsService } from 'src/app/_Services/QTD/positions.service';

@Component({
  selector: 'app-flypanel-filter-task-list-review-tasks',
  templateUrl: './flypanel-filter-task-list-review-tasks.component.html',
  styleUrls: ['./flypanel-filter-task-list-review-tasks.component.scss']
})
export class FlypanelFilterTaskListReviewTasksComponent implements OnInit {

  @Input() filterTaskValues : any;
  @Output() filterType = new EventEmitter<any>();
  @Output() closed = new EventEmitter<boolean>();
  positions:Position[];
  taskIds:string[]=[];
  filterFormGroup:UntypedFormGroup;
  isLoading:boolean;
  constructor(
    private posService:PositionsService,
    private fb:UntypedFormBuilder
  ) { }

  ngOnInit(): void {
    this.isLoading = true;
    this.getAllActivePositions();
    this.getFilterTaskValue();
    this.initializeFilterForm();
  }

  getFilterTaskValue() {
    this.isLoading = true;
    setTimeout(() => {
      this.initializeFilterForm();
      this.isLoading = false; 
    }, 1000);
  }

  initializeFilterForm(){
    this.filterFormGroup = this.fb.group({
      position: new UntypedFormControl(this.filterTaskValues?.position),
      taskFilterBy:new UntypedFormControl(this.filterTaskValues?.taskFilterBy),
    })
    this.taskIds = this.filterTaskValues?.taskIds ?? [];
  }

  async getAllActivePositions(){
    this.positions = (await this.posService.getPositionTaskAsync()).filter(x=>x.active);
  }

  closeFilterFlyPanel(){
    this.closed.emit(false);
  }

  filterBy(s: string) {
    this.filterFormGroup.get('taskFilterBy').setValue(s);
  }

  getFilterRadioValue(){
    return this.filterFormGroup.get("taskFilterBy").value;
  }

  positionClick(e:any){
    this.filterFormGroup.get('position').setValue(e.value)
    this.filterFormGroup.get('taskFilterBy').setValue('allWithPosition');
    var position = this.positions.find(x=>x.id == e.value);
    if(position != null){
      this.taskIds = Array.from(new Set(position.position_Tasks.map(x=>x.taskId)));
    }
    else{
      this.taskIds = [];
    }
  }

  clearPositionSelection(){
    this.filterFormGroup.get('position').setValue(null);
    this.filterFormGroup.get('taskFilterBy').setValue(null);
  }

  getFilterValue(){
    this.filterTaskValues = {
      position : this.filterFormGroup.get("position").value,
      taskFilterBy: this.filterFormGroup.get("taskFilterBy").value
    }
  }

  filterTask(){
    this.getFilterValue();
    this.filterType.emit({...this.filterTaskValues, taskIds : this.taskIds});
    this.closed.emit(false);
  }
}

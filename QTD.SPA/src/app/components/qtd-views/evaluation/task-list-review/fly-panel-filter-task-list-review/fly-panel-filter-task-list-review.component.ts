import { DatePipe } from '@angular/common';
import { Component, EventEmitter, Input, OnInit, Output, ViewChild } from '@angular/core';
import { UntypedFormBuilder, UntypedFormControl, UntypedFormGroup } from '@angular/forms';
import { MatLegacySelect as MatSelect } from '@angular/material/legacy-select';
import { TaskListReviewStatus_VM } from '@models/TaskListReview/TaskListReviewStatus_VM';
import { TaskListReviewType_VM } from '@models/TaskListReview/TaskListReviewType_VM';
import { TaskReviewStatusVM } from '@models/Task_Review/TaskReviewStatusVM';
import { ApiTaskListReviewStatusService } from 'src/app/_Services/QTD/TaskListReviewStatus/api.tasklistreviewstatus.service';
import { ApiTaskListReviewTypeService } from 'src/app/_Services/QTD/TaskListReviewType/api.tasklistreviewtype.service';
import { ApiTaskReviewService } from 'src/app/_Services/QTD/TaskReview/api.taskReview.service';
import { PositionsService } from 'src/app/_Services/QTD/positions.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { cloneDeep } from 'lodash';

@Component({
  selector: 'app-fly-panel-filter-task-list-review',
  templateUrl: './fly-panel-filter-task-list-review.component.html',
  styleUrls: ['./fly-panel-filter-task-list-review.component.scss']
})
export class FlyPanelFilterTaskListReviewComponent implements OnInit {
  @Input()  taskListReviewFilterValue:any;
  @Output() taskListReviewFilterChange = new EventEmitter<any>();
  filterTaskListReviewForm: UntypedFormGroup;
  taskListReviewTypes : TaskListReviewType_VM[] = [];
  taskListReviewStatuses : TaskListReviewStatus_VM[] = [];
  taskReviewStatuses : TaskReviewStatusVM[] = [];
  statuses : string[] = ["Active","Inactive"];
  allPositions:any[] = [];
  originalPositions:any[] = [];
  @ViewChild('positionSelect', { static: false }) positionSelect!: MatSelect;

  constructor(  private fb: UntypedFormBuilder,
    public flyPanelService: FlyInPanelService,
    public taskListReviewTypeService : ApiTaskListReviewTypeService,
    public taskListReviewStatusService : ApiTaskListReviewStatusService,
    public taskReviewService : ApiTaskReviewService,
    public datePipe : DatePipe,
    private positionService:PositionsService
    ) { }

  ngOnInit(): void {
    this.initializeTaskListReviewForm();
    this.loadAsync();
  }

  initializeTaskListReviewForm() {
    this.filterTaskListReviewForm = this.fb.group({
      type: new UntypedFormControl(this.taskListReviewFilterValue?.type),
      reviewStatus: new UntypedFormControl(this.taskListReviewFilterValue?.reviewStatus),
      reviewPeriodStart: new UntypedFormControl(this.datePipe.transform(this.taskListReviewFilterValue?.reviewPeriodStart, 'yyyy-MM-dd')),
      reviewPeriodEnd: new UntypedFormControl(this.datePipe.transform(this.taskListReviewFilterValue?.reviewPeriodEnd, 'yyyy-MM-dd')),
      status: new UntypedFormControl(this.taskListReviewFilterValue?.status),
      reviewApprovalDate: new UntypedFormControl(this.datePipe.transform(this.taskListReviewFilterValue?.reviewApprovalDate, 'yyyy-MM-dd')),
      taskReviewStatus: new UntypedFormControl(this.taskListReviewFilterValue?.taskReviewStatus),
      onlyMyTaskReviews: new UntypedFormControl(this.taskListReviewFilterValue?.onlyMyTaskReviews),
      position:new UntypedFormControl(this.taskListReviewFilterValue?.position),
      positionSearchText:new UntypedFormControl('')
    });
    
  }
  async loadAsync(){
    this.taskListReviewTypes = await this.taskListReviewTypeService.getAllAsync();
    this.taskListReviewStatuses = await this.taskListReviewStatusService.getAllAsync();
    this.taskReviewStatuses = await this.taskReviewService.getAllStatusAsync();
    await this.getAllPositions();
  }

  closeFlyPanel() {
    this.flyPanelService.close();
  }

  applyFiltersClick(){
    this.taskListReviewFilterChange.emit(this.filterTaskListReviewForm.value);
    this.flyPanelService.close();
  }
  
  clearSelection(name : string) {
    this.filterTaskListReviewForm.get(name)?.setValue(null);
  }

  async getAllPositions(){
    this.allPositions = await this.positionService.getActiveInactiveList('active');
      this.allPositions?.sort((a, b) => {
      return a.positionTitle.localeCompare(b.positionTitle);
    });
    this.originalPositions = cloneDeep(this.allPositions);
  }

  resetSearch(){
    setTimeout(() => {
      this.filterTaskListReviewForm.get('positionSearchText')?.setValue('');
      this.positionSearch();
    }, 500);
  }
  
  
  handleKeydown(event: KeyboardEvent) {
    this.positionSelect._handleKeydown = (event: KeyboardEvent) => {
      if (event.key === 'SPACE') return;
    };
  }
  
  positionSearch(){
      var searchValue = this.filterTaskListReviewForm.get('positionSearchText')?.value;
      if (searchValue !== undefined && searchValue !== null) {
        searchValue = String(searchValue).toLowerCase();
      } else {
        searchValue = "";
      }
      this.allPositions =  this.originalPositions.filter((x) => {
          return x.positionTitle.trim().toLowerCase().includes(searchValue)
      });
  }
  
  positionFilterSearch(){
      this.filterTaskListReviewForm.get('positionSearchText')?.setValue('');
      this.positionSearch();
  }
}

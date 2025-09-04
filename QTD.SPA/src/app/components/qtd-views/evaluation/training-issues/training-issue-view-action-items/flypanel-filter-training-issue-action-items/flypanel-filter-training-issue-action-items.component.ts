import { Component, EventEmitter, Input, OnInit, Output, ViewChild } from '@angular/core';
import { UntypedFormBuilder, UntypedFormControl, UntypedFormGroup, Validators } from '@angular/forms';
import { TrainingIssue_ActionItemPriority_VM } from '@models/TrainingIssues/TrainingIssue_ActionItemPriority_VM';
import { TrainingIssue_ActionItemStatus_VM } from '@models/TrainingIssues/TrainingIssue_ActionItemStatus_VM';
import { TrainingIssuesService } from 'src/app/_Services/QTD/TrainingIssues/training-issues.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';

@Component({
  selector: 'app-flypanel-filter-training-issue-action-items',
  templateUrl: './flypanel-filter-training-issue-action-items.component.html',
  styleUrls: ['./flypanel-filter-training-issue-action-items.component.scss']
})
export class FlypanelFilterTrainingIssueActionItemsComponent implements OnInit {

  @Input()  actionItemFilterValue:any;
  @Output() actionItemFilterChange = new EventEmitter<any>();
  filterActionItemForm:UntypedFormGroup;
  actionItemPriorities:TrainingIssue_ActionItemPriority_VM[];
  actionItemStatuses:TrainingIssue_ActionItemStatus_VM[];
  isLoading:boolean;
  constructor(
    private flyPanelService:FlyInPanelService,
    private fb:UntypedFormBuilder,
    private apiTrainingIssueService:TrainingIssuesService
  ) { }

  ngOnInit(): void {
    this.isLoading = true;
    this.loadAsync();
    this.initializeActionItemForm();
  }
  async loadAsync(){
    await this.getAllPriorities();
    await this.getActionItemStatuses();
    this.isLoading = false;
  }

  async getAllPriorities(){
    this.actionItemPriorities = await this.apiTrainingIssueService.getAllActionItemPrioritiesAsync();
  }

  async getActionItemStatuses(){
    this.actionItemStatuses = await this.apiTrainingIssueService.getAllTrainingIssueActionItemStatusAsync();
  }

  initializeActionItemForm(){
    this.filterActionItemForm = this.fb.group({
      assignedToId: new UntypedFormControl(this.actionItemFilterValue?.assignedToId),
      priorityId: new UntypedFormControl(this.actionItemFilterValue?.priorityId),
      dateAssigned: new UntypedFormControl(this.actionItemFilterValue?.dateAssigned),
      dueDate: new UntypedFormControl(this.actionItemFilterValue?.dueDate),
      dateCompleted: new UntypedFormControl(this.actionItemFilterValue?.dateCompleted),
      statusId: new UntypedFormControl(this.actionItemFilterValue?.statusId),
      assigneeName:new UntypedFormControl(this.actionItemFilterValue?.assigneeName)
    })
  }
  
  closeFlyPanel(){
    this.flyPanelService.close();
  }

  clearSelection(name : string) {
    this.filterActionItemForm.get(name)?.setValue(null);
  }

  applyFilterClick(){
    this.actionItemFilterChange.emit(this.filterActionItemForm.value);
    this.closeFlyPanel();
  }

}

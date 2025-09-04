import { Component, EventEmitter, OnInit, Output, ViewChild } from '@angular/core';
import { UntypedFormBuilder, UntypedFormControl, UntypedFormGroup, Validators } from '@angular/forms';
import { TrainingIssue_ActionItemPriority_VM } from '@models/TrainingIssues/TrainingIssue_ActionItemPriority_VM';
import { TrainingIssue_ActionItemStatus_VM } from '@models/TrainingIssues/TrainingIssue_ActionItemStatus_VM';
import { TrainingIssue_ActionItem_VM } from '@models/TrainingIssues/TrainingIssue_ActionItem_VM';
import { TrainingIssuesService } from 'src/app/_Services/QTD/TrainingIssues/training-issues.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';

@Component({
  selector: 'app-flypanel-add-training-issue-action-item',
  templateUrl: './flypanel-add-training-issue-action-item.component.html',
  styleUrls: ['./flypanel-add-training-issue-action-item.component.scss']
})
export class FlypanelAddTrainingIssueActionItemComponent implements OnInit {

  @Output () actionItemDetails = new EventEmitter<TrainingIssue_ActionItem_VM>();
  actionItemForm:UntypedFormGroup;
  priorities:string[];
  actionItemStatuses:TrainingIssue_ActionItemStatus_VM[];
  actionItemPriorities:TrainingIssue_ActionItemPriority_VM[];
  constructor(
    private flyPanelService:FlyInPanelService,
    private fb:UntypedFormBuilder,
    private apiTrainingIssueService:TrainingIssuesService
  ) { }

  ngOnInit(): void {
    this.loadAsync();
    this.initializeActionItemForm();
  }

  async loadAsync(){
    await this.getActionItemStatuses();
    await this.getAllPriorities();
  }

  async getActionItemStatuses(){
    this.actionItemStatuses = await this.apiTrainingIssueService.getAllTrainingIssueActionItemStatusAsync();
  }

  async getAllPriorities(){
    this.actionItemPriorities = await this.apiTrainingIssueService.getAllActionItemPrioritiesAsync();
  }

  initializeActionItemForm(){
    this.actionItemForm = this.fb.group({
      actionStepName: new UntypedFormControl(null,[Validators.required]),
      priority: new UntypedFormControl(null),
      dateAssigned: new UntypedFormControl(null),
      dueDate: new UntypedFormControl(null),
      dateCompleted: new UntypedFormControl(null),
      status: new UntypedFormControl(null),
      notes: new UntypedFormControl(null),
      assigneeName: new UntypedFormControl(null),
      addAnother: new UntypedFormControl(false)
    })
  }

  onActionItemSave(){
    var actionItemData = new TrainingIssue_ActionItem_VM();
    actionItemData.actionItemName = this.actionItemForm.get('actionStepName')?.value;
    actionItemData.priorityId = this.actionItemForm.get('priority')?.value;
    actionItemData.dateAssigned = this.actionItemForm.get('dateAssigned')?.value;
    actionItemData.dueDate = this.actionItemForm.get('dueDate')?.value;
    actionItemData.dateCompleted = this.actionItemForm.get('dateCompleted')?.value;
    actionItemData.statusId = this.actionItemForm.get('status')?.value;
    actionItemData.notes = this.actionItemForm.get('notes')?.value;
    actionItemData.assigneeName = this.actionItemForm.get('assigneeName')?.value;
    this.actionItemDetails.emit(actionItemData);
    if(this.actionItemForm.get('addAnother')?.value){
      this.actionItemForm.reset();
    }else{
      this.closeFlyPanel();
    }
  }

  closeFlyPanel(){
    this.flyPanelService.close();
  }

}

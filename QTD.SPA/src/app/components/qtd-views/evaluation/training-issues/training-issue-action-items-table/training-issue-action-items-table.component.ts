import { moveItemInArray } from '@angular/cdk/drag-drop';
import { Component, Input, OnInit, ViewContainerRef } from '@angular/core';
import { TrainingIssue_ActionItemPriority_VM } from '@models/TrainingIssues/TrainingIssue_ActionItemPriority_VM';
import { TrainingIssue_ActionItemStatus_VM } from '@models/TrainingIssues/TrainingIssue_ActionItemStatus_VM';
import { TrainingIssue_ActionItem_VM } from '@models/TrainingIssues/TrainingIssue_ActionItem_VM';
import { TrainingIssue_VM } from '@models/TrainingIssues/TrainingIssue_VM';
import { TrainingIssuesService } from 'src/app/_Services/QTD/TrainingIssues/training-issues.service';
import { cloneDeep } from 'lodash';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { TemplatePortal } from '@angular/cdk/portal';
import { MatLegacyTableDataSource as MatTableDataSource } from '@angular/material/legacy-table';

@Component({
  selector: 'app-training-issue-action-items-table',
  templateUrl: './training-issue-action-items-table.component.html',
  styleUrls: ['./training-issue-action-items-table.component.scss']
})
export class TrainingIssueActionItemsTableComponent implements OnInit {

  @Input () mode:'edit' | 'view';
  @Input () trainingIssueDetail:TrainingIssue_VM;
  tableColumns: string[];
  actionItemPriorities:TrainingIssue_ActionItemPriority_VM[];
  actionItemStatuses:TrainingIssue_ActionItemStatus_VM[];
  filterValues:TrainingIssue_ActionItem_VM;
  actionItemDataSource:MatTableDataSource<TrainingIssue_ActionItem_VM> = new MatTableDataSource();
  constructor(
    private apiTrainingIssueService: TrainingIssuesService,
    private vcf: ViewContainerRef,
    private flyPanelService:FlyInPanelService,
  ) { }

  ngOnInit(): void {
    this.loadAsync();
    this.tableColumns = ["drag","actionStep","assignedTo","priority","dateAssigned","dueDate","dateCompleted","status","notes","action"];
    this.actionItemDataSource = new MatTableDataSource(this.trainingIssueDetail?.actionItems ?? []);
  }

  async loadAsync(){
    await this.getActionItemStatuses();
    await this.getAllPriorities();
  }

  async getAllPriorities(){
    this.actionItemPriorities = await this.apiTrainingIssueService.getAllActionItemPrioritiesAsync();
  }

  async getActionItemStatuses(){
    this.actionItemStatuses = await this.apiTrainingIssueService.getAllTrainingIssueActionItemStatusAsync();
  }

  getStatusLabel(statusId: string): string {
    const status = this.actionItemStatuses?.find(item => item.id === statusId);
    return status ? status.status : '';
  }

  getPriorityLabel(priorityId: string): string {
    const priority = this.actionItemPriorities?.find(item => item.id === priorityId);
    return priority ? priority.priority : '';
  }

  async dropTable(event: any) {
    moveItemInArray(this.trainingIssueDetail.actionItems, event.previousIndex, event.currentIndex);
    this.trainingIssueDetail.actionItems.forEach((item, index) => {
      item.order = index + 1;
    });
    this.actionItemDataSource.data = this.trainingIssueDetail?.actionItems;
  }

  openFlyInPanel(templateRef: any) {
    const portal = new TemplatePortal(templateRef, this.vcf, {sideBarBackDrop: true,});
    this.flyPanelService.open(portal);
  }

  setFilters(values:any){
    this.filterValues = values;
    this.getActionItemFilterValues();
  }

  getActionItemFilterValues(){
    let filteredActionItem = cloneDeep(this.trainingIssueDetail?.actionItems);
    if(this.filterValues?.priorityId){
      filteredActionItem = filteredActionItem.filter(x => x.priorityId == this.filterValues.priorityId);
    }
    if (this.filterValues?.assigneeName) {
      const searchWord = this.filterValues.assigneeName.toLowerCase();
      filteredActionItem = filteredActionItem.filter(x => 
        x.assigneeName && x.assigneeName.toLowerCase().includes(searchWord)
      );
    }
    if(this.filterValues?.statusId){
      filteredActionItem = filteredActionItem.filter(x => x.statusId == this.filterValues.statusId);
    }
    if(this.filterValues?.dateAssigned){
      filteredActionItem = filteredActionItem.filter(x => this.trimToDate(x.dateAssigned) == this.filterValues.dateAssigned.toString());
    }
    if(this.filterValues?.dateCompleted){
      filteredActionItem = filteredActionItem.filter(x => this.trimToDate(x.dateCompleted) == this.filterValues.dateCompleted.toString());
    }
    if(this.filterValues?.dueDate){
      filteredActionItem = filteredActionItem.filter(x => this.trimToDate(x.dueDate) == this.filterValues.dueDate.toString());
    }
    this.actionItemDataSource.data = filteredActionItem;
  }

  getActionItemDetails(data:TrainingIssue_ActionItem_VM){
    const order = this.trainingIssueDetail.actionItems.length + 1;
    const newActionItem:TrainingIssue_ActionItem_VM = { ...data, order:order,priority:this.getPriorityLabel(data.priorityId),status:this.getStatusLabel(data.statusId)};
    this.trainingIssueDetail.actionItems.push(newActionItem);
    this.actionItemDataSource.data = this.trainingIssueDetail?.actionItems;
  }

  deleteActionItem(id:string){
    var deletedActionItemIndex = this.trainingIssueDetail.actionItems.findIndex(x=>x.id==id);
    this.trainingIssueDetail.actionItems.splice(deletedActionItemIndex,1);
    this.actionItemDataSource.data = this.trainingIssueDetail?.actionItems;
  }

  formatDateToYyyyMmDd(dateString: string): string {
    const date = new Date(dateString);
  
    const year = date.getUTCFullYear();
    const month = ('0' + (date.getUTCMonth() + 1)).slice(-2);
    const day = ('0' + date.getUTCDate()).slice(-2);
  
    return `${year}-${month}-${day}`;
  }
  

  actionItemNameChange(event:any,actionItem:TrainingIssue_ActionItem_VM){
    var inputName = event.target.value;
    actionItem.actionItemName = inputName;
  }

  actionItemNoteChange(event:any,actionItem:TrainingIssue_ActionItem_VM){
    var inputNote = event.target.value;
    actionItem.notes = inputNote;
  }

  assigneeNameChange(event:any,actionItem:TrainingIssue_ActionItem_VM){
    var inputName = event.target.value;
    actionItem.assigneeName = inputName;
  }

  onPrioritySelect(event:any,actionItem:TrainingIssue_ActionItem_VM){
    var inputPriority = event.value;
    actionItem.priorityId = inputPriority;
  }

  onAssignedDateChange(event:any,actionItem:TrainingIssue_ActionItem_VM){
    var inputAssignedDate = event.target.value;
    actionItem.dateAssigned = inputAssignedDate;
  }

  onDueDateChange(event:any,actionItem:TrainingIssue_ActionItem_VM){
    var inputDueDate = event.target.value;
    actionItem.dueDate = inputDueDate;
  }

  onCompletedDateChange(event:any,actionItem:TrainingIssue_ActionItem_VM){
    var inputCompletedDate = event.target.value;
    actionItem.dateCompleted = inputCompletedDate;
  }

  onStatusSelect(event:any,actionItem:TrainingIssue_ActionItem_VM){
    var inputStatus = event.value;
    actionItem.statusId = inputStatus;
  }

  trimToDate(dateTimeString: Date): string {
    return dateTimeString.toString().split('T')[0];
  }

}

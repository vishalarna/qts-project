import { TemplatePortal } from '@angular/cdk/portal';
import { Component, Input, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { MatLegacyDialog as MatDialog } from '@angular/material/legacy-dialog';
import { MatSort, Sort } from '@angular/material/sort';
import { MatLegacyTableDataSource as MatTableDataSource } from '@angular/material/legacy-table';
import { TaskListReview_TaskReviewActionItem_VM } from '@models/TaskListReview/TaskListReview_TaskReviewActionItem_VM';
import { TaskListReview_TaskReview_VM } from '@models/TaskListReview/TaskListReview_TaskReview_VM';
import { TaskListReview_VM } from '@models/TaskListReview/TaskListReview_VM';
import { TaskReviewActionItem_VM } from '@models/Task_Review/TaskReviewActionItem_VM';
import { DynamicLabelReplacementPipe } from 'src/app/_Pipes/dynamic-label-replacement.pipe';
import { TaskReviewActionItemService } from 'src/app/_Services/QTD/TaskReviewActionItem/api.task-review-actionItem.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';

@Component({
  selector: 'app-action-items',
  templateUrl: './action-items.component.html',
  styleUrls: ['./action-items.component.scss']
})
export class ActionItemsComponent implements OnInit {

  @Input () inputTaskListReviewVM : TaskListReview_VM;
  expandedData: TaskListReview_TaskReview_VM | null;
  public openExpandedData: boolean;
  taskActionItemsDisplayColumn:string[];
  actionItemsDetailDisplayColumns:string[];
  taskReviewDetail:TaskListReview_TaskReview_VM;
  taskReviewData:TaskListReview_TaskReview_VM;
  @ViewChild(MatSort) sort: MatSort;
  @ViewChild('innerSort') innerSort: MatSort;
  @ViewChild('deleteActionItemDialog') deleteActionItemDialog: any;
  deletedActionItemId:string;
  selectedActionItemId:string;
  selectedActionItemType:string;
  constructor(
    private actionItemSrvc:TaskReviewActionItemService,
    private alert:SweetAlertService,
    private dynamicLabelPipe:DynamicLabelReplacementPipe,
    private flyPanelService : FlyInPanelService,
    public dialog :MatDialog
  ) { }

  ngOnInit(): void {
    this.expandedData = null;
    this.taskReviewData = null;
    this.openExpandedData = false;
    this.taskActionItemsDisplayColumn = ['#','number','statement','noOfActionItems','action'];
    this.actionItemsDetailDisplayColumns = ['actionsItems','assignees','priority','dateAssigned','dueDate','actions'];
  }
  openActionItemFlyIn(templateRef: TemplateRef<any>) {
    const portal = new TemplatePortal(templateRef, null);
    this.flyPanelService.open(portal);
  }

  getTaskReviewDataSource(){
    return new MatTableDataSource(this.inputTaskListReviewVM?.taskReviews);
  }
  
  getActionItemDataSource(){
    return new MatTableDataSource(this.expandedData.taskReviewActionItems);
  }

  async deleteActionItem(id:string){
    var result = await this.actionItemSrvc.deleteAsync(id);
    var deletedIndex = this.expandedData.taskReviewActionItems.findIndex(x=>x.id==id);
    this.expandedData.taskReviewActionItems.splice(deletedIndex,1);
    this.alert.successToast(await this.dynamicLabelPipe.transform(result));
  }

  getActionItemData(data:TaskReviewActionItem_VM){
    var actionItems = new TaskListReview_TaskReviewActionItem_VM();
    actionItems.id = data.id;
    actionItems.type = data.type;
    actionItems.priority = data.priority;
    actionItems.assignees = data.assignees;
    actionItems.assignedDate = data.assignedDate;
    actionItems.dueDate = data.dueDate;
    const idx = this.taskReviewData.taskReviewActionItems.findIndex(item => item.id === data.id);

    if (idx !== -1) {
      this.taskReviewData.taskReviewActionItems[idx] = actionItems;
    } else {
      this.taskReviewData.taskReviewActionItems.push(actionItems);
    }
    if (!this.expandedData) {
      this.expandedData = this.taskReviewData;
    }
  }

  sortTaskReviewData(sort: Sort) {
    this.getTaskReviewDataSource().sort = this.sort;
    const data = this.getTaskReviewDataSource().data;
    if (!sort.active || sort.direction === '') {
      this.getTaskReviewDataSource().data = data;
      return;
    }

    this.getTaskReviewDataSource().data = data.sort((a, b) => {
      const isAsc = sort.direction === 'asc';
      switch (sort.active) {
        case 'number':
          return this.compare(a.number, b.number, isAsc);
        case 'statement':
          return this.compare(a.statement, b.statement, isAsc);
        case 'noOfActionItems':
          return this.compare(a.taskReviewActionItems.length, b.taskReviewActionItems.length, isAsc);
        default:
          return 0;
      }
    });
  }

  compare(a: number | string | Date, b: number | string | Date, isAsc: boolean) {
    return (a < b ? -1 : 1) * (isAsc ? 1 : -1);
  }

  async sortActionItemData(sort: any) {
    const datasource = await this.getActionItemDataSource();
    datasource.sort = this.innerSort;
    const data = this.getActionItemDataSource()?.data;
    if (!sort.active || sort.direction === '') {
      this.getActionItemDataSource().data = data;
      return;
    }

    this.getActionItemDataSource().data = data.sort((a, b) => {
      const isAsc = sort.direction === 'asc';
      switch (sort.active) {
        case 'actionsItems':
          return this.compare(a.type, b.type, isAsc);
        case 'assignees':
          return this.compare(a.assignees, b.assignees, isAsc);
        case 'priority':
          return this.compare(a.priority, b.priority, isAsc);
        case 'dateAssigned':
          return this.compare(a.assignedDate, b.assignedDate, isAsc);  
        case 'dueDate':
          return this.compare(a.dueDate, b.dueDate, isAsc); 
        default:
          return 0;
      }
    });
  }

  openDeleteActionItemDialog(){
    this.dialog.open(this.deleteActionItemDialog, {
      width: '600px',
      height: 'auto',
      hasBackdrop: true,
      disableClose: true,
    });
  }

}

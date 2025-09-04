import { TemplatePortal } from '@angular/cdk/portal';
import { Component, Input, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { MatLegacyTableDataSource as MatTableDataSource } from '@angular/material/legacy-table';
import { TaskListReview_VM } from '@models/TaskListReview/TaskListReview_VM';
import { TaskReviewCreateOption } from '@models/Task_Review/TaskReviewCreateOption';
import { ApiTaskListReviewService } from 'src/app/_Services/QTD/TaskListReview/api.tasklistreview.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { TaskReviewTableComponent } from './task-review-table/task-review-table.component';
import { LabelReplacementPipe } from 'src/app/_Pipes/label-replacement.pipe';
import { TaskListReview_TaskReview_VM } from '@models/TaskListReview/TaskListReview_TaskReview_VM';

@Component({
  selector: 'app-task-list-review-tasks',
  templateUrl: './task-list-review-tasks.component.html',
  styleUrls: ['./task-list-review-tasks.component.scss'],
})
export class TaskListReviewTasksComponent implements OnInit {
  @Input() inputTaskListReviewVM : TaskListReview_VM ;
  @ViewChild('taskReviewTable') taskReviewTable: TaskReviewTableComponent;
 
  constructor(public flyPanelService: FlyInPanelService,
    private taskListReviewService : ApiTaskListReviewService,
    private alert: SweetAlertService,
    private labelPipe:LabelReplacementPipe
    ) {}

  ngOnInit(): void {
  }

  openAddTaskFlyPanel(templateRef: TemplateRef<any>) {
    const portal = new TemplatePortal(templateRef, null);
    this.flyPanelService.open(portal);
  }
  
  async addTasksToTaskListReviewAsync(taskIds : string[]) {
    let option = new TaskReviewCreateOption();
    option.taskIds = taskIds;
    await this.taskListReviewService.createTaskReviewsAsync(this.inputTaskListReviewVM?.id,option).then(async res=> {
      this.inputTaskListReviewVM.taskReviews = this.inputTaskListReviewVM.taskReviews.concat(res);
      if(this.taskReviewTable){
        this.taskReviewTable.inputTaskReviewVMs = this.inputTaskListReviewVM.taskReviews;
      }
      this.alert.successToast(await this.labelPipe.transform('Task') + " Review(s) created successfully");
    });
  }

  getAlreadyLinkedIds(){
    var taskIds = this.inputTaskListReviewVM?.taskReviews?.map(x=>x.taskId) ?? [] ;
    return Array.from(new Set(taskIds));
  }

  getUpdatedLinkedData(data:TaskListReview_TaskReview_VM[]){
    this.inputTaskListReviewVM.taskReviews = data;
  }
}

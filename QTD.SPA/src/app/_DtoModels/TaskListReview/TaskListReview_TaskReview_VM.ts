import { TaskListReview_TaskReviewActionItem_VM } from "./TaskListReview_TaskReviewActionItem_VM";
import { TaskListReview_TaskReviewReviewer_VM } from "./TaskListReview_TaskReviewReviewer_VM";

export class TaskListReview_TaskReview_VM{
    id:string;
    taskId:string;
    number:string;
    statement:string;
    positions:string;
    recentHistoryDate:Date;
    reviewedBy:string;
    conclusion:string;
    reviewDate:Date;
    finding:string;
    status:string;
    taskReviewActionItems:TaskListReview_TaskReviewActionItem_VM[];
    reviewers: TaskListReview_TaskReviewReviewer_VM[];
}
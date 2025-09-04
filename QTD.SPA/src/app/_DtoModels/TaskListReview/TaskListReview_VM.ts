import { TaskListReview_GeneralReviewer_VM } from './TaskListReview_GeneralReviewer_VM';
import { TaskListReview_TaskReview_VM } from './TaskListReview_TaskReview_VM';

export class TaskListReview_VM {
  id?: string;
  title: string;
  typeId: string;
  statusId: string;
  startDate: Date;
  endDate: Date;
  conclusion: string;
  generalReviewers: TaskListReview_GeneralReviewer_VM[] = [];
  taskReviews: TaskListReview_TaskReview_VM[] = [];
  approvalDate: Date;
  signature: string;
  active:boolean;
  positionIds:string[];
  reviewedBy:string;
}

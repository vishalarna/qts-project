import { TaskListReview_TaskReview_VM } from "./TaskListReview_TaskReview_VM";

export class  TaskListReviewOverview_TaskListReview_VM{
    id:string;
    title:string;
    type:string;
    startDate:Date;
    endDate:Date;
    status:string;
    approvalDate:Date;
    active:boolean;
    positions:string[] = [];
    taskReviews:TaskListReview_TaskReview_VM[];
}
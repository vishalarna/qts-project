import { TaskReview_Reviewer_VM } from "./TaskReview_Reviewer_VM";
import { TaskReview_TaskReviewActionItem_VM } from "./TaskReview_TaskReviewActionItem_VM";

export class TaskReview_VM{
         id:string;
         number:string;
         statement:string;
         recentHistoryDate?:string;
         reviewDate?:string;
         reviewers : TaskReview_Reviewer_VM[] = [];
         findingId: string;
         requalificationDueDate?:string;
         notes:string;
         taskReviewActionItems:TaskReview_TaskReviewActionItem_VM[] = [];
         nextTaskReviewId:string;
         taskId:string;
         fullNumber: string;
         trainingIssueId:string;
}
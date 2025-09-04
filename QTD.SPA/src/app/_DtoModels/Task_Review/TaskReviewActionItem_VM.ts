import { TaskReviewActionItem_EnablingObjective_Operation_VM } from "./TaskReviewActionItem_EnablingObjective_Operation_VM";
import { TaskReviewActionItem_Procedure_Operation_VM } from "./TaskReviewActionItem_Procedure_Operation_VM";
import { TaskReviewActionItem_QuestionAndAnswer_Operation_VM } from "./TaskReviewActionItem_QuestionAndAnswer_Operation_VM";
import { TaskReviewActionItem_RegulatoryRequirement_Operation_VM } from "./TaskReviewActionItem_RegulatoryRequirement_Operation_VM";
import { TaskReviewActionItem_SafetyHazard_Operation_VM } from "./TaskReviewActionItem_SafetyHazard_Operation_VM";
import { TaskReviewActionItem_Step_Operation_VM } from "./TaskReviewActionItem_Step_Operation_VM";
import { TaskReviewActionItem_SubDutyArea_Operation_VM } from "./TaskReviewActionItem_SubDutyArea_Operation_VM";
import { TaskReviewActionItem_Suggestion_Operation_VM } from "./TaskReviewActionItem_Suggestion_Operation_VM";
import { TaskReviewActionItem_Tool_Operation_VM } from "./TaskReviewActionItem_Tool_Operation_VM";

export class TaskReviewActionItem_VM{
         id:string;
         type:string;
         assigneeId:string;
         priorityId:string;
         assignedDate: Date;
         notes:string;
         dueDate:Date;
         subDuty_Operations:TaskReviewActionItem_SubDutyArea_Operation_VM[] = [];
         task_number:string;
         task_statement:string;
         step_Operations:TaskReviewActionItem_Step_Operation_VM[] = [];
         questionAndAnswer_Operations:TaskReviewActionItem_QuestionAndAnswer_Operation_VM[] = [];
         suggestion_Operations:TaskReviewActionItem_Suggestion_Operation_VM[] = [];
         conditions:string;
         criteria:string;
         references:string;
         tool_Operations:TaskReviewActionItem_Tool_Operation_VM[] = [];
         enablingObjective_Operations:TaskReviewActionItem_EnablingObjective_Operation_VM[] = [];
         procedure_Operations:TaskReviewActionItem_Procedure_Operation_VM[] = [];
         regulatoryRequirement_Operations:TaskReviewActionItem_RegulatoryRequirement_Operation_VM[] = [];
         safetyHazard_Operations:TaskReviewActionItem_SafetyHazard_Operation_VM[] = [];
         isMeta:boolean;
         priority:string;
         assignees:string;
}
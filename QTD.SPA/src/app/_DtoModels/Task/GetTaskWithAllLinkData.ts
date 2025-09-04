import { ToolDataVM } from "@models/Tool/ToolDataVM";
import { EOWithCountOptions } from "../EnablingObjective/EOWithCountOptions";
import { ILAWithCountOptions } from "../ILA/ILAWithCountOptions";
import { ProcedureWithLinkCount } from "../Procedure/ProcedureWithLinkCount";
import { SafetyHazardWithLinkCount } from "../SaftyHazard/SafetyHazardWithLinkCount";
import { Task_Suggestion } from "../Task_Suggestion/Task_Suggestion";
import { TrainingGroup } from "../TrainingGroup/TrainingGroup";
import { TaskPositionWithCount } from "./TaskPositionWithCount"
import { TaskRRWithCount } from "./TaskRRWithCount";
import { Task_StepDescriptionVM } from "@models/Task_Step/Task_StepDescriptionVM";

export class GetTaskWithAllLinkData {
  taskPositionWithCount : TaskPositionWithCount[] = [];
  ilaWithCountOptions : ILAWithCountOptions[] = [];
  enablingObjectiveWithCountOptions : EOWithCountOptions[] = [];
  proceduresWithLinkCounts : ProcedureWithLinkCount[] = [];
  safetyHazardWithLinkCounts : SafetyHazardWithLinkCount[] = [];
  regulatoryRequirementWithLinkCounts : TaskRRWithCount[] = [];
  trainingGroups : TrainingGroup[] = [];
  tools : ToolDataVM[] = [];
  task_Steps : Task_StepDescriptionVM[] = [];
  task_Suggestions :Task_Suggestion[]=[];
}
